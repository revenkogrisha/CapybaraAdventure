using UnityEngine;
using Zenject;
using CapybaraAdventure.UI;
using System;

namespace CapybaraAdventure.Player
{
    public class HeroJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private AnimationCurve _jumpCurve;

        private JumpSlider _jumpSlider;
        private JumpButton _jumpButton;
        private readonly float _jumpXDivider = 2f; 
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;

        public float XJumpAxis => _jumpForce / _jumpXDivider;

        public event Action OnJumped;

        #region MonoBehaviour

        public void OnEnable()
        {
            _jumpButton.OnClicked += SayShouldJump;
            _jumpButton.OnClicked += UpdateForceValue;
        }

        public void OnDisable()
        {
            _jumpButton.OnClicked -= SayShouldJump;
            _jumpButton.OnClicked -= UpdateForceValue;
        }
        
        private void Update()
        {
            TryJump();
        }

        #endregion

        [Inject]
        public void Construct(JumpButton button, JumpSlider slider)
        {
            _jumpSlider = slider;
            _jumpButton = button;
        }

        private void SayShouldJump()
        {
            _shouldJump = true;
        }

        private void UpdateForceValue()
        {
            _jumpForce = _jumpSlider.Value;
        }

        private void TryJump()
        {
            if (_expiredJumpTime > _duration)
                EndJump();

            if (_shouldJump == true)
                PerformJump();
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