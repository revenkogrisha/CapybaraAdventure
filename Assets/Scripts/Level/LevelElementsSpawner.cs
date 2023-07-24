using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using Zenject;
using UnityTools;

namespace CapybaraAdventure.Level
{
    public class LevelElementsSpawner
    {
        private readonly DiContainer _diContainer;
        private readonly Food _foodPrefab;
        private readonly Chest _chestPrefab;
        private readonly Enemy _enemyPrefab;
        private readonly Transform _parent;

        public LevelElementsSpawner(
            DiContainer diContainer,
            Food foodPrefab,
            Chest chestPrefab,
            Enemy enemyPrefab,
            Transform parent)
        {
            _diContainer = diContainer;
            _foodPrefab = foodPrefab;
            _chestPrefab = chestPrefab;
            _enemyPrefab = enemyPrefab;
            _parent = parent;
        }

        public void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false)
                    return;

                Chest chest = _diContainer
                    .InstantiatePrefabForComponent<Chest>(_chestPrefab);

                Vector3 position = marker.Position;
                chest.transform.SetParent(platformInGame.transform);
                chest.transform.position = position;
            }
        }

        public void SpawnFood(Platform platformInGame)
        {
            FoodSpawnMarker[] markers = platformInGame.FoodMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false)
                    return;

                Vector3 position = marker.Position;
                Food food = NightPool.Spawn(_foodPrefab, position, Quaternion.identity);

                food.transform.SetParent(_parent);
            }
        }

        public void SpawnEnemies(Platform platformInGame)
        {
            EnemySpawnMarker[] markers = platformInGame.EnemyMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false)
                    return;

                Vector3 position = marker.Position;
                Enemy enemy = NightPool.Spawn(_enemyPrefab, position);

                enemy.transform.SetParent(platformInGame.transform);
            }
        }
    }
}