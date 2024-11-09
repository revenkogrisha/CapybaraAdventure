using Cinemachine;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class MainCamera : MonoBehaviour
    {
        public const float DefaultFOV = 5.25f;
        
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        
        private bool _isLerping = false;
        private float _targetFOV = 5f;
        private float _lerpStartTime;
        private float _lerpDuration;
        private float _initialFOV;

        public bool IsLerping => _isLerping;
        
        private float FOV { get => _cinemachine.m_Lens.OrthographicSize; set => _cinemachine.m_Lens.OrthographicSize = value; }

        private void Update()
        {
            if (_isLerping == true)
                LerpFOV();
        }

        public void SetDefaultFOV()
        {
            FOV = DefaultFOV;
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

            _initialFOV = FOV; 
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
                FOV = _targetFOV;
                _isLerping = false;
                return;
            }
            
            FOV = Mathf.Lerp(_initialFOV, _targetFOV, timeDelta);
        }
    }
}