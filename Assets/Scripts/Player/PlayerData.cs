using System;
using CapybaraAdventure.Level;
using CapybaraAdventure.Save;
using CapybaraAdventure.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        private readonly LevelPlaythrough _levelPlaythrough;
        private const float MaxDistanceUpgradeLimit = 22f;
        private const float MaxDistanceUpgradeIncrease = 1f;
        private const float LerpSpeedDecrease = 0.08f;
        private const float FoodBonusIncrease = 0.125f;
        public const float FoodBonusLimit = 1f;
        
        // NEW
        public const float MaxDistanceConst = 21f;
        public const float FoodBonusConst = 1.125f;

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
            // FoodBonus = data.FoodBonus;
            
            // NEW
            MaxDistance = MaxDistanceConst;
            FoodBonus = FoodBonusConst;
            // –––
            
            DistanceUpgradeCost = data.DistanceUpgradeCost;
            FoodUpgradeCost = data.FoodUpgradeCost;
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