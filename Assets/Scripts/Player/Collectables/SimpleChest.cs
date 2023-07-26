using Zenject;

namespace CapybaraAdventure.Player
{
    public class SimpleChest : Chest
    {
        public const int CoinsInsideAmount = 5;

        private PlayerData _playerData;

        public override void Open()
        {
            base.Open();
            ReleaseContent();
        }

        protected void ReleaseContent()
        {
            _playerData.AddSimpleChestCoins();
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}