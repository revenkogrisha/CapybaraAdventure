using System;
using CapybaraAdventure.Save;
using UnityEngine;
using CapybaraAdventure.UI;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Player
{
    public class PlayerData : MonoBehaviour
    {
        private const float MaxDistanceUpgradeLimit = 25f;
        private const float MaxDistanceUpgradeIncrease = 1f;
        private const float FoodBonusIncrease = 0.25f;
        public const float FoodBonusLimit = 1f;
        private const int DefaultUpgradeCost = 15;

        private float _changeDirectionChanceDecrease = 7;
        private float _lerpSpeedDecrease = 0.07f;

        public int Coins { get; private set; } = 0;
        public float MaxDistance { get; private set; } = 15f;
        public float FoodBonus { get; private set; } = 0f;
        public int FoodUpgradeCost { get; private set; } = DefaultUpgradeCost;
        public int DistanceUpgradeCost { get; private set; } = DefaultUpgradeCost;

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

        public void LoadData(SaveData data)
        {
            Coins = data.Coins;
            MaxDistance = data.MaxDistance;
            FoodBonus = data.FoodBonus;
            DistanceUpgradeCost = data.DistanceUpgradeCost;
            FoodUpgradeCost = data.FoodUpgradeCost;
        }

        public void AddSimpleChestCoins()
        { 
            int amount = SimpleChest.CoinsInsideAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Coins += amount;

            OnCoinsChanged?.Invoke(Coins);
        }

        public void AddTreasureChestCoins()
        { 
            int amount = SimpleChest.CoinsInsideAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Coins += amount;

            OnCoinsChanged?.Invoke(Coins);
        }

        public void AddRandomAdCoins(int minInclusive, int maxExclusive)
        {
            int amount = Random.Range(minInclusive, maxExclusive);

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Coins += amount;

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

            DistanceUpgradeCost = block.Cost;

            float increased = MaxDistance += MaxDistanceUpgradeIncrease;
            if (increased > MaxDistanceUpgradeLimit)
                return;

            MaxDistance = increased;
        }

        public void UpgradeFoodBonus(UpgradeBlock block)
        {
            //  It is supposed to work like this.
            //  It makes illusion of unlimited upgrading for player

            FoodUpgradeCost = block.Cost;

            float increased = FoodBonus += FoodBonusIncrease;
            if (increased > FoodBonusLimit)
                return;

            FoodBonus = increased;
        }

        private bool TrySubstractCoins(int amount)
        {
            if (Coins < amount)
                return false;

            Coins -= amount;

            OnCoinsChanged?.Invoke(Coins);
            return true;
        }
    }
}