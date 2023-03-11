using UnityEngine.UI;
using UnityEngine;
using System.ComponentModel;

namespace CapybaraAdventure.Player
{
    public class JumpSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]
        [SerializeField] private float _startLerpDuration = 1f;

        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private float _timeElapsed = 0f;
        private float _minValue;
        private float _maxValue;
        private float _lerpDuration;

        #region MonoBehaviour

        private void Awake()
        {
            _lerpDuration = _startLerpDuration;
            _minValue = _slider.minValue;
            _maxValue = _slider.maxValue;
        }

        private void Start()
        {
            _slider.value = _slider.minValue;
        }

        private void Update()
        {
            LerpSliderValue();
        }

        #endregion

        private void LerpSliderValue()
        {
            switch (_state)
            {
                case JumpSliderState.LerpingUp:
                    LerpUp();
                    break;

                case JumpSliderState.LerpingDown:
                    LerpDown();
                    break;

                default:
                    throw new InvalidEnumArgumentException("Unknown JumpSliderState case has occured!");
            }          
        }

        private void LerpDown()
        {
            if (_timeElapsed < _lerpDuration)
            {
                var progress = _timeElapsed / _lerpDuration;
                _slider.value = Mathf.Lerp(_maxValue, _minValue, progress);

                _timeElapsed += Time.deltaTime;
                return;
            }

            _timeElapsed = 0f;
            _state = JumpSliderState.LerpingUp;
        }

        private void LerpUp()
        {
            if (_timeElapsed < _lerpDuration)
            {
                var progress = _timeElapsed / _lerpDuration;
                _slider.value = Mathf.Lerp(_minValue, _maxValue, progress);

                _timeElapsed += Time.deltaTime;
                return;
            }

            _timeElapsed = 0f;
            _state = JumpSliderState.LerpingDown;
        }
    }
}