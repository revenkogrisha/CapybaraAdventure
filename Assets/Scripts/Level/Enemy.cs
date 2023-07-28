using UnityEngine;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Level
{
    public class Enemy : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ParticlesPlayer _deathParticles;
        [SerializeField] private MovingObject _movement;

        [Header("Death Settings")]
        [SerializeField] private float _disappearDelayInSeconds = 1f;

        public void GetKicked(Vector2 force)
        {
            Rigidbody2D rigidbody2D = _movement.Rigidbody2D;
            rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

        public void PerformDeath()
        {
            _movement.BecomePhysical();
            Invoke(nameof(Disappear), _disappearDelayInSeconds);
        }

        private void Disappear()
        {
            _deathParticles.SpawnTillDuration();
            Destroy(gameObject);
        }
    }
}
