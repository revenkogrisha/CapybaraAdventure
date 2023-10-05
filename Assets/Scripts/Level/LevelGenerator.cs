using System;
using System.Collections.Generic;
using UnityEngine;
using NTC.Global.Pool;
using CapybaraAdventure.Player;
using UnityTools;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace CapybaraAdventure.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float HeroPositionCheckFrequency = 1.5f;
        private const int SpecialPlatformSequentialNumber = 4;
        private const int LocationChangeSequentialNumber = 10;
        private const int QuestPlatformSequentialNumber = 16;
        private const float BackgroundLerpDuration = 3.5f;

        [Header("Components")]
        [SerializeField] private LevelElementsSpawner _elementsSpawner;

        [Header("Prefabs")]
        [SerializeField] private Food _foodPrefab;
        [SerializeField] private Chest _chestPrefab;
        [SerializeField] private Enemy _enemyPrefab;

        [Header("Platform Generation Settings")]
        [SerializeField] private Transform _platformsParent;
        [SerializeField] private int _platformsAmountToGenerate = 5;
        [SerializeField] private float _XstartPoint = 0f;
        [SerializeField] private float _platformsY = -2f;

        [Header("Platforms")]
        [SerializeField] private SimplePlatform _startPlatform;
        [SerializeField] private SpecialPlatform _questPlatform;
        [SerializeField] private Location[] _locations;

        private Camera _camera;
        private Transform _heroTransform;
        private bool _heroIsInitialized = false;
        private readonly Queue<Platform> _platformsOnLevel = new();
        private int _platformNumber = 0;
        private float _lastGeneratedPlatformX = 0f;
        private int _locationNumber = 0;
        private Color _defaultBackground;
        private bool _isQuestCompleted = false;
        private CancellationToken _cancellationToken;

        private string PlatformName => $"Platform №{_platformNumber}";
        private Location CurrentLocation => _locations[_locationNumber];
        public float QuestPlatformXPosition => Platform.Length * QuestPlatformSequentialNumber;
        private float HeroX => _heroTransform.position.x;
        private bool IsNowSpecialPlatformTurn 
        {
            get
            {
                return _platformNumber % SpecialPlatformSequentialNumber == 0
                && _platformNumber > 0;
            }
        }

        private bool IsNowQuestPlatformTurn
        {
            get
            {
                return _platformNumber % QuestPlatformSequentialNumber == 0
                && _platformNumber > 0 && _isQuestCompleted == false;
            }
        }

        private bool IsNowLocationChangeTurn 
        {
            get
            {
                return _platformNumber % LocationChangeSequentialNumber == 0
                && _platformNumber > 0;
            }
        }

        private bool IsLevelMidPointXLessHeroX
        {
            get
            {
                float midPointX = GetLevelMidPointX();
                return midPointX < HeroX;

                float GetLevelMidPointX()
                {
                    Platform oldestPlatform = _platformsOnLevel.Peek();
                    float oldestPlatformX = oldestPlatform.transform.position.x;
                    return (_lastGeneratedPlatformX + oldestPlatformX) / 2f;
                }
            }
        }

        public event Action<float> OnHeroXPositionUpdated;

        private void Awake()
        {
            _lastGeneratedPlatformX = _XstartPoint;
            _camera = Camera.main;
            _cancellationToken = this.GetCancellationTokenOnDestroy();

            _defaultBackground = _camera.backgroundColor;
        }

        private void Start()
        {
            ChangeBackgroundColor();

            CheckPlayerPosition().Forget();
        }

        public void SpawnStartPlatform()
        {
            GeneratePlatform(_startPlatform);
        }
        
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

        private async UniTask CheckPlayerPosition()
        {
            while (this != null)
            {
                await UniTask.WaitUntil(() => _heroIsInitialized == true);

                OnHeroXPositionUpdated?.Invoke(HeroX);

                if (IsLevelMidPointXLessHeroX == true) 
                {
                    DespawnOldestPlatform();
                    GenerateRandomPlatform();
                }

                TimeSpan delay = TimeSpan.FromSeconds(HeroPositionCheckFrequency);
                await UniTask.Delay(delay);
            }
        }

        private void GenerateRandomPlatform()
        {
            if (IsNowLocationChangeTurn == true)
            {
                ChangeLocation();
                ChangeBackgroundColor();
            }
 
            Location currentLocation = CurrentLocation;
            SimplePlatform[] simplePlatforms = currentLocation.SimplePlatforms;
            SpecialPlatform[] specialPlatforms = currentLocation.SpecialPlatforms;

            Platform randomPlatform;
            if (IsNowQuestPlatformTurn == true)
            {
                randomPlatform = _questPlatform;
                _isQuestCompleted = true;
            }
            else
            {
                randomPlatform = IsNowSpecialPlatformTurn
                    ? GetRandomPlatform(specialPlatforms)
                    : GetRandomPlatform(simplePlatforms);
            }

            GeneratePlatform(randomPlatform);
        }

        private void ChangeLocation()
        {
            _locationNumber++;
                
            int locationsLength = _locations.Length;
            if (_locationNumber >= locationsLength)
                _locationNumber = _locations.GetRandomIndex();
        }

        private void ChangeBackgroundColor()
        {
            Location currentLocation = CurrentLocation;
            Color newBackground;
            if (currentLocation.UseDefaultBackground == true)
                newBackground = _defaultBackground;
            else
                newBackground = currentLocation.BackgroundColor;

            LerpBackground(newBackground, _cancellationToken).Forget();
        }

        private async UniTask LerpBackground(Color newBackground, CancellationToken token)
        {
            float elapsedTime = 0;
            Color currentBackground = _camera.backgroundColor;
            while (elapsedTime < BackgroundLerpDuration)
            {
                float time = elapsedTime / BackgroundLerpDuration;
                _camera.backgroundColor = Color.Lerp(currentBackground, newBackground, time);

                elapsedTime += Time.deltaTime;
                await UniTask.NextFrame(token);
            }
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
            platform.transform.SetParent(_platformsParent);
            platform.name = PlatformName;

            SpawnElements(platform);

            _lastGeneratedPlatformX += Platform.Length;
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

        private void SpawnElements(Platform platform)
        {
//  Refactor spawner with no monobeh, create a config class
//  Refactor spawner with no monobeh, create a config class
//  Refactor spawner with no monobeh, create a config class
//  Refactor spawner with no monobeh, create a config class
            _elementsSpawner.SpawnFood(platform);
            _elementsSpawner.SpawnChests(platform);
            _elementsSpawner.SpawnTreasureChests(platform);
            _elementsSpawner.SpawnEnemies(platform);
        }

        private void DespawnOldestPlatform()
        {
            Platform oldestPlatformInGame = _platformsOnLevel.Dequeue();
            NightPool.Despawn(oldestPlatformInGame);
        }
    }
}