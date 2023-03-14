using UnityEngine;
using Zenject;
using CapybaraAdventure.Player.UI;

namespace CapybaraAdventure.Player
{
    public class HeroJump : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _jumpCurve;

        private JumpSlider _jumpSlider;
        private JumpButton _jumpButton;
        private bool _shouldJump = false;

        #region MonoBehaviour

        public void OnEnable()
        {
            _jumpButton.OnClicked += SayShouldJump;
        }

        public void OnDisable()
        {
            _jumpButton.OnClicked -= SayShouldJump;
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

        private void TryJump()
        {
            if (_shouldJump)
                PerformJump();
        }

        private void PerformJump()
        {
            Debug.Log("Jump!");

            _shouldJump = false;
        }
    }
}