using UnityEngine.UI;
using UnityEngine;
using CapybaraAdventure.Game;
using Zenject;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class JumpSlider : MonoBehaviour
    {
        private const float LerpSpeedIncrease = 0.05f;
        private const float MaxLerpSpeed = 0.70f;
        private const float MinValue = 3f;
        private const float StartLerpSpeed = 0.35f;

        [SerializeField] private Slider _slider;

        private float _lerpSpeed;
        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private PauseManager _pauseManager;
        private PlayerData _playerData;

        public float LerpSpeedDecrease => _playerData.FinalLerpSpeedDecrease;
        public float Value => _slider.value;
        public bool IsPaused => _pauseManager.IsPaused;
        public bool IsLerpingUp => _state == JumpSliderState.LerpingUp;
        public bool IsValueOutOfMaxOrMin => 
            _slider.value >= _playerData.MaxDistance 
            || _slider.value <= MinValue;

        #region MonoBehaviour

        private void Awake()
        {
            _lerpSpeed = StartLerpSpeed;
            _slider.value = _slider.minValue;
            UpdateSliderProperties();
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
            if (decreased < StartLerpSpeed)
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

        private void ChangeLerpDirection()
        {
            _state = IsLerpingUp
                ? JumpSliderState.LerpingDown
                : JumpSliderState.LerpingUp;
        }
    }
}