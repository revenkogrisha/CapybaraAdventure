using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using Zenject;

namespace CapybaraAdventure.Level
{
    public class LevelElementsSpawner
    {
        private readonly DiContainer _diContainer;
        private readonly Food _foodPrefab;
        private readonly Chest _chestPrefab;
        private readonly Transform _parent;

        public LevelElementsSpawner(
            DiContainer diContainer,
            Food foodPrefab,
            Chest chestPrefab,
            Transform parent)
        {
            _diContainer = diContainer;
            _foodPrefab = foodPrefab;
            _chestPrefab = chestPrefab;
            _parent = parent;
        }

        public void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                Chest chest = _diContainer
                    .InstantiatePrefabForComponent<Chest>(_chestPrefab);

                chest.transform.SetParent(platformInGame.transform);
                chest.transform.position = marker.Position;
            }
        }

        public void SpawnFood(Platform platformInGame)
        {
            FoodSpawnMarker[] markers = platformInGame.FoodMarkers;
            SpawnFoodOnMarkers(markers);
        }

        public void SpawnFoodOnMarkers(FoodSpawnMarker[] markers)
        {
            foreach (var marker in markers)
                SpawnFoodOnMarker(marker);
        }

        public void SpawnFoodOnMarker(FoodSpawnMarker marker)
        {
            int chance = marker.SpawnChance;
            int randomChance = Random.Range(0, 101);
            if (chance < randomChance)
                return;

            Vector3 position = marker.Position;
            Food food = NightPool.Spawn(_foodPrefab, position, Quaternion.identity);

            food.transform.SetParent(_parent);
        }
    }
}
