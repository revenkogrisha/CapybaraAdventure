using CapybaraAdventure.Player;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class AdCoinsRewarder : AdRewardGranter
    {
        private PlayerData _playerData;

        [Inject]
        private void Construct(
            PlayerData playerData)
        {
            _playerData = playerData;
        }

        protected override void GrantReward()
        {
            _playerData.AddSimpleChestCoins();
        }
    }
}