using UnityEngine.UI;
using UnityEngine;
using CapybaraAdventure.Game;
using Zenject;
using System.Collections;

namespace CapybaraAdventure.UI
{
    public class JumpSlider : MonoBehaviour
    {
        private const float LerpDirectionChangeIntervalInSeconds = 1f;
        private const int ChangeDirectionChanceIncrease = 3;
        private const int ChangeDirectionChanceDecrease = 6;
        private const int MaxChangeLerpDirectionChance = 50;
        private const float MaxLerpSpeed = 0.038f;
        private const float LerpSpeedIncrease = 0.001f;
        private const float LerpSpeedDecrease = 0.002f;

        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]
        [SerializeField] private float _startLerpSpeed = 0.02f;

        private float _lerpSpeed;
        private int _lerpDirectionChangeChance = 10;
        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private float _minValue;
        private float _maxValue;
        private PauseManager _pauseManager;

        public float Value => _slider.value;
        public bool IsPaused => _pauseManager.IsPaused;
        public bool IsLerpingUp => _state == JumpSliderState.LerpingUp;
        public bool IsValueOutOfMaxOrMin => 
            _slider.value >= _maxValue 
            || _slider.value <= _minValue;

        #region MonoBehaviour

        private void Awake()
        {
            _lerpSpeed = _startLerpSpeed;
            _minValue = _slider.minValue;
            _maxValue = _slider.maxValue;
            _slider.value = _slider.minValue;

            StartCoroutine(RandomlyChangeLerpDirection());
        }

        private void Update()
        {
            if (IsPaused)
                return;

            LerpValue();
        }

        private void LerpValue()
        {
            LerpValueByDirection();

            if (IsValueOutOfMaxOrMin)
                ChangeLerpDirection();
        }

        #endregion

        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        public void IncreaseLerpDirectionChangeChance() =>
            _lerpDirectionChangeChance += ChangeDirectionChanceIncrease;

        public void DecreaseLerpDirectionChangeChance() =>
            _lerpDirectionChangeChance -= ChangeDirectionChanceDecrease;

        public void TryIncreaseLerpSpeed()
        {
            var increased = _lerpSpeed + LerpSpeedIncrease;
            if (increased > _startLerpSpeed)
                return;

            _lerpSpeed = increased;
        }

        public void TryDecreaseLerpSpeed()
        {
            var decreased = _lerpSpeed - LerpSpeedDecrease;
            if (decreased < MaxLerpSpeed)
                return;

            _lerpSpeed = decreased;
        }

        private void LerpValueByDirection()
        {
            _slider.value = IsLerpingUp
                ? _slider.value + _lerpSpeed
                : _slider.value - _lerpSpeed;
        }

        private IEnumerator RandomlyChangeLerpDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(LerpDirectionChangeIntervalInSeconds);

                int randomPercentageChance = Random.Range(0, 100);
                if (_lerpDirectionChangeChance < randomPercentageChance)
                    continue;

                ChangeLerpDirection();
            }
        }

        private void ChangeLerpDirection()
        {
            _state = IsLerpingUp
                ? JumpSliderState.LerpingDown
                : JumpSliderState.LerpingUp;
        }
    }
}