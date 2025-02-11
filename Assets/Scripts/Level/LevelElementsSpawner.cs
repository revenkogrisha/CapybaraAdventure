using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using UnityTools;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Level
{
    public class LevelElementsSpawner
    {
        private const int SwordChestsLimit = 2;
 
        private readonly LevelElementsSpawnerConfig _config;
        private int _swordChestsAmount = 0;

        private bool CanSpawnSwordChest => SwordChestsLimit > _swordChestsAmount;

        public LevelElementsSpawner(LevelElementsSpawnerConfig config)
        {
            _config = config;
        }

        public void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                if (marker == null)
                    return;
                
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                bool isSwordChestSpawn = Tools.GetChance(_config.SwordChestSpawnChance);
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
                if (marker == null)
                    return;
                
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                TreasureChest chest = DIContainerRef.Container
                    .InstantiatePrefabForComponent<TreasureChest>(_config.TreasureChestPrefab);

                Vector3 position = marker.Position;
                marker.SpawnedObject = chest.gameObject;
                chest.transform.SetParent(platformInGame.transform);
                chest.transform.position = position;
            }
        }
        public void SpawnMelonChests(Platform platformInGame)
        {
            MelonChestSpawnMarker[] markers = platformInGame.MelonChestMarkers;
            foreach (var marker in markers)
            {
                if (marker == null)
                    return;
                
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                MelonChest chest = DIContainerRef.Container
                    .InstantiatePrefabForComponent<MelonChest>(_config.MelonChestPrefab);

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
                if (marker == null)
                    return;
                
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                Vector3 position = marker.Position;
                Food food = NightPool.Spawn(_config.FoodPrefab, position, Quaternion.identity);

                marker.SpawnedObject = food.gameObject;
                food.transform.SetParent(platformInGame.transform);
            }
        }

        public void SpawnEnemies(Platform platformInGame)
        {
            EnemySpawnMarker[] markers = platformInGame.EnemyMarkers;
            foreach (var marker in markers)
            {
                if (marker == null)
                    return;
                
                bool isChanceSucceeded = Tools.GetChance(marker.SpawnChance);
                if (isChanceSucceeded == false || marker.HasActiveObject == true)
                    continue;

                Vector3 position = marker.Position;
                Enemy enemy = NightPool.Spawn(_config.EnemyPrefab, position);

                marker.SpawnedObject = enemy.gameObject;
                enemy.transform.SetParent(platformInGame.transform);
            }
        }

        private Chest SpawnSimpleChest()
        {
            Chest chest = DIContainerRef.Container
                    .InstantiatePrefabForComponent<Chest>(_config.ChestPrefab);

            return chest;
        }

        private Chest SpawnSwordChest()
        {
            Chest chest = NightPool.Spawn(_config.SwordChestPrefab);
            _swordChestsAmount++;
            return chest;
        }
    }
}