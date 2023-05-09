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
        private const float LerpSpeedIncrease = 0.05f;
        private const float LerpDirectionChangeIntervalInSeconds = 1.5f;
        private const int ChangeDirectionChanceIncrease = 3;
        private const float MaxLerpDirectionChangeChance = 35;
        private const float MinLerpDirectionChangeChance = 0;
        private const float MaxLerpSpeed = 0.75f;
        private const float MinValue = 3f;
        private const float StartLerpSpeed = 0.35f;

        [SerializeField] private Slider _slider;
        [Tooltip("Put value in seconds")]

        private float _lerpSpeed;
        private float _lerpDirectionChangeChance = 10;
        private JumpSliderState _state = JumpSliderState.LerpingUp;
        private PauseManager _pauseManager;
        private PlayerData _playerData;

        public float ChangeDirectionChanceDecrease => 
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
            _lerpSpeed = StartLerpSpeed;
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

        public void IncreaseLerpDirectionChangeChance() 
        {
            float increased = _lerpDirectionChangeChance + ChangeDirectionChanceIncrease;

            if (increased > MaxLerpDirectionChangeChance)
                return;

            _lerpDirectionChangeChance = increased;
        }

        public void DecreaseLerpDirectionChangeChance()
        {
            float decreased = _lerpDirectionChangeChance - ChangeDirectionChanceDecrease;

            if (decreased < MinLerpDirectionChangeChance)
                return;

            _lerpDirectionChangeChance = decreased;
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