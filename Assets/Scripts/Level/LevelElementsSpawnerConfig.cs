using CapybaraAdventure.Player;
using UnityEngine;

namespace CapybaraAdventure.Level
{
    [CreateAssetMenu(
        fileName = "LevelElementsSpawnerConfig",
        menuName = "Configs/Level Elements Spawner Config")]
    public class LevelElementsSpawnerConfig : ScriptableObject
    {
        [Header("Element Prefabs")]
        [SerializeField] public Food _foodPrefab;
        [SerializeField] public SimpleChest _chestPrefab;
        [SerializeField] public TreasureChest _treasureChestPrefab;
        [SerializeField] public SwordChest _swordChestPrefab;
        [SerializeField] public Enemy _enemyPrefab;


        [Header("Chest Spawn Settings")]
        [SerializeField, Range(0, 101)] private int _swordChestSpawnChance = 20;

        public Food FoodPrefab => _foodPrefab;
        public SimpleChest ChestPrefab => _chestPrefab;
        public TreasureChest TreasureChestPrefab => _treasureChestPrefab;
        public SwordChest SwordChestPrefab => _swordChestPrefab;
        public Enemy EnemyPrefab => _enemyPrefab;
        public int SwordChestSpawnChance => _swordChestSpawnChance;
    }
}
