using UnityEngine;
using NTC.Global.Pool;
using Cysharp.Threading.Tasks;

namespace CapybaraAdventure.Level
{
    public class MovingObject : MonoBehaviour, IPoolItem
    {
        private const float DirectionBlockDuration = 0.1f; // in seconds

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private Collider2D _collider2D;

        [Header("Movement Settings")]
        [SerializeField, Min(0f)] private float _leftOffset = 0f;
        [SerializeField, Min(0f)] private float _rightOffset = 0f;
        [SerializeField] private MovingDirection _startDirection = MovingDirection.Left;
        [SerializeField, Min(0f)] private float _speed = 2f;
        [SerializeField] private bool _enabled = true;

        [Header("Reflection")]
        [SerializeField] private bool _reflectObject = true;

        private float _leftBorder;
        private float _rightBorder;
        private MovingDirection _currentDirection;
        private Transform _transform;
        private bool _canChangeDirection = true;

        public bool IsDirectedLeft => _currentDirection == MovingDirection.Left;
        public Rigidbody2D Rigidbody2D => _rigidBody2D;

        #region MonoBehaviour

        private void Update()
        {
            if (_enabled == true)
            {
                CheckBordersAndChangeDirection();
                Move();
            }
        }
        
        #endregion

        public void OnSpawn()
        {
            InitFields();
        }

        public void OnDespawn()
        {
            Destroy(gameObject);
        }

        public void InitFields()
        {
            _transform = transform;

            float originalX = _transform.position.x;
            _leftBorder = originalX - _leftOffset;
            _rightBorder = originalX + _rightOffset;
            _currentDirection = _startDirection;
        }
        
        public void BecomePhysical()
        {
            _enabled = false;
            _collider2D.isTrigger = false;
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private void CheckBordersAndChangeDirection()
        {
            float posX = _transform.position.x;
            if (posX < _leftBorder || posX > _rightBorder)
                TryChangeDirection();
        }

        private void Move()
        {
            float directedSpeed = GetDirectedSpeed();
            var velocity = new Vector2(directedSpeed, 0f);

            _rigidBody2D.velocity = velocity;
        }

        private float GetDirectedSpeed()
        {
            return _speed * (float)_currentDirection;
        }

        private void TryChangeDirection()
        {
            if (_canChangeDirection == false)
                return;

            _currentDirection = IsDirectedLeft 
                ? MovingDirection.Right 
                : MovingDirection.Left;

            if (_reflectObject == true)
                Reflect();

            BlockDirectionForDuration().Forget(exc => throw exc);
        }

        private void Reflect()
        {
            Vector3 reflected = _transform.localScale;
            reflected.x *= -1f;

            _transform.localScale = reflected; 
        }

        private async UniTask BlockDirectionForDuration()
        {
            _canChangeDirection = false;
            await UniTask.WaitForSeconds(DirectionBlockDuration);
            _canChangeDirection = true;
        }
    }
}