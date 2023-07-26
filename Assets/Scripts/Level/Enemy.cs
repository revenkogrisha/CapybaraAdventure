using UnityEngine;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Level
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ParticlesPlayer _deathParticles;

        public void PerformDeath()
        {
            _deathParticles.SpawnTillDuration();
            Destroy(gameObject);
        }

    }
}
