using System;
using UnityEngine;
using Zenject;
using NTC.Global.Pool;
using UnityTools;
using CapybaraAdventure.Game;
using CapybaraAdventure.Level;
using CapybaraAdventure.Other;
using Core.Audio;
using Core.Player;

namespace CapybaraAdventure.Player
{
    public class Hero : MonoBehaviour, IPhysicalObject, IPauseHandler
    {
        private const float HeightTestRadius = 0.05f;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private Collider2D _collider;
        [Tooltip("Optional (Can be null)")]
        [SerializeField] private ParticlesPlayer _jumpParticlesPlayer;
        [SerializeField] private HeroSkinSetter _heroSkinSetter;

        [Header("Fight")]
        [SerializeField] private GameObject _sword;
        [SerializeField] private float _kickForce = 8f;

        [Header("Jump Settings")]
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private AnimationCurve _jumpXAccelCurve;
        
        [Header("*NEW* Ground Check Settings")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _groundCheckTransform;
        [SerializeField] private float _groundCheckRadius = 0.35f;

        [Header("Audio")]
        [SerializeField] private HeroAudioPlayer _audioPlayer;
        
        private HeroAnimator _heroAnimator;
        private PauseManager _pauseManager;
        private bool _isDead = false;
        private bool _hasJumped = false;
        private bool _hasSword = false;
        private Enemy _enemyToKick = null;
        private IAudioHandler _audioHandler;

        public HeroJump Jump { get; private set; }
        public bool IsPaused => _pauseManager.IsPaused;
        public bool ShouldPlayParticles => _jumpParticlesPlayer != null;
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => _rigidBody2D;
        public HeroSkinSetter SkinSetter => _heroSkinSetter;

        public event Action OnDeath;
        public event Action OnFoodEaten;

        #region MonoBehaviour

        private void Awake()
        {
            _sword.SetActive(false);
            InitFields();
        }

        private void OnEnable()
        {
            _pauseManager.Register(this);
            Jump.OnJumped += AnimateJump;
            Jump.OnJumped += PlayJumpSound;
            Jump.OnLanded += AnimateJumpEnd;
            Jump.OnLanded += HasLanded;
            Jump.OnJumped += RemoveParent;

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
            Jump.OnJumped -= RemoveParent;

            if (ShouldPlayParticles == true)
                Jump.OnJumped -= SpawnParticles;
        }

        private void Update()
        {
            if (IsPaused == true)
                return;

            Jump.TryJump();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsPaused == true || _isDead == true)
                return;
            
            HandleDeadlyObjectCollision(other);
            HandleEnemyCollision(other);
            HandleFoodCollision(other);
            HandleSwordChestCollision(other);
            HandleChestCollision(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HandleFallingIslandCollision(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            HandleFallingIslandExit(other);
        }

        #endregion
        
        [Inject]
        private void Construct(PauseManager pauseManager, IAudioHandler audioHandler)
        {
            _pauseManager = pauseManager;
            _audioHandler = audioHandler;
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused == true)
                DisableRigidbody();
            else
                EnableRigidbody();
        }

        public void GetSword()
        {
            _heroAnimator.DoHit();
            
            if (_hasSword == true)
                return;

            _hasSword = true;
            _heroAnimator.GetSword();
            _audioPlayer.PlaySword();
        }

        public void Kick()
        {
            if (_enemyToKick == null)
                throw new Exception("_enemyToKick is null!");

            var force = Vector2.right * _kickForce;
            _enemyToKick.GetKicked(force);
            _audioPlayer.PlaySword();

            _enemyToKick = null;
        }

        public void ActivateSwordObject()
        {
            _sword.SetActive(true);
        }

        public void Revive()
        {
            transform.position = Jump.LastJump;
            _isDead = false;
            EnableRigidbody();
        }

        private void InitFields()
        {
            HeightCheckService heightTestService = new(_collider,
                _ground,
                HeightTestRadius,
                _groundCheckTransform,
                _groundCheckRadius);
            
            Jump = new HeroJump(
                this,
                _jumpCurve,
                _jumpXAccelCurve,
                heightTestService,
                _duration);

            _heroAnimator = new HeroAnimator(_animator);
            
            _audioPlayer.AudioHandler = _audioHandler;
        }

        private void HasLanded()
        {
            _hasJumped = false;
        }
        
        private void PlayJumpSound()
        {
            if (_hasJumped == true)
                return;

            _hasJumped = true;
            _audioPlayer.PlayJump();
        }

        public void PlayFoodSound()
        {
            _audioPlayer.PlayEat();
        }

        private void AnimateJump()
        {
            _heroAnimator.SetAsJumping();
        }
        
        private void AnimateJumpEnd()
        {
            _heroAnimator.EndJumping();
        }

        private void SpawnParticles() =>
             _jumpParticlesPlayer.SpawnTillDuration();

        private void HandleDeadlyObjectCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<DeadlyForPlayerObject>(other, PerformDeath);
        }

        private void HandleEnemyCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<Enemy>(other, InteractWithEnemy);
        }

        private void HandleFoodCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<Food>(other, EatFood);
        }

        private void HandleChestCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<Chest>(other, OpenChest);
        }

        private void HandleSwordChestCollision(Collider2D other)
        {
            Tools.InvokeIfNotNull<SwordChest>(other, GetSword);
        }

        private void HandleFallingIslandCollision(Collision2D other)
        {
            Tools.InvokeIfNotNull<FallingIsland>(other, LandOnFallingIsland);
        }

        private void HandleFallingIslandExit(Collision2D other)
        {
            Tools.InvokeIfNotNull<FallingIsland>(other, RemoveParent);
        }

        private void PerformDeath()
        {
            _heroAnimator.PerformDeath();
            _audioPlayer.PlayGameOver();
            
            DisableRigidbody();
            _isDead = true;

            OnDeath?.Invoke();
        }

        private void InteractWithEnemy(Enemy enemy)
        {
            if (_hasSword == false)
            {
                PerformDeath();
                return;
            }

            _enemyToKick = enemy;
            _heroAnimator.DoHit();

            enemy.PerformDeath();
        }

        private void EatFood(Food food)
        {
            if (_isDead == true)
                return;
            
            _heroAnimator.EatFood();
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
            _heroAnimator.DoHit();
            _audioPlayer.PlayCollected();
            chest.Open();
        }

        private void LandOnFallingIsland(FallingIsland island)
        {
            transform.SetParent(island.transform, true);
            island.TriggerFalling();
        }

        private void RemoveParent()
        {
            transform.SetParent(null, true);
        }
    }
}