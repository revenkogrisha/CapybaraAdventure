using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class LevelPlaythrough
    {
        public struct Reward
        {
            public RewardType Type;
            public ResourceType Resource;
            public int Amount;

            public Reward(RewardType type, ResourceType resource, int amount)
            {
                Type = type;
                Resource = resource;
                Amount = amount;
            }
        }
        
        public enum RewardType
        {   
            Collected,
            Reward,
            Defeated
        }
        
        public enum ResourceType
        {   
            Coins,
            Food
        }
        
        public int CoinsCollected { get; set; }
        public int FoodCollected { get; set; }
        public int CoinsRewarded { get; set; }
        public int FoodRewarded { get; set; }

        public Reward[] GetRewards()
        {
            Reward[] rewards = 
            {
                new(RewardType.Collected, ResourceType.Coins, CoinsCollected),
                new(RewardType.Collected, ResourceType.Food, FoodCollected),
                new(RewardType.Reward, ResourceType.Coins, CoinsRewarded),
                new(RewardType.Reward, ResourceType.Food, FoodRewarded),
            };
            return rewards;
        }
        
        public void Clear()
        {
            CoinsCollected = default;
            FoodCollected = default;
            CoinsRewarded = default;
            FoodRewarded = default;
        }

        public void LogAll()
        {
            Debug.Log($"[LP] | CoinsCollected: {CoinsCollected}");
            Debug.Log($"[LP] | FoodCollected: {FoodCollected}");
            Debug.Log($"[LP] | CoinsRewarded: {CoinsRewarded}");
            Debug.Log($"[LP] | FoodRewarded: {FoodRewarded}");
        }
    }
}