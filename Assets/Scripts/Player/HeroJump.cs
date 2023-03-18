using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroJump
    {
        private const float JumpXDivider = 2f;
        private const float HeightTestRadius = 0.05f;

        private readonly Rigidbody2D _rigidBody2D;
        private readonly AnimationCurve _jumpCurve;
        private readonly LayerMask _ground;
        private readonly Collider2D _heroCollider;
        private readonly float _duration;
        private readonly float _heightTest;
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;

        public float XJumpAxis => _jumpForce / JumpXDivider;

        public bool IsNotGrounded
        {
            get
            {
                var heroCenter = _heroCollider.bounds.center;
                var hit = Physics2D.Raycast(heroCenter, Vector2.down, _heightTest, _ground);

                return hit.collider == null;
            }
        }

        public HeroJump(
            Rigidbody2D rigidbody2D,
            AnimationCurve jumpCurve,
            Collider2D heroCollider,
            LayerMask ground,
            float duration)
        {
            _rigidBody2D = rigidbody2D;
            _jumpCurve = jumpCurve;
            _heroCollider = heroCollider;
            _ground = ground;
            _duration = duration;

            _heightTest = _heroCollider.bounds.extents.y + HeightTestRadius;
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