using UnityEngine;
using System;

namespace CapybaraAdventure.Player
{
    public class HeroJump
    {
        private readonly Rigidbody2D _rigidBody2D;
        private readonly AnimationCurve _jumpCurve;
        private readonly float _duration;
        private readonly float _jumpXDivider = 2f;
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;

        public float XJumpAxis => _jumpForce / _jumpXDivider;

        public HeroJump(
            Rigidbody2D rigidbody2D,
            AnimationCurve jumpCurve,
            float duration)
        {
            _rigidBody2D = rigidbody2D;
            _jumpCurve = jumpCurve;
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