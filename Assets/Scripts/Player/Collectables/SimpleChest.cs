using Zenject;

namespace CapybaraAdventure.Player
{
    public class SimpleChest : Chest
    {
        public const int CoinsInsideAmount = 5;

        private PlayerData _playerData;

        protected override void ReleaseContent()
        {
            _playerData.AddSimpleChestCoins();
            print("Added: " + _playerData.Coins);
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}