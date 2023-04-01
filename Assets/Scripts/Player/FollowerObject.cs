using UnityEngine;
using System.Collections;
using System;

namespace CapybaraAdventure.Player
{
    public class FollowerObject : MonoBehaviour
    {
        [SerializeField] private Transform _toFollow;
        [SerializeField] private float _updateIntervalInSeconds = 1f;
        [SerializeField] private bool _ignoreXMovement;
        [SerializeField] private bool _ignoreYMovement;

        private Transform _transform;

        public bool IsFollowObjectInitialized => _toFollow != null;

        #region MonoBehaviour

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            StartCoroutine(Follow());
        }

        #endregion

        public void Init(GameObject toFollow)
        {
            if (IsFollowObjectInitialized == true)
                throw new InvalidOperationException("Object to follow has been already initialized! You cannot do it more than once");

            _toFollow = toFollow.transform;
        }

        public void StopFollowing()
        {
            StopCoroutine(Follow());
        }

        private IEnumerator Follow()
        {
            while (true)
            {
                yield return new WaitUntil(
                    () => IsFollowObjectInitialized == true
                    );

                Vector3 movedPosition = GetMovedPosition();
                _transform.position = movedPosition;

                yield return new WaitForSeconds(_updateIntervalInSeconds);
            }
        }

        private Vector3 GetMovedPosition()
        {
            Vector3 movedPosition = _transform.position;
            if (_ignoreXMovement == false)
                movedPosition = MoveX(movedPosition);

            if (_ignoreYMovement == false)
                movedPosition = MoveY(movedPosition);

            return movedPosition;
        }

        private Vector2 MoveX(Vector2 movedPosition)
        {
            Vector3 toFollowPosition = _toFollow.position;
            movedPosition.x = toFollowPosition.x;

            return movedPosition;
        }

        private Vector2 MoveY(Vector2 movedPosition)
        {
            Vector3 toFollowPosition = _toFollow.position;
            movedPosition.y = toFollowPosition.y;

            return movedPosition;
        }
    }
}