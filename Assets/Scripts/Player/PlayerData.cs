using System;
using CapybaraAdventure.Save;
using UnityEngine;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        public const string DistanceUpgradeCost = nameof(DistanceUpgradeCost);
        public const string FoodUpgradeCost = nameof(FoodUpgradeCost);
        private const float MaxDistanceUpgradeLimit = 25f;
        private const float MaxDistanceUpgradeIncrease = 1f;
        private const float FoodBonusIncrease = 0.25f;
        public const float FoodBonusLimit = 1f;

        private readonly SaveService _saveService;
        private float _changeDirectionChanceDecrease = 7;
        private float _lerpSpeedDecrease = 0.07f;

        public SaveData Data => _saveService.Data;
        public int Coins { get; private set; } = 0;
        public float MaxDistance { get; private set; } = 15f;
        public float FoodBonus { get; private set; } = 0f;

        public float ChangeDirectionChanceDecrease
        {
            get => _changeDirectionChanceDecrease + (_changeDirectionChanceDecrease * FoodBonus);
            private set => _changeDirectionChanceDecrease = value;
        }

        public float LerpSpeedDecrease
        {
            get => _lerpSpeedDecrease + (_lerpSpeedDecrease * FoodBonus);
            private set => _lerpSpeedDecrease = value;
        }

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

        public void UpgradeFoodBonus(UpgradeBlock block)
        {
            //  It is supposed to work like this.
            //  It makes illusion of unlimited upgrading for player

            int cost = block.Cost;
            PlayerPrefs.SetInt(FoodUpgradeCost, cost);

            float increased = FoodBonus += FoodBonusIncrease;
            if (increased > FoodBonusLimit)
                return;

            FoodBonus = increased;
            PlayerPrefs.SetFloat(nameof(FoodBonus), FoodBonus);
        }

        private void Init()
        {
            Coins = Data.Coins;
            MaxDistance = Data.MaxDistance;
            FoodBonus = Data.FoodBonus;
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