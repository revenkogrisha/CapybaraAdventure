using UnityEngine;
using Zenject;
using System;
using CapybaraAdventure.Game;
using NTC.Global.Pool;

namespace CapybaraAdventure.Player
{
    public class Hero : MonoBehaviour
    {
        private const float HeightTestRadius = 0.05f;

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private AnimationCurve _jumpCurve;
        [Tooltip("Optional")]
        [SerializeField] private HeroJumpParticlesPlayer _jumpParticlesPlayer;
        
        private HeroAnimator _heroAnimator;
        private PauseManager _pauseManager;
        private bool _isDead;
        private ParticleSystem _particles;

        public HeroJump Jump { get; private set; }
        public bool IsPaused => _pauseManager.IsPaused;
        public bool ShouldPlayParticles => _jumpParticlesPlayer != null;

        public event Action OnDeath;
        public event Action OnFoodEaten;

        #region MonoBehaviour

        private void Awake()
        {
            _isDead = false;

            var heightTestService = new HeightCheckService(_collider, _ground, HeightTestRadius);
            Jump = new HeroJump(_rigidBody2D, _jumpCurve, heightTestService,_duration);
            _heroAnimator = new HeroAnimator(_animator);
        }

        private void OnEnable()
        {
            Jump.OnJumped += AnimateJump;
            Jump.OnLanded += AnimateJumpEnd;

            if (ShouldPlayParticles == true)
                Jump.OnJumped += SpawnParticles;
        }

        private void OnDisable()
        {
            Jump.OnJumped -= AnimateJump;
            Jump.OnLanded -= AnimateJumpEnd;

            if (ShouldPlayParticles == true)
                Jump.OnJumped -= SpawnParticles;
        }

        private void Update()
        {
            if (IsPaused)
                return;

            Jump.TryJump();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsPaused
                || _isDead)
                return;
                
            HandleDeadlyObjectCollision(other);
            HandleFoodCollision(other);
        }

        #endregion

        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        private void AnimateJump() => _heroAnimator.SetAsJumping();
        
        private void AnimateJumpEnd() => _heroAnimator.EndJumping();

        private void SpawnParticles() =>
             _jumpParticlesPlayer.SpawnTillDuration();

        private void HandleDeadlyObjectCollision(Collider2D other)
        {
            UnityTools.Tools
                .InvokeIfNotNull<DeadlyForPlayerObject>(other, PerformDeath);
        }

        private void HandleFoodCollision(Collider2D other)
        {
            UnityTools.Tools
                .InvokeIfNotNull<Food>(other, EatFood);
        }

        private void PerformDeath()
        {
            _rigidBody2D.simulated = false;
            _isDead = true;
            OnDeath?.Invoke();
        }

        private void EatFood(Food food)
        {
            OnFoodEaten?.Invoke();
            NightPool.Despawn(food);
        }
    }
}