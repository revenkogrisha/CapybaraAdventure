using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroJump
    {
        private const float JumpXDivider = 2f;

        private readonly Rigidbody2D _rigidBody2D;
        private readonly AnimationCurve _jumpCurve;
        private HeightCheckService _heightTestService;
        private readonly float _duration;
        private readonly float _heightTest;
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;

        public float XJumpAxis => _jumpForce / JumpXDivider;

        public bool IsNotGrounded => _heightTestService.IsNotGrounded;

        public HeroJump(
            Rigidbody2D rigidbody2D,
            AnimationCurve jumpCurve,
            HeightCheckService heightTestService,
            float duration)
        {
            _rigidBody2D = rigidbody2D;
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

        public void SayShouldJump()
        {
            _shouldJump = true;
        }

        public void UpdateForceValue(float value)
        {
            _jumpForce = value;
        }

        private void PerformJump()
        {
            _expiredJumpTime += Time.deltaTime;

            var xAxis = XJumpAxis;
            var yAxis = GetYByCurve(_jumpCurve);

            var jumpVelocity = new Vector2(xAxis, yAxis);
            _rigidBody2D.velocity = jumpVelocity;
        }

        private void EndJump()
        {
            _shouldJump = false;
            _expiredJumpTime = 0f;
        }

        private float GetYByCurve(AnimationCurve jumpCurve)
        {
            var progress = _expiredJumpTime / _duration;
            var curveValue = jumpCurve.Evaluate(progress);
            return curveValue * _jumpForce;
        }
    }
}