using UnityEngine;
using Zenject;
using System;
using CapybaraAdventure.Game;
using NTC.Global.Pool;
using UnityTools;

namespace CapybaraAdventure.Player
{
    public class Hero : MonoBehaviour, IPauseHandler
    {
        private const float HeightTestRadius = 0.05f;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private Collider2D _collider;
        [Tooltip("Optional (Can be null)")]
        [SerializeField] private ParticlesPlayer _jumpParticlesPlayer;

        [Header("Jump Settings")]
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private AnimationCurve _jumpCurve;

        [Header("Audio")]
        [SerializeField] private HeroAudioPlayer _audioPlayer;
        
        private HeroAnimator _heroAnimator;
        private PauseManager _pauseManager;
        private bool _isDead;
        private bool _hasJumped;

        public HeroJump Jump { get; private set; }
        public bool IsPaused => _pauseManager.IsPaused;
        public bool ShouldPlayParticles => _jumpParticlesPlayer != null;

        public event Action OnDeath;
        public event Action OnFoodEaten;

        #region MonoBehaviour

        private void Awake()
        {
            _isDead = false;
            _hasJumped = false;

            var heightTestService = new HeightCheckService(_collider, _ground, HeightTestRadius);
            Jump = new HeroJump(
                _rigidBody2D,
                _jumpCurve,
                heightTestService,
                transform,
                _duration);

            _heroAnimator = new HeroAnimator(_animator);
        }

        private void OnEnable()
        {
            _pauseManager.Register(this);
            Jump.OnJumped += AnimateJump;
            Jump.OnJumped += PlayJumpSound;
            Jump.OnLanded += AnimateJumpEnd;
            Jump.OnLanded += HasLanded;

            if (ShouldPlayParticles == true)
                Jump.OnJumped += SpawnParticles;
        }

        private void OnDisable()
        {
            _pauseManager.Unregister(this);
            Jump.OnJumped -= AnimateJump;
            Jump.OnJumped -= PlayJumpSound;
            Jump.OnLanded -= AnimateJumpEnd;
            Jump.OnLanded -= HasLanded;

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
            HandleChestCollision(other);
        }

        #endregion
        
        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused == true)
                DisableRigidbody();
            else
                EnableRigidbody();
        }

        public void Revive()
        {
            transform.position = Jump.LastJump;
            _isDead = false;
            EnableRigidbody();
        }

        private void HasLanded() => _hasJumped = false;
        
        private void PlayJumpSound()
        {
            if (_hasJumped == true)
                return;

            _hasJumped = true;
            _audioPlayer.PlayJump();
        }

        private void AnimateJump() => _heroAnimator.SetAsJumping();
        
        private void AnimateJumpEnd() => _heroAnimator.EndJumping();

        private void SpawnParticles() =>
             _jumpParticlesPlayer.SpawnTillDuration();

        private void HandleDeadlyObjectCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<DeadlyForPlayerObject>(other, PerformDeath);
        }

        private void HandleFoodCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<Food>(other, EatFood);
        }

        private void HandleChestCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<Chest>(other, OpenChest);
        }

        private void PerformDeath()
        {
            _audioPlayer.PlayGameOver();
            DisableRigidbody();
            _isDead = true;

            OnDeath?.Invoke();
        }

        private void EatFood(Food food)
        {
            _audioPlayer.PlayEat();
            OnFoodEaten?.Invoke();
            NightPool.Despawn(food);
        }

        private void EnableRigidbody()
        {
            _rigidBody2D.simulated = true;
        }

        private void DisableRigidbody()
        {
            _rigidBody2D.simulated = false;
        }

        private void OpenChest<T>(T chest)
            where T : Chest
        {
            _audioPlayer.PlayCollected();
            chest.Open();
        }
    }
}