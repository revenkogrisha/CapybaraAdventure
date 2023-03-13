using UnityEngine;
using Zenject;
using CapybaraAdventure.Player.UI;

namespace CapybaraAdventure.Player
{
    public class HeroJump : IInitializable, ILateDisposable
    {
        private JumpSlider _jumpSlider;
        private JumpButton _jumpButton;
        private AnimationCurve _jumpCurve;

        public HeroJump(AnimationCurve jumpCurve)
        {
            _jumpCurve = jumpCurve;
        }

        #region Zenject

        public void Initialize()
        {
            _jumpButton.OnClicked += TryJump;
        }

        public void LateDispose()
        {
            _jumpButton.OnClicked -= TryJump;
        }
        
        [Inject]
        public void Construct(JumpButton button, JumpSlider slider)
        {
            _jumpSlider = slider;
            _jumpButton = button;
        }

        #endregion

        private void TryJump()
        {
            var jumpForce = _jumpSlider.Value;
            //Via bool
        }
    }
}