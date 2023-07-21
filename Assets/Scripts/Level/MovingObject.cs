using System.Collections;
using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class MovingObject : MonoBehaviour
    {
        private const float DirectionBlockDuration = 0.1f; // in seconds

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [Header("Moving borders")]
        [SerializeField, Min(0f)] private float _leftOffset = 0f;
        [SerializeField, Min(0f)] private float _rightOffset = 0f;
        [Header("Settings")]
        [SerializeField] private MovingDirection _startDirection = MovingDirection.Left;
        [SerializeField, Min(0f)] private float _speed = 2f;
        [SerializeField] private bool _reflectObject = true;

        private float _leftBorder;
        private float _rightBorder;
        private MovingDirection _currentDirection;
        private Transform _transform;
        private bool _canChangeDirection = true;

        #region MonoBehaviour
        
        private void Awake()
        {
            InitFields();
        }

        private void Update()
        {
            float posX = _transform.position.x;
            if (posX < _leftBorder || posX > _rightBorder)
                TryChangeDirection();

            Move();
        }
        
        #endregion

        private void InitFields()
        {
            _transform = transform;

            float originalX = _transform.position.x;
            _leftBorder = originalX - _leftOffset;
            _rightBorder = originalX + _rightOffset;
            _currentDirection = _startDirection;
        }

        private void Move()
        {
            float directedSpeed = GetDirectedSpeed();
            var velocity = new Vector2(directedSpeed, 0f);

            _rigidBody2D.velocity = velocity;
        }

        private float GetDirectedSpeed() => _speed * (float)_currentDirection;

        private void TryChangeDirection()
        {
            if (_canChangeDirection == false)
                return;

            if (_currentDirection == MovingDirection.Left)
                _currentDirection = MovingDirection.Right;
            else
                _currentDirection = MovingDirection.Left;

            if (_reflectObject == true)
                Reflect();

            StartCoroutine(BlockDirection());
        }

        private void Reflect()
        {
            Vector3 reflected = _transform.localScale;
            reflected.x *= -1f;

            _transform.localScale = reflected; 
        }

        private IEnumerator BlockDirection()
        {
            _canChangeDirection = false;

            yield return new WaitForSeconds(DirectionBlockDuration);

            _canChangeDirection = true;
        }
    }
}