using UnityEngine.UI;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    public class JumpSlider : MonoBehaviour
    {
        private const float DifficultyLerpDurationDecrease = 0.05f;
        private const float MinLerpDuration = 0.3f;

        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]
        [SerializeField] private float _startLerpDuration = 1f;

        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private float _timeElapsed = 0f;
        private float _minValue;
        private float _maxValue;
        private float _lerpDuration;

        public float Value => _slider.value;

        #region MonoBehaviour

        private void Awake()
        {
            _lerpDuration = _startLerpDuration;
            _minValue = _slider.minValue;
            _maxValue = _slider.maxValue;
            _slider.value = _slider.minValue;
        }

        private void Update()
        {
            LerpSliderValue();
        }

        #endregion

        public void TryDecreaseLerpDuration()
        {
            var decreased = _lerpDuration - DifficultyLerpDurationDecrease;
            if (decreased < MinLerpDuration)
                return;

            _lerpDuration = decreased;
        }

        private void LerpSliderValue()
        {
            var isLerpingUp = _state == JumpSliderState.LerpingUp;
            if (_timeElapsed < _lerpDuration)
            {
                var progress = _timeElapsed / _lerpDuration;
                _slider.value = isLerpingUp
                    ? LerpUp(progress) 
                    : LerpDown(progress);

                _timeElapsed += Time.deltaTime;
                return;
            }

            _timeElapsed = 0f;
            _state = isLerpingUp
                ? JumpSliderState.LerpingDown
                : JumpSliderState.LerpingUp;
        }

        private float LerpDown(float progress)
        {
            return Mathf.Lerp(_maxValue, _minValue, progress);
        }

        private float LerpUp(float progress)
        {
            return Mathf.Lerp(_minValue, _maxValue, progress);
        }
    }
}