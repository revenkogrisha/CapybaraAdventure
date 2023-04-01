using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Level
{
    public class FoodSpawner
    {
        private Food _foodPrefab;

        public FoodSpawner(Food foodPrefab)
        {
            _foodPrefab = foodPrefab;
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
            var position = marker.Position;
            NightPool.Spawn(_foodPrefab, position, Quaternion.identity);
        }
    }
}
