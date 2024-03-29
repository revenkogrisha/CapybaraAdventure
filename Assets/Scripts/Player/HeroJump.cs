using System;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroJump
    {
        private const float JumpXDivider = 2f;

        private readonly Rigidbody2D _rigidBody2D;
        private readonly AnimationCurve _jumpCurve;
        private readonly Transform _transform;
        private readonly float _duration;
        private HeightCheckService _heightTestService;
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;
        private bool _shouldSaveLastJump = true;

        public float XJumpAxis => _jumpForce / JumpXDivider;
        public bool IsNotGrounded => _heightTestService.IsNotGrounded;
        public Vector2 LastJump { get; private set; } = new();

        public event Action OnJumped;
        public event Action OnLanded;

        public HeroJump(
            IPhysicalObject hero,
            AnimationCurve jumpCurve,
            HeightCheckService heightTestService,
            float duration)
        {
            _rigidBody2D = hero.Rigidbody2D;
            _transform = hero.Transform;
            _jumpCurve = jumpCurve;
            _heightTestService = heightTestService;
            _duration = duration;
        }

        public void TryJump()
        {
            if (_expiredJumpTime > _duration)
                EndJump();

            if (_shouldJump == true)
                PerformJump();
        }

        public void SayShouldJump() => _shouldJump = true;

        public void UpdateForceValue(float value) => _jumpForce = value;

        private void PerformJump()
        {
            OnJumped?.Invoke();

            if (_shouldSaveLastJump == true)
            {
                LastJump = _transform.position;
                _shouldSaveLastJump = false;
            }

            _expiredJumpTime += Time.deltaTime;

            float xAxis = XJumpAxis;
            float yAxis = GetYByCurve(_jumpCurve);

            var jumpVelocity = new Vector2(xAxis, yAxis);
            _rigidBody2D.velocity = jumpVelocity;
        }

        private void EndJump()
        {
            OnLanded?.Invoke();
            
            _shouldJump = false;
            _shouldSaveLastJump = true;
            _expiredJumpTime = 0f;
        }

        private float GetYByCurve(AnimationCurve jumpCurve)
        {
            float progress = _expiredJumpTime / _duration;
            float curveValue = jumpCurve.Evaluate(progress);
            return curveValue * _jumpForce;
        }
    }
}