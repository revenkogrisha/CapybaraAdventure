using System.Collections.Generic;
using UnityEngine;
using NTC.Global.Pool;
using Random = UnityEngine.Random;

namespace CapybaraAdventure.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        public const float PlatformLength = 20f;
        private const int PlatformsAmountToGenerate = 5;
        private const float PlatformY = -3f;

        [SerializeField] private Transform _platformParent;
        [SerializeField] private Platform[] _platforms;

        private readonly Vector2 _startPoint = Vector2.zero;
        private readonly Queue<Platform> _platformsOnLevel = new();
        private float _lastGeneratedPlatformX = 0f;

        private string PlatformName => $"Platform №{_lastGeneratedPlatformX / PlatformLength}";

        private void Awake()
        {
            _lastGeneratedPlatformX = _startPoint.y;
        }
        
        public void GenerateDefaultAmount()
        {
            for (var i = 0; i < PlatformsAmountToGenerate; i++)
                SpawnRandomPlatform();
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
            var position = new Vector3(_lastGeneratedPlatformX, PlatformY);
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