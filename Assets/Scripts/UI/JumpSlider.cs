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
        private const int ChangeDirectionChanceIncrease = 5;
        private const int ChangeDirectionChanceDecrease = 7;
        private const int MaxChangeLerpDirectionChance = 70;
        private const float MaxLerpSpeed = 0.035f;
        private const float LerpSpeedIncrease = 0.0005f;
        private const float LerpSpeedDecrease = 0.001f;

        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]
        [SerializeField] private float _startLerpDuration = 1f;

        private float _lerpSpeed = 0.02f;
        private int _lerpDirectionChangeChance = 30;
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
            _minValue = _slider.minValue;
            _maxValue = _slider.maxValue;
            _slider.value = _slider.minValue;

            //StartCoroutine(RandomlyChangeLerpDirection());
        }

        private void Update()
        {
            if (IsPaused)
                return;

            //LerpSliderValue();

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
            if (increased > _startLerpDuration)
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
                    yield return null;

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