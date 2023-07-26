using Zenject;

namespace CapybaraAdventure.Player
{
    public class TreasureChest : Chest
    {
        public const int CoinsInsideAmount = 15;

        private PlayerData _playerData;

        public override void Open()
        {
            base.Open();
            ReleaseContent();
        }

        protected void ReleaseContent()
        {
            _playerData.AddTreasureChestCoins();
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}