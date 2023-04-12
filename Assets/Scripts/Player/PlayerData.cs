using System;
using CapybaraAdventure.Save;
using UnityEngine;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        public const string DistanceUpgradeCost = nameof(DistanceUpgradeCost);
        private const float MaxDistanceUpgradeLimit = 25f;
        private const float MaxDistanceUpgradeIncrease = 1f;

        private readonly SaveService _saveService;

        public int Coins { get; private set; } = 0;
        public int ChangeDirectionChanceDecrease { get; private set; } = 7;
        public float LerpSpeedDecrease { get; private set; } = 0.07f;
        public float MaxDistance { get; private set; } = 15f;

        public event Action<int> OnCoinsChanged;

        public PlayerData(SaveService saveService)
        {
            _saveService = saveService;
            _saveService.OnDataLoaded += Init;
        }

        public void Disable()
        {
            _saveService.OnDataLoaded -= Init;
        }

        public void AddSimpleChestCoins()
        { 
            var amount = SimpleChest.CoinsInsideAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Coins += amount;
            PlayerPrefs.SetInt(SaveService.Coins, Coins);

            OnCoinsChanged?.Invoke(Coins);
        }

        public bool TrySubstractCoins(UpgradeBlock block)
        {
            int cost = block.Cost;
            return TrySubstractCoins(cost);
        }

        public void UpgradeJumpDistance(UpgradeBlock block)
        {
            //  It is supposed to work like this.
            //  It makes illusion of unlimited upgrading for player

            int cost = block.Cost;
            PlayerPrefs.SetInt(DistanceUpgradeCost, cost);

            float increased = MaxDistance += MaxDistanceUpgradeIncrease;
            if (increased > MaxDistanceUpgradeLimit)
                return;

            MaxDistance = increased;
            PlayerPrefs.SetFloat(nameof(MaxDistance), MaxDistance);
        }

        private void Init()
        {
            Coins = _saveService.Data.Coins;
            MaxDistance = _saveService.Data.MaxDistance;
        }

        private bool TrySubstractCoins(int amount)
        {
            if (Coins < amount)
                return false;

            Coins -= amount;
            PlayerPrefs.SetInt(SaveService.Coins, Coins);

            OnCoinsChanged?.Invoke(Coins);
            return true;
        }
    }
}