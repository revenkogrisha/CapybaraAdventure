using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Player
{
    public class TreasureChest : Chest
    {
        public const int CoinsInsideAmount = 15;
        public const int FoodInsideAmount = 5;

        private PlayerData _playerData;

        public override void Open()
        {
            base.Open();
            GetComponent<BoxCollider2D>().enabled = false;
            
            ReleaseContent();
        }

        protected void ReleaseContent()
        {
            _playerData.AddTreasureChestReward();
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}