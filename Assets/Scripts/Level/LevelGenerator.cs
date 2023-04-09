using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using Random = UnityEngine.Random;
using IEnumerator = System.Collections.IEnumerator;

namespace CapybaraAdventure.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float PlatformLength = 30f;
        private const float HeroPositionCheckFrequencyInSeconds = 1.5f;

        [SerializeField] private Food _foodPrefab;
        [SerializeField] private Chest _chestPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private int _platformsAmountToGenerate = 5;
        [SerializeField] private float _XstartPoint = 0f;
        [SerializeField] private float _platformsY = -2f;
        [SerializeField] private int _specialPlatformSequentialNumber = 4;
        [SerializeField] private SimplePlatform _startPlatform;
        [SerializeField] private SimplePlatform[] _simplePlatforms;
        [SerializeField] private SpecialPlatform[] _specialPlatforms;

        private DiContainer _diContainer;
        private FoodSpawner _foodSpawner;
        private Transform _heroTransform;
        private bool _heroIsInitialized = false;
        private readonly Queue<Platform> _platformsOnLevel = new();
        private int _platformNumber = 0;
        private float _lastGeneratedPlatformX = 0f;

        private string PlatformName => $"Platform №{_platformNumber}";

        private bool IsNowSpecialPlatformTurn => 
            _platformNumber % _specialPlatformSequentialNumber == 0
            && _platformNumber > 0;

        private bool IsLevelMidPointXLessHeroX
        {
            get
            {
                Vector3 heroPosition = _heroTransform.position;
                float heroX = heroPosition.x;
                float midPointX = GetLevelMidPointX(heroX);
                return midPointX < heroX;
            }
        }

        private void Awake()
        {
            _foodSpawner = new(_foodPrefab, _parent);
            _lastGeneratedPlatformX = _XstartPoint;

            StartCoroutine(CheckPlayerPosition());
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void SpawnStartPlatform() => SpawnPlatform(_startPlatform);
        
        public void GenerateDefaultAmount()
        {
            for (var i = 0; i < _platformsAmountToGenerate; i++)
                SpawnRandomPlatform();
        }

        public void InitHeroTransform(Hero hero)
        {
            if (_heroIsInitialized == true)
                throw new InvalidOperationException("Hero had been already initialized before! You can do it only once.");

            _heroTransform = hero.transform;
            _heroIsInitialized = true;
        }

        private IEnumerator CheckPlayerPosition()
        {
            while (true)
            {
                yield return new WaitUntil(
                    () => _heroIsInitialized == true
                    );

                if (IsLevelMidPointXLessHeroX)
                {
                    DespawnOldestPlatform();
                    SpawnRandomPlatform();
                }

                yield return new WaitForSeconds(HeroPositionCheckFrequencyInSeconds);
            }
        }

        private float GetLevelMidPointX(float heroX)
        {
            Platform oldestPlatform = _platformsOnLevel.Peek();
            float oldestPlatformX = oldestPlatform.transform.position.x;
            return (_lastGeneratedPlatformX + oldestPlatformX) / 2f;
        }

        private void SpawnRandomPlatform()
        {
            Platform randomPlatform = IsNowSpecialPlatformTurn
                ? GetRandomPlatform(_specialPlatforms)
                : GetRandomPlatform(_simplePlatforms);

            SpawnPlatform(randomPlatform);
        }

        private T GetRandomPlatform<T>(T[] platforms)
            where T : Platform
        {
            int random = Random.Range(0, platforms.Length);
            return platforms[random];
        }

        private void SpawnPlatform(Platform platform)
        {
            var position = new Vector3(_lastGeneratedPlatformX, _platformsY);
            Platform platformInGame = NightPool.Spawn(
                platform,
                position,
                Quaternion.identity);

            platformInGame.transform.SetParent(_parent);
            platformInGame.name = PlatformName;

            SpawnFood(platformInGame);
            SpawnChests(platformInGame);

            _lastGeneratedPlatformX += PlatformLength;
            _platformNumber++;
            _platformsOnLevel.Enqueue(platformInGame);
        }

        private void SpawnFood(Platform platformInGame)
        {
            FoodSpawnMarker[] markers = platformInGame.FoodMarkers;
            _foodSpawner.SpawnFoodOnMarkers(markers);
        }

        private void SpawnChests(Platform platformInGame)
        {
            ChestSpawnMarker[] markers = platformInGame.ChestMarkers;
            foreach (var marker in markers)
            {
                Chest chest = _diContainer
                    .InstantiatePrefabForComponent<Chest>(_chestPrefab);

                chest.transform.position = marker.Position;
            }
        }

        private void DespawnOldestPlatform()
        {
            Platform oldestPlatformInGame = _platformsOnLevel.Dequeue();
            NightPool.Despawn(oldestPlatformInGame);
        }
    }
}