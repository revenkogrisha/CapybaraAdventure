using System;
using UnityEngine;

namespace UnityTools
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private int _cooldown = 2;

        private float _time = 0;
        private int _timeInSeconds = 0;
        private int _checkedTime = 0;

        public Action OnCooldownPassed;

        #region MonoBehaviour

        private void Update()
        {
            _timeInSeconds = GetTimeInSeconds();

            if (IsRightCooldown())
            {
                OnCooldownPassed?.Invoke();
                _checkedTime = _timeInSeconds;
            }
        }

        #endregion

        private int GetTimeInSeconds()
        {
            _time += Time.deltaTime;

            return Mathf.RoundToInt(_time);
        }

        private bool IsRightCooldown()
        {
            if ((_timeInSeconds % _cooldown) != 0
                || _timeInSeconds <= _checkedTime)
                return false;

            return true;
        }
    }
}
