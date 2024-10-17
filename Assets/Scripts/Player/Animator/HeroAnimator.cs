using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using CapybaraAdventure.Player;
using DG.Tweening;
using UnityEngine.UI;

namespace Core.Player
{
    public class HeroAnimator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Hero _hero;
        [SerializeField] private Animator _animator;

        [Header("General")]
        [SerializeField] private bool _alwaysIdle = false;
        [SerializeField] private HeroAnimatorConfig _config;
        
        [Header("Arms")]
        [SerializeField] private Transform _armWithHook;
        [SerializeField] private GameObject _sword;
        [SerializeField] private GameObject _slash;

        [Header("Legs")]
        [SerializeField] private Transform[] _legs;
        
        private Transform _thisTransform;
        private bool _shouldRotateHand;
        private bool _shouldRotateBody;

        public bool HeroRaising => 
            _hero.Rigidbody2D.velocity.y > _config.HeroRaisingVelocityMinimum;

        public bool HeroFalling => 
            _hero.Rigidbody2D.velocity.y < _config.HeroFallingVelocityMinimum;


        #region MonoBehaviour

        private void Awake() => 
            _thisTransform = transform;

        private void OnEnable()
        {
            if (_alwaysIdle == true)
            {
                PerformLanding();
                SetRunning(false);
                return;
            }
        }
        
        #endregion

        private async UniTaskVoid StartRotatingBody(Transform targetJoint)
        {
            _shouldRotateBody = true;

            while (_shouldRotateBody == true)
            {
                _thisTransform.rotation = LerpRotate(
                    _thisTransform,
                    targetJoint,
                    _config.BodyRotationSpeed);

                await UniTask.NextFrame(destroyCancellationToken);
            }
        }

        private async UniTaskVoid StartRotatingHand(Transform targetJoint)
        {
            _animator.SetBool(HeroAnimatorConfig.FreeFallingHash, false);
            _animator.enabled = false;

            _shouldRotateHand = true;

            while (_shouldRotateHand == true)
            {
                _armWithHook.rotation = LerpRotate(
                    _armWithHook,
                    targetJoint,
                    _config.HandRotationSpeed);

                await UniTask.NextFrame(destroyCancellationToken);
            }
        }

        private Quaternion LerpRotate(Transform target, Transform targetJoint, float speed)
        {
            Vector2 direction = targetJoint.position - target.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            
            float delta = Time.deltaTime * speed;
            return Quaternion.Slerp(
                target.rotation,
                quaternion,
                delta);
        }

        private void StopRotatingHand()
        {
            _shouldRotateHand = false;
            LerpArmToDefault();

            _animator.enabled = true;
            _animator.SetBool(HeroAnimatorConfig.FreeFallingHash, true);
        }

        private void StopRotatingBody()
        {
            _shouldRotateBody = false;
            LerpBodyToDefatult();
        }

        private void LerpArmToDefault() =>
            RotateToDefault(_armWithHook);

        private void LerpBodyToDefatult() =>
            RotateToDefault(_thisTransform);

        private void RotateToDefault(Transform target) =>
            target.DORotate(_config.DefaultRotation, _config.RotateToDefaultDuration);

        private void RotateLegsRaising()
        {
            foreach (Transform leg in _legs)
                leg.DORotate(_config.RaisingLegsRotation, _config.LegsRotationDuration);
        }

        private void RotateLegsFalling()
        {
            foreach (Transform leg in _legs)
                leg.DORotate(_config.FallingLegsRotation, _config.LegsRotationDuration);
        }

        private void PerformLanding()
        {
            _animator.enabled = true;
            _sword.SetActive(true);
            _animator.SetTrigger(HeroAnimatorConfig.LandedHash);
            _animator.SetBool(HeroAnimatorConfig.GrapplingHash, false);
        }

        private void StartGrappling()
        {
            _slash.SetActive(false);
            _sword.SetActive(false);
            _animator.SetBool(HeroAnimatorConfig.GrapplingHash, true);
        }

        private void SetRunning(bool value)
        {
            _animator.enabled = true;
            _animator.SetBool(HeroAnimatorConfig.RunningHash, value);
        }

        private void SetJumping(bool value)
        {
            _slash.SetActive(!value);
            _animator.SetBool(HeroAnimatorConfig.JumpingHash, value);
        }

        private void PerformDash()
        {
            _slash.SetActive(true);
            _animator.SetTrigger(HeroAnimatorConfig.DashedHash);
        }
    }
}