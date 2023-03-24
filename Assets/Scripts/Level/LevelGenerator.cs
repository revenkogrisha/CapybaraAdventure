using IEnumerator = System.Collections.IEnumerator;
using System.Collections.Generic;
using UnityEngine;
using NTC.Global.Pool;
using System;
using CapybaraAdventure.Player;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        public const float PlatformLength = 20f;
        private const int PlatformsAmountToGenerate = 5;
        private const float PlatformY = -3f;
        public const float HeroPositionCheckFrequencyInSeconds = 1.5f;

        [SerializeField] private Transform _platformParent;
        [SerializeField] private Platform[] _platforms;

        private Transform _heroTransform;
        private bool _heroIsInitialized = false;
        private readonly Vector2 _startPoint = Vector2.zero;
        private readonly Queue<Platform> _platformsOnLevel = new();
        private float _lastGeneratedPlatformX = 0f;

        private string PlatformName => $"Platform №{_lastGeneratedPlatformX / PlatformLength}";

        private void Awake()
        {
            _lastGeneratedPlatformX = _startPoint.y;

            StartCoroutine(CheckPlayerPosition());
        }
        
        public void GenerateDefaultAmount()
        {
            for (var i = 0; i < PlatformsAmountToGenerate; i++)
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
                if (_heroIsInitialized == false)
                    yield return 
                        new WaitUntil(() => _heroIsInitialized == true);

                var heroPosition = _heroTransform.position;
                var heroX = heroPosition.x;

                var oldestPlatform = _platformsOnLevel.Peek();
                var oldestPlatformX = oldestPlatform.transform.position.x;

                var midPointX = (_lastGeneratedPlatformX + oldestPlatformX) / 2f;
                if (midPointX < heroX)
                {
                    DespawnOldestPlatform();
                    SpawnRandomPlatform();
                }

                yield return new WaitForSeconds(HeroPositionCheckFrequencyInSeconds);
            }

        }

        private void SpawnRandomPlatform()
        {
            var randomPlatform = GetRandomPlatform();
            SpawnPlatform(randomPlatform);
        }

        private Platform GetRandomPlatform()
        {
            var random = Random.Range(0, _platforms.Length);
            return _platforms[random];
        }

        private void SpawnPlatform(Platform platform)
        {
            var position = new Vector3((float)_lastGeneratedPlatformX, PlatformY);
            var platformInGame = NightPool.Spawn(
                platform,
                position,
                Quaternion.identity);
            
            platformInGame.transform.parent = _platformParent;
            platformInGame.name = PlatformName; 

            _lastGeneratedPlatformX += PlatformLength;

            _platformsOnLevel.Enqueue(platformInGame);
        }

        private void DespawnOldestPlatform()
        {
            var oldestPlatformInGame = _platformsOnLevel.Dequeue();
            NightPool.Despawn(oldestPlatformInGame);
        }
    }
}