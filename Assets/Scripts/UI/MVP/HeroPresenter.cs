using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class HeroPresenter
    {
        private readonly Hero _hero;
        private readonly HeroJump _heroJump;
        private readonly JumpButton _jumpButton;
        private readonly JumpSlider _jumpSlider;
        private readonly GameUI _gameUI;

        public HeroPresenter(
            Hero hero,
            JumpButton button,
            JumpSlider slider,
            GameUI gameUI)
        {
            _hero = hero;
            _jumpButton = button;
            _jumpSlider = slider;
            _gameUI = gameUI;

            _heroJump = _hero.Jump;
        }

        public void Enable()
        {
            _hero.OnFoodEaten += OnFoodEatenHandler;
            _jumpButton.OnClicked += OnClickedHandler;
            _gameUI.SwordAdRewarded.OnRewardGotten += OnSwordAdRewardedHandler;
        }

        public void Disable()
        {
            _hero.OnFoodEaten -= OnFoodEatenHandler;
            _jumpButton.OnClicked -= OnClickedHandler;
            _gameUI.SwordAdRewarded.OnRewardGotten -= OnSwordAdRewardedHandler;
        }

        private void OnFoodEatenHandler()
        {
            _jumpSlider.TryDecreaseLerpSpeed();
            _jumpSlider.DecreaseLerpDirectionChangeChance();
        }

        private void OnClickedHandler()
        {
            if (_heroJump.IsNotGrounded)
                return;
                
            UpdateForceValue();
            _heroJump.SayShouldJump();

            _jumpSlider.TryIncreaseLerpSpeed();
            _jumpSlider.IncreaseLerpDirectionChangeChance();
        }

        private void UpdateForceValue()
        {
            float value = _jumpSlider.Value;
            _heroJump.UpdateForceValue(value);
        }

        private void OnSwordAdRewardedHandler()
        {
            _hero.GetSword();
        }
    }
}