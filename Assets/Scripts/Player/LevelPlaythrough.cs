using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class LevelPlaythrough
    {
        public int CoinsCollected { get; set; }
        public int FoodCollected { get; set; }
        public int CoinsRewarded { get; set; }
        public int FoodRewarded { get; set; }
        
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