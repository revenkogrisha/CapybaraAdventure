using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using UnityTools;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Level
{
    public class LevelElementsSpawner : MonoBehaviour
    {
        private const int SwordChestsLimit = 2;

        [Header("Element Prefabs")]
        [SerializeField] private Food _foodPrefab;
        [SerializeField] private SimpleChest _chestPrefab;
        [SerializeField] private TreasureChest _treasureChestPrefab;
        [SerializeField] private SwordChest _swordChestPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _parent;

        [Header("Chest Spawn Settings")]
        [SerializeField, Range(0, 101)] private int _swordChestSpawnChance = 20;
 
        private int _swordChestsAmount = 0;

        private bool CanSpawnSwordChest => SwordChestsLimit > _swordChestsAmount;

        public void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                bool isSwordChestSpawn = Tools.GetChance(_swordChestSpawnChance);
                Chest chest = isSwordChestSpawn == true && CanSpawnSwordChest == true
                    ? SpawnSwordChest()
                    : SpawnSimpleChest();

                Vector3 position = marker.Position;
                marker.SpawnedObject = chest.gameObject;
                chest.transform.SetParent(platformInGame.transform);
                chest.transform.position = position;
            }
        }

        public void SpawnTreasureChests(Platform platformInGame)
        {
            TreasureChestSpawnMarker[] markers = platformInGame.TreasureChestMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                TreasureChest chest = DIContainerRef.Container
                    .InstantiatePrefabForComponent<TreasureChest>(_treasureChestPrefab);

                Vector3 position = marker.Position;
                marker.SpawnedObject = chest.gameObject;
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
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                Vector3 position = marker.Position;
                Food food = NightPool.Spawn(_foodPrefab, position, Quaternion.identity);

                marker.SpawnedObject = food.gameObject;
                food.transform.SetParent(platformInGame.transform);
            }
        }

        public void SpawnEnemies(Platform platformInGame)
        {
            EnemySpawnMarker[] markers = platformInGame.EnemyMarkers;
            foreach (var marker in markers)
            {
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                Vector3 position = marker.Position;
                Enemy enemy = NightPool.Spawn(_enemyPrefab, position);

                marker.SpawnedObject = enemy.gameObject;
                enemy.transform.SetParent(platformInGame.transform);
            }
        }

        private Chest SpawnSimpleChest()
        {
            Chest chest = DIContainerRef.Container
                    .InstantiatePrefabForComponent<Chest>(_chestPrefab);

            return chest;
        }

        private Chest SpawnSwordChest()
        {
            Chest chest = NightPool.Spawn(_swordChestPrefab);
            _swordChestsAmount++;
            return chest;
        }
    }
}