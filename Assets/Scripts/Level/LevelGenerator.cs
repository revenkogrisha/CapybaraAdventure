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

        [SerializeField] private Transform _parent;
        [Header("Prefabs")]
        [SerializeField] private Food _foodPrefab;
        [SerializeField] private Chest _chestPrefab;
        [Header("Settings:")]
        [SerializeField] private int _platformsAmountToGenerate = 5;
        [SerializeField] private float _XstartPoint = 0f;
        [SerializeField] private float _platformsY = -2f;
        [SerializeField] private int _specialPlatformSequentialNumber = 4;
        [Header("Platforms:")]
        [SerializeField] private SimplePlatform _startPlatform;
        [SerializeField] private SimplePlatform[] _simplePlatforms;
        [SerializeField] private SpecialPlatform[] _specialPlatforms;

        private DiContainer _diContainer;
        private CollectablesSpawner _collectablesSpawner;
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

                float GetLevelMidPointX(float heroX)
                {
                    Platform oldestPlatform = _platformsOnLevel.Peek();
                    float oldestPlatformX = oldestPlatform.transform.position.x;
                    return (_lastGeneratedPlatformX + oldestPlatformX) / 2f;
                }
            }
        }

        private void Awake()
        {
            _collectablesSpawner = new(
                _diContainer,
                _foodPrefab,
                _chestPrefab,
                _parent);

            _lastGeneratedPlatformX = _XstartPoint;

            StartCoroutine(CheckPlayerPosition());
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void SpawnStartPlatform() => GeneratePlatform(_startPlatform);
        
        public void GenerateDefaultAmount()
        {
            for (var i = 0; i < _platformsAmountToGenerate; i++)
                GenerateRandomPlatform();
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
                    GenerateRandomPlatform();
                }

                yield return new WaitForSeconds(HeroPositionCheckFrequencyInSeconds);
            }
        }

        private void GenerateRandomPlatform()
        {
            Platform randomPlatform = IsNowSpecialPlatformTurn
                ? GetRandomPlatform(_specialPlatforms)
                : GetRandomPlatform(_simplePlatforms);

            GeneratePlatform(randomPlatform);
        }

        private T GetRandomPlatform<T>(T[] platformPrefabs)
            where T : Platform
        {
            int random = Random.Range(0, platformPrefabs.Length);
            return platformPrefabs[random];
        }

        private void GeneratePlatform(Platform platformPrefab)
        {
            Platform platform = SpawnPlatform(platformPrefab);
            platform.transform.SetParent(_parent);
            platform.name = PlatformName;

            SpawnCollectables(platform);

            _lastGeneratedPlatformX += PlatformLength;
            _platformNumber++;
            _platformsOnLevel.Enqueue(platform);
        }

        private Platform SpawnPlatform(Platform platformPrefab)
        {
            var position = new Vector3(_lastGeneratedPlatformX, _platformsY);
            Platform platform = NightPool.Spawn(
                platformPrefab,
                position,
                Quaternion.identity);
            
            return platform;
        }

        private void SpawnCollectables(Platform platform)
        {
            _collectablesSpawner.SpawnFood(platform);
            _collectablesSpawner.SpawnChests(platform);
        }

        private void DespawnOldestPlatform()
        {
            Platform oldestPlatformInGame = _platformsOnLevel.Dequeue();
            NightPool.Despawn(oldestPlatformInGame);
        }
    }
}