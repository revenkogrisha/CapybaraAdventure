using UnityEngine.UI;
using UnityEngine;
using CapybaraAdventure.Game;
using Zenject;
using System.Collections;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class JumpSlider : MonoBehaviour
    {
        public const float LerpSpeedIncrease = 0.03f;
        public const float LerpDirectionChangeIntervalInSeconds = 1.2f;
        public const int ChangeDirectionChanceIncrease = 3;
        public const int MaxChangeLerpDirectionChance = 40;
        public const float MaxLerpSpeed = 0.05f;
        private const float MinValue = 0f;

        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]
        [SerializeField] private float _startLerpSpeed = 0.03f;

        private float _lerpSpeed;
        private int _lerpDirectionChangeChance = 10;
        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private PauseManager _pauseManager;
        private PlayerData _playerData;

        public int ChangeDirectionChanceDecrease => 
            _playerData.ChangeDirectionChanceDecrease;

        public float LerpSpeedDecrease => _playerData.LerpSpeedDecrease;
        public float Value => _slider.value;
        public bool IsPaused => _pauseManager.IsPaused;
        public bool IsLerpingUp => _state == JumpSliderState.LerpingUp;
        public bool IsValueOutOfMaxOrMin => 
            _slider.value >= _playerData.MaxDistance 
            || _slider.value <= MinValue;

        #region MonoBehaviour

        private void Awake()
        {
            _lerpSpeed = _startLerpSpeed;
            _slider.value = _slider.minValue;
            UpdateSliderProperties();

            StartCoroutine(RandomlyChangeLerpDirection());
        }

        private void FixedUpdate()
        {
            if (IsPaused)
                return;

            LerpValue();
        }

        #endregion

        [Inject]
        private void Construct(
            PauseManager pauseManager,
            PlayerData playerData)
        {
            _pauseManager = pauseManager;
            _playerData = playerData;
        }

        public void IncreaseLerpDirectionChangeChance() =>
            _lerpDirectionChangeChance += ChangeDirectionChanceIncrease;

        public void DecreaseLerpDirectionChangeChance() =>
            _lerpDirectionChangeChance -= ChangeDirectionChanceDecrease;

        public void TryIncreaseLerpSpeed()
        {
            float increased = _lerpSpeed + LerpSpeedIncrease;
            if (increased > MaxLerpSpeed)
                return;
                
            _lerpSpeed = increased;
        }

        public void TryDecreaseLerpSpeed()
        {
            float decreased = _lerpSpeed - LerpSpeedDecrease;
            if (decreased < _startLerpSpeed)
                return;

            _lerpSpeed = decreased;
        }

        private void UpdateSliderProperties()
        {
            _slider.maxValue = _playerData.MaxDistance;
            _slider.minValue = MinValue;
        }

        private void LerpValue()
        {
            LerpValueByDirection();

            if (IsValueOutOfMaxOrMin)
                ChangeLerpDirection();
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