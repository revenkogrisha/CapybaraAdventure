using System.Threading;
using CapybaraAdventure.Other;
using Cysharp.Threading.Tasks;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Serialization;
using UnityTools;

namespace CapybaraAdventure.Level
{
    public class FallingIsland : MonoBehaviour, IPoolItem
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidBody2D;

        [Header("Trigger Chance")]
        [SerializeField, Range(0, 101)] private int _triggerChance = 40;

        [FormerlySerializedAs("_moveDirection")]
        [Header("Falling Settings")]
        [SerializeField] private Vector3 _moveDirectionVector = Vector3.down;
        [SerializeField] private float _delayBeforeFalling = 1.7f;
        [SerializeField] private float _delayBeforeRigidbodyFreeze = 2f;
        [SerializeField] private float _fallSpeed = 10f;
        [SerializeField] private float _moveDownDuration = 0.2f;
        [SerializeField] private float _moveDownDistance = 1.4f;

        [Header("Particle Effect")]
        [SerializeField] private ParticleSystem _particleEffect;

        private bool _isTriggered = false;
        private Vector3 _initialLocalPosition;
        private CancellationToken _cancellationToken;

        public void OnSpawn()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            FreezeRigidBody();
        }

        public void OnDespawn()
        {
            Vector2 newPosition = transform.localPosition;
            newPosition.y = _initialLocalPosition.y;
            transform.localPosition = newPosition;
            _isTriggered = false;
        }

        public async UniTask TriggerFalling(CancellationToken token = default)
        {
            if (token == default)
                token = _cancellationToken;
                
            bool shallTrigger = Tools.GetChance(_triggerChance);
            if (_isTriggered == true || shallTrigger == false)
                return;

            _isTriggered = true;

            TryPlayParticles();

            _initialLocalPosition = transform.localPosition;
            MoveDownSmoothly(token).Forget();

            // NEW
            // await MyUniTask.Delay(_delayBeforeFalling, token);
            // StartFalling(token).Forget();
        }

        private void TryPlayParticles()
        {
            if (_particleEffect == null)
                return;

            _particleEffect.Play();
        }

        private async UniTask MoveDownSmoothly(CancellationToken token)
        {
            float elapsedTime = 0f;
            Vector3 targetPosition = transform.localPosition + _moveDirectionVector * _moveDownDistance;

            while (elapsedTime < _moveDownDuration)
            {
                MoveToPosition(targetPosition, elapsedTime / _moveDownDuration);
                elapsedTime += Time.deltaTime;
                await UniTask.NextFrame(token);
            }

            MoveToPosition(targetPosition, 1f);
        }

        private async UniTask StartFalling(CancellationToken token)
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody2D.velocity = Vector2.down * _fallSpeed;

            await MyUniTask.Delay(_delayBeforeRigidbodyFreeze, token);

            FreezeRigidBody();
        }

        private void MoveToPosition(Vector3 targetPosition, float time)
        {
            transform.localPosition = Vector3.Lerp(_initialLocalPosition, targetPosition, time);
        }

        private void FreezeRigidBody()
        {
            if (this == null)
                return;
                
            _rigidBody2D.bodyType = RigidbodyType2D.Static;
            _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
