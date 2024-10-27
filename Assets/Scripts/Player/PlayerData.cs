using System;
using CapybaraAdventure.Save;
using CapybaraAdventure.UI;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        private const float MaxDistanceUpgradeLimit = 22f;
        private const float MaxDistanceUpgradeIncrease = 1f;
        private const float LerpSpeedDecrease = 0.08f;
        private const float FoodBonusIncrease = 0.125f;
        public const float FoodBonusLimit = 1f;

        public int Coins { get; private set; }
        public int Food { get; private set; }
        public float MaxDistance { get; private set; }
        public float FoodBonus { get; private set; }
        public int FoodUpgradeCost { get; private set; }
        public int DistanceUpgradeCost { get; private set; }
        public bool IsCutsceneWatched { get; set; }

        public float FinalLerpSpeedDecrease
        {
            get => LerpSpeedDecrease + (LerpSpeedDecrease * FoodBonus);
        }

        public event Action<int> OnCoinsChanged;
        public event Action<int> OnFoodChanged;

        public void LoadData(SaveData data)
        {
            Coins = data.Coins;
            Food = data.Food;
            MaxDistance = data.MaxDistance;
            FoodBonus = data.FoodBonus;
            DistanceUpgradeCost = data.DistanceUpgradeCost;
            FoodUpgradeCost = data.FoodUpgradeCost;
            IsCutsceneWatched = data.IsCutsceneWatched;
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
            int amount = TreasureChest.CoinsInsideAmount;

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

        public void AddSimpleMelon()
        {
            int amount = MelonChest.SimpleAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Food += amount;
        }

        public bool TrySubstractCoins(UpgradeBlock block)
        {
            int cost = block.Cost;
            return TrySubstractCoins(cost);
        }

        public void UpgradeJumpDistance(UpgradeBlock block)
        {
            DistanceUpgradeCost = block.Cost;

            float increased = MaxDistance += MaxDistanceUpgradeIncrease;
            //  It is supposed to work like this.
            //  It makes illusion of unlimited upgrading for player
            if (increased > MaxDistanceUpgradeLimit)
                return;

            MaxDistance = increased;
        }

        public void UpgradeFoodBonus(UpgradeBlock block)
        {
            FoodUpgradeCost = block.Cost;

            float increased = FoodBonus += FoodBonusIncrease;
            //  It is supposed to work like this.
            //  It makes illusion of unlimited upgrading for player
            if (increased > FoodBonusLimit)
                return;

            FoodBonus = increased;
        }
        
        public bool TrySubstractFood(int amount)
        {
            if (Food < amount)
                return false;

            Food -= amount;

            OnFoodChanged?.Invoke(Food);
            return true;
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