using System;
using CapybaraAdventure.Player;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class HeroPresenter
    {
        private readonly HeroJump _heroJump;
        private readonly JumpButton _jumpButton;
        private readonly JumpSlider _jumpSlider;

        public HeroPresenter(
            HeroJump jump,
            JumpButton button,
            JumpSlider slider
            )
        {
            _heroJump = jump;
            _jumpButton = button;
            _jumpSlider = slider;
        }

        public void Enable()
        {
            _jumpButton.OnClicked += OnClickHandler;
        }

        public void Disable()
        {
            _jumpButton.OnClicked -= OnClickHandler;
        }

        private void OnClickHandler()
        {
            _heroJump.SayShouldJump();
            UpdateForceValue();
            _jumpSlider.TryDecreaseLerpDuration();
        }

        private void UpdateForceValue()
        {
            var value = _jumpSlider.Value;
            _heroJump.UpdateForceValue(value);
        }
    }
}
