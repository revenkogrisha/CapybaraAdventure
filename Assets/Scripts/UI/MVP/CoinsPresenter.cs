using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class CoinsPresenter
    {
        private readonly PlayerData _playerData;
        private readonly UIText _coinsText;

        public CoinsPresenter(PlayerData playerData, UIText coinsText)
        {
            _playerData = playerData;
            _coinsText = coinsText;
        }

        public void Init()
        {
            int coins = _playerData.Coins;
            UpdateCoinsText(coins);
        }

        public void Enable()
        {
            _playerData.OnCoinsChanged += UpdateCoinsText;
        }

        public void Disable()
        {
            _playerData.OnCoinsChanged -= UpdateCoinsText;
        }

        private void UpdateCoinsText(int amount)
        {
            string text = $"{amount}";
            _coinsText.UpdateText(text);
        }
    }
}
