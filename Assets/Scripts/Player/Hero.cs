using UnityEngine;
using CapybaraAdventure.UI;
using Zenject;
using System;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.Player
{
    public class Hero : MonoBehaviour
    {
        private const float HeightTestRadius = 0.05f;

        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private AnimationCurve _jumpCurve;
        
        private bool _isDead;
        private JumpButton _jumpButton;
        private JumpSlider _jumpSlider;
        private PauseManager _pauseManager;
        private HeroJump _jump;
        private HeroPresenter _presenter;

        public bool IsPaused => _pauseManager.IsPaused;

        public event Action OnDeath;

        #region MonoBehaviour

        private void Awake()
        {
            _isDead = false;

            var heightTestService = new HeightCheckService(_collider, _ground, HeightTestRadius);
            _jump = new HeroJump(_rigidBody2D, _jumpCurve, heightTestService,_duration);
            _presenter = new HeroPresenter(_jump, _jumpButton, _jumpSlider);
        }

        private void OnEnable()
        {
            _presenter.Enable();
        }

        private void OnDisable()
        {
            _presenter.Disable();
        }

        private void Update()
        {
            if (IsPaused)
                return;

            _jump.TryJump();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsPaused)
                return;
                
            HandleCollisionWithDeadlyObject(other);
        }

        private void HandleCollisionWithDeadlyObject(Collider2D other)
        {
            if (_isDead)
                return;

            UnityTools
                .Tools
                .InvokeIfNotNull<DeadlyForPlayerObject>(other, PerformDeath);

            _isDead = true;
        }

        #endregion

        [Inject]
        private void Construct(
            JumpButton button,
            JumpSlider slider,
            PauseManager pauseManager)
        {
            _jumpButton = button;
            _jumpSlider = slider;
            _pauseManager = pauseManager;
        }

        private void PerformDeath() => OnDeath?.Invoke();
    }
}