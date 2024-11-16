using UnityEngine;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Level
{
    public class Enemy : MonoBehaviour
    {
        public const int DefeatRewardCoins = 10;
        private const string DeadEnemy = nameof(DeadEnemy);

        [Header("Components")]
        [SerializeField] private ParticlesPlayer _deathParticles;
        [SerializeField] private MovingObject _movement;

        [Header("Death Settings")]
        [SerializeField] private float _disappearDelayInSeconds = 1.2f;

        private int _deathLayer;

        private void Awake()
        {
            _deathLayer = LayerMask.NameToLayer(DeadEnemy);
        }

        public void GetKicked(Vector2 force)
        {
            Rigidbody2D rigidbody2D = _movement.Rigidbody2D;
            rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }

        public void PerformDeath()
        {
            _movement.BecomePhysical();
            gameObject.layer = _deathLayer;
            Invoke(nameof(Disappear), _disappearDelayInSeconds);
        }

        private void Disappear()
        {
            _deathParticles.SpawnTillDuration();
            Destroy(gameObject);
        }

    }
}
