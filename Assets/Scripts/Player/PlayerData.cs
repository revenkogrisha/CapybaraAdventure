using System;
using CapybaraAdventure.Level;
using CapybaraAdventure.Save;
using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        private const float MaxDistanceUpgradeLimit = 22f;
        private const float MaxDistanceUpgradeIncrease = 1f;
        private const float LerpSpeedDecrease = 0.08f;
        private const float FoodBonusIncrease = 0.0625f;
        private const int MaxLevel = 20;
        
        // NEW
        public const float MaxDistanceConst = 21f;
        public const float FoodBonusConst = 1.125f;

        private readonly LevelPlaythrough _levelPlaythrough;
        
        public int Coins { get; private set; }
        public int Food { get; private set; }
        public float MaxDistance { get; private set; }
        public float FoodBonus { get; private set; }
        public int CommonUpgradeCost { get; private set; }
        public int DistanceUpgradeCost { get; private set; }
        public bool IsCutsceneWatched { get; set; }

        public float FinalLerpSpeedDecrease
        {
            get => LerpSpeedDecrease + (LerpSpeedDecrease * FoodBonus);
        }

        public int CurrentLevel => Mathf.RoundToInt((FoodBonus - 0.25f) / FoodBonusIncrease) + 1;
        public bool CanUpgrade => CurrentLevel < MaxLevel;

        public event Action<int> OnCoinsChanged;
        public event Action<int> OnFoodChanged;

        [Inject]
        private PlayerData(LevelPlaythrough levelPlaythrough)
        {
            _levelPlaythrough = levelPlaythrough;
        }

        public void LoadData(SaveData data)
        {
            Coins = data.Coins;
            Food = data.Food;
            // MaxDistance = data.MaxDistance;
            FoodBonus = data.FoodBonusNEW;
            
            // NEW
            MaxDistance = MaxDistanceConst;
            // FoodBonus = FoodBonusConst;
            // –––
            
            DistanceUpgradeCost = data.DistanceUpgradeCost;
            CommonUpgradeCost = data.CommonUpgradeCost;
            IsCutsceneWatched = data.IsCutsceneWatched;
        }

        public void AddEnemyDefeatedReward()
        {
            int amount = Enemy.DefeatRewardCoins;
            Coins += amount;
            _levelPlaythrough.CoinsDefeated += amount;
            
            OnCoinsChanged?.Invoke(amount);
        }

        public void AddSimpleChestCoins()
        { 
            int amount = SimpleChest.CoinsInsideAmount;
            Coins += amount;
            _levelPlaythrough.CoinsCollected += amount;
            
            OnCoinsChanged?.Invoke(Coins);
        }

        public void AddTreasureChestReward()
        { 
            int coins = TreasureChest.CoinsInsideAmount;
            int food = TreasureChest.FoodInsideAmount;
            Coins += coins;
            Food += food;
            
            _levelPlaythrough.CoinsRewarded += coins;
            _levelPlaythrough.FoodRewarded += food;

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
            Food += amount;
            _levelPlaythrough.FoodCollected += amount;
            
            OnFoodChanged?.Invoke(Food);
        }
        
        public void AddPurchasedMelons(int amount)
        { 
            Food += amount;
            
            OnFoodChanged?.Invoke(Food);
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
            CommonUpgradeCost = block.Cost;

            float increased = FoodBonus += FoodBonusIncrease;
            
            int level = Mathf.RoundToInt((increased - 0.25f) / FoodBonusIncrease) + 1;
            if (MaxLevel >= level)
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