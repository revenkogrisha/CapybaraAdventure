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
using Cysharp.Threading.Tasks;

namespace CapybaraAdventure.Player
{
    public class Hero : MonoBehaviour, IPhysicalObject, IPauseHandler
    {
        private const float HeightTestRadius = 0.03f;
        private const float GameFinishedDelay = 7f;

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

        [Header("*NEW* Anim Force")]
        [SerializeField] private Vector2 _animJumpForce = new(0f, 2f);
        [SerializeField] private Vector2 _animForceOnFinish = new (-8f, 0f);
        [SerializeField] private float _handleFinishDelay = 1.5f;

        [Header("*NEW* Main Camera")]
        [SerializeField] private float _jumpFOV = 6.25f;
        [SerializeField] private float _landingFOV = MainCamera.DefaultFOV;
        [SerializeField] private float _fovLerpDuration = 0.25f;
        [SerializeField] private float _focusFOV = 2.5f;
        [SerializeField] private float _focusFOVLerpDuration = 5f;

        [Header("Audio")]
        [SerializeField] private HeroAudioPlayer _audioPlayer;
        
        private HeroAnimator _heroAnimator;
        private PauseManager _pauseManager;
        private bool _isDead = false;
        private bool _hasJumped = false;
        private bool _hasSword = false;
        private Enemy _enemyToKick = null;
        private IAudioHandler _audioHandler;
        private MainCamera _mainCamera;

        public HeroJump Jump { get; private set; }
        public bool IsPaused => _pauseManager.IsPaused;
        public bool ShouldPlayParticles => _jumpParticlesPlayer != null;
        public Transform Transform => transform;
        public Rigidbody2D Rigidbody2D => _rigidBody2D;
        public HeroSkinSetter SkinSetter => _heroSkinSetter;

        public event Action OnDeath;
        public event Action<float> OnLevelFinished;
        public event Action OnFoodEaten;

        #region MonoBehaviour

        private void Awake()
        {
            _sword.SetActive(false);
            InitFields();
        }

        private void Start()
        {
            _mainCamera.SetFollow(transform);
        }

        private void OnEnable()
        {
            _pauseManager.Register(this);
            Jump.OnJumped += AnimateJump;
            Jump.OnJumped += HandleCameraOnJump;
            Jump.OnJumped += PlayJumpSound;
            Jump.OnLanded += AnimateJumpEnd;
            Jump.OnLanded += HasLanded;
            Jump.OnLanded += HandleCameraOnLanding;
            Jump.OnJumped += RemoveParent;

            if (ShouldPlayParticles == true)
                Jump.OnJumped += SpawnParticles;
        }

        private void OnDisable()
        {
            _pauseManager.Unregister(this);
            Jump.OnJumped -= AnimateJump;
            Jump.OnJumped -= HandleCameraOnJump;
            Jump.OnJumped -= PlayJumpSound;
            Jump.OnLanded -= AnimateJumpEnd;
            Jump.OnLanded -= HasLanded;
            Jump.OnLanded -= HandleCameraOnLanding;
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
        private void Construct(PauseManager pauseManager, IAudioHandler audioHandler, MainCamera mainCamera)
        {
            _pauseManager = pauseManager;
            _audioHandler = audioHandler;
            _mainCamera = mainCamera;
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
                return;

            var force = Vector2.right * _kickForce;
            _enemyToKick.GetKicked(force);
            _audioHandler.PlaySound(AudioName.EnemyDeath);

            _enemyToKick = null;
        }

        public void ActivateSwordObject()
        {
            _sword.SetActive(true);
        }

        public void Revive()
        {
            _heroAnimator.SetDeath(false);
            transform.position = Jump.LastJump;
            _isDead = false;
            EnableRigidbody();
        }

        public void AddRigidbodyForceUp()
        {
            _rigidBody2D.AddForce(_animJumpForce, ForceMode2D.Impulse);
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

            _mainCamera.SetDefaultFOV();
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

        private void HandleCameraOnJump()
        {
            if (_mainCamera != null)
                _mainCamera.SetLerpFOV(_jumpFOV, _fovLerpDuration);
        }
        
        private void HandleCameraOnLanding()
        {
            if (_mainCamera != null)
                _mainCamera.SetLerpFOV(_landingFOV, _fovLerpDuration);
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
            _heroAnimator.SetDeath(true);
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

            // NEW
            if (chest is TreasureChest)
            {
                HandleTreasureChest();
            }
        }

        private void LandOnFallingIsland(FallingIsland island)
        {
            transform.SetParent(island.transform, true);
            island.TriggerFalling().Forget();
        }

        private void RemoveParent()
        {
            transform.SetParent(null, true);
        }

        // NEW
        private void HandleTreasureChest()
        {
            OnLevelFinished?.Invoke(GameFinishedDelay);
            _audioPlayer.PlayGameFinished();
            
            // DisableRigidbody();
            
            _mainCamera.SetLerpFOV(_focusFOV, _focusFOVLerpDuration);
            
            Invoke(nameof(HandleFinishAnim), _handleFinishDelay);
        }

        private void HandleFinishAnim()
        {
            _mainCamera.SetFollow(default);
            _rigidBody2D.AddForce(_animForceOnFinish, ForceMode2D.Impulse);
            _heroAnimator.SetFinished();
        }
    }
}