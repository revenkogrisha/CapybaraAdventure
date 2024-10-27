using UnityEngine;

namespace Core.Level
{
    public class Parallax : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Settings")]
        [SerializeField] private bool _applyX = true;
        [SerializeField, Range(0f, 1f)] private float _effectX;

        [Space]
        [SerializeField] private bool _applyY = false;
        [SerializeField, Range(0f, 1f)] private float _effectY;
        
        private float _length;
        private Transform _camera;
        private Transform _thisTransform;
        private Vector2 _startPosition;
        private float _temp;

        #region MonoBehaviour

        private void Awake()
        {
            _thisTransform = transform;
            _camera = Camera.main.transform;
            _startPosition = _thisTransform.position;
            _length = _spriteRenderer.bounds.size.x;
        }
        
        private void FixedUpdate()
        {
            Perform();
            
            if (_temp > _startPosition.x + _length)
                MoveRight();
            
            if (_temp < _startPosition.x - _length)
                MoveLeft();
        }

        #endregion

        private void Perform()
        {
            _temp = _camera.position.x * (1 - _effectX);

            Vector2 parallaxed = GetParallaxedPosition();
            _thisTransform.position = parallaxed;
        }

        private void MoveRight() =>
            _startPosition.x += _length;

        private void MoveLeft() =>
            _startPosition.x -= _length;

        private Vector2 GetParallaxedPosition()
        {
            Vector2 parallaxed = _thisTransform.position;
            if (_applyX == true)
            {
                float distanceX = _camera.position.x * _effectX;
                parallaxed.x = _startPosition.x + distanceX;
            }

            if (_applyY == true)
            {
                float distanceY = _camera.position.y * _effectY;
                parallaxed.y = _startPosition.y + distanceY;
            }

            return parallaxed;
        }
    }
}