using System.Collections;
using UnityEngine;
using UnityTools;

namespace CapybaraAdventure.Level
{
    public class FallingIsland : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidBody2D;

        [Header("Trigger Chance")]
        [SerializeField, Range(0, 101)] private int _triggerChance = 40;

        [Header("Falling Settings")]
        [SerializeField] private float _delayBeforeFalling = 1.5f;
        [SerializeField] private float _delayBeforeDestroy = 2f;
        [SerializeField] private float _fallSpeed = 10f;
        [SerializeField] private float _moveDownDuration = 0.2f;
        [SerializeField] private float _moveDownDistance = 1.4f;

        [Header("Particle Effect")]
        [SerializeField] private ParticleSystem _particleEffect;

        private bool _isTriggered = false;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Static;
            _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void TriggerFalling()
        {
            bool shallTrigger = Tools.GetChance(_triggerChance);
            if (_isTriggered == true || shallTrigger == false)
                return;

            _isTriggered = true;

            TryPlayParticles();

            _initialPosition = transform.position;
            StartCoroutine(MoveDownSmoothly());

            Invoke(nameof(StartFalling), _delayBeforeFalling);
        }

        private void StartFalling()
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody2D.velocity = Vector2.down * _fallSpeed;

            Destroy(gameObject, _delayBeforeDestroy);
        }

        private void TryPlayParticles()
        {
            if (_particleEffect == null)
                return;

            _particleEffect.Play();
        }

        private IEnumerator MoveDownSmoothly()
        {
            float elapsedTime = 0f;
            Vector3 targetPosition = transform.position + Vector3.down * _moveDownDistance;

            while (elapsedTime < _moveDownDuration)
            {
                MoveToPosition(targetPosition, elapsedTime / _moveDownDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            MoveToPosition(targetPosition, 1f);
        }

        private void MoveToPosition(Vector3 targetPosition, float time)
        {
            transform.position = Vector3.Lerp(_initialPosition, targetPosition, time);
        }
    }
}
