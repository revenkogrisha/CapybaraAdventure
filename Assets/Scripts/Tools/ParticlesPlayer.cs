using NTC.Global.Pool;
using UnityEngine;

namespace CapybaraAdventure.Other
{
    public class ParticlesPlayer : MonoBehaviour
    {
        private const float JumpParticlesDespawnDelay = 1f;

        [SerializeField] private ParticleSystem _particlesPrefab;
        [Tooltip("The transform's pos is used to determine the spawn position")]
        [SerializeField] private Transform _spawnTransform;

        private ParticleSystem _particles;
        private bool _areParticlesPlaying;

        public void SpawnTillDuration()
        {
            if (_areParticlesPlaying == true)
                return;

            _areParticlesPlaying = true;

            Vector3 spawnPosition = _spawnTransform.position;
            _particles = NightPool.Spawn(_particlesPrefab, spawnPosition);

            _particles.Play();
            
            Invoke(nameof(Despawn), JumpParticlesDespawnDelay);
        }

        private void Despawn()
        {
            NightPool.Despawn(_particles);
            _areParticlesPlaying = false;
        }
    }
}
