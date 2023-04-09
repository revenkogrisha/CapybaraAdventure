using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class CoinsPresenter
    {
        private readonly PlayerData _playerData;
        private readonly CoinsUIContainer _container;
        private readonly UIText _coinsText;

        public CoinsPresenter(PlayerData playerData, CoinsUIContainer container)
        {
            _playerData = playerData;
            _container = container;

            _coinsText = _container.Text;
        }

        public void Init()
        {
            _container.transform.SetAsLastSibling();

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
