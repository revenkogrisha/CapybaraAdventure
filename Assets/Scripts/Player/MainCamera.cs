using Cinemachine;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class MainCamera : MonoBehaviour
    {
        public const float DefaultFOV = 5.75f;
        
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        
        private bool _isLerping = false;
        private float _targetFOV = 5f;
        private float _lerpStartTime;
        private float _lerpDuration;

        public bool IsLerping => _isLerping;

        private void Update()
        {
            if (_isLerping == true)
                LerpFOV();
        }

        public void SetDefaultFOV()
        {
            _cinemachine.m_Lens.OrthographicSize = DefaultFOV;
        }

        public void SetFollow(Transform target)
        {
            _cinemachine.Follow = target;
        }

        public void SetLerpFOV(float fov, float duration)
        {
            if (_isLerping == true)
            {
                LerpFOV(forceFinish: true);
                SetLerpFOV(fov, duration);
                return;
            }
            
            print("+ lerp");
            _targetFOV = fov;
            _lerpStartTime = Time.time;
            _lerpDuration = duration;
            _isLerping = true;
        }

        private void LerpFOV(bool forceFinish = false)
        {
            float timeDelta = (Time.time - _lerpStartTime) / _lerpDuration;
            if (timeDelta >= 0.99f || forceFinish == true)
            {
                _cinemachine.m_Lens.OrthographicSize = _targetFOV;
                _isLerping = false;
                print("- lerp");
                return;
            }
            
            _cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(_cinemachine.m_Lens.OrthographicSize, _targetFOV, timeDelta);
            print("lerp: " + _cinemachine.m_Lens.OrthographicSize);
        }
    }
}