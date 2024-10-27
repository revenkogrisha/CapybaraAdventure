using Zenject;

namespace CapybaraAdventure.Player
{
    public class MelonChest : Chest
    {
        public const int SimpleAmount = 1;
        
        private PlayerData _playerData;

        public override void Open()
        {
            base.Open();
            ReleaseContent();
        }

        protected void ReleaseContent()
        {
            _playerData.AddSimpleMelon();
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}