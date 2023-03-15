using UnityEngine;
using Zenject;
using CapybaraAdventure.Player.UI;

namespace CapybaraAdventure.Player
{
    public class HeroJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private AnimationCurve _jumpCurve;

        private JumpSlider _jumpSlider;
        private JumpButton _jumpButton;
        private bool _shouldJump = false;
        private float _expiredJumpTime = 0f;
        private float _jumpForce;

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
            if (_shouldJump)
                PerformJump();
        }

        private void PerformJump()
        {
            _expiredJumpTime += Time.deltaTime;

            if (_expiredJumpTime > _duration)
                _shouldJump = false;

            var progress = _expiredJumpTime / _duration;
            var y = _jumpCurve.Evaluate(progress);

            var jumpVelocity = new Vector2(_jumpForce, y * _jumpForce);
            _rigidBody2D.velocity = jumpVelocity;
        }
    }
}