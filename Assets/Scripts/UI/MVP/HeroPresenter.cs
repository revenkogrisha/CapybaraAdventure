using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class HeroPresenter
    {
        private readonly Hero _hero;
        private readonly HeroJump _heroJump;
        private readonly JumpButton _jumpButton;
        private readonly JumpSlider _jumpSlider;

        public HeroPresenter(
            Hero hero,
            JumpButton button,
            JumpSlider slider)
        {
            _hero = hero;
            _jumpButton = button;
            _jumpSlider = slider;

            _heroJump = _hero.Jump;
        }

        public void Enable()
        {
            _hero.OnFoodEaten += OnFoodEatenHandler;
            _jumpButton.OnClicked += OnClickedHandler;
        }

        public void Disable()
        {
            _hero.OnFoodEaten -= OnFoodEatenHandler;
            _jumpButton.OnClicked -= OnClickedHandler;
        }

        private void OnFoodEatenHandler()
        {
            _jumpSlider.TryDecreaseLerpSpeed();
        }

        private void OnClickedHandler()
        {
            if (_heroJump.IsNotGrounded)
                return;
                
            UpdateForceValue();
            _heroJump.SayShouldJump();

            _jumpSlider.TryIncreaseLerpSpeed();
        }

        private void UpdateForceValue()
        {
            float value = _jumpSlider.Value;
            _heroJump.UpdateForceValue(value);
        }
    }
}