using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Level
{
    public class FoodSpawner
    {
        private Food _foodPrefab;
        private Transform _parent;

        public FoodSpawner(Food foodPrefab, Transform parent)
        {
            _foodPrefab = foodPrefab;
            _parent = parent;
        }

        public void SpawnFoodOnMarkers(FoodSpawnMarker[] markers)
        {
            foreach (var marker in markers)
            {
                SpawnFoodOnMarker(marker);
            }
        }

        public void SpawnFoodOnMarker(FoodSpawnMarker marker)
        {
            var chance = marker.SpawnChance;
            int randomChance = Random.Range(0, 101);
            if (chance < randomChance)
                return;

            var position = marker.Position;
            var food = NightPool.Spawn(_foodPrefab, position, Quaternion.identity);

            food.transform.SetParent(_parent);
        }
    }
}
