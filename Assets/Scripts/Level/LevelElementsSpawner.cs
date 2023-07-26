using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using Zenject;
using UnityTools;

namespace CapybaraAdventure.Level
{
    public class LevelElementsSpawner : MonoBehaviour
    {
        [Header("Element Prefabs")]
        [SerializeField] private Food _foodPrefab;
        [SerializeField] private SimpleChest _chestPrefab;
        [SerializeField] private SwordChest _swordChestPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _parent;

        [Header("Chest Spawn Settings")]
        [SerializeField, Range(0, 101)] private int _swordChestSpawnChance = 30;
 
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false)
                    return;

                bool isSwordChestSpawn = Tools.GetChance(_swordChestSpawnChance);
                Chest chest = isSwordChestSpawn == true
                    ? SpawnSwordChest()
                    : SpawnSimpleChest();

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

        private Chest SpawnSimpleChest()
        {
            Chest chest = _diContainer
                    .InstantiatePrefabForComponent<Chest>(_chestPrefab);

            return chest;
        }

        private Chest SpawnSwordChest()
        {
            Chest chest = NightPool.Spawn(_swordChestPrefab);
            return chest;
        }
    }
}