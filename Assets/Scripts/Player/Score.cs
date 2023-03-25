using System;
using System.Collections;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class Score : MonoBehaviour
    {
        private const float _scoreUpdateIntervalInSeconds = 0.5f;
        
        private Transform _heroTransform;

        public bool IsInitialized => _heroTransform != null;

        public int ScoreCount { get; private set; }

        public event Action<int> OnScoreChanged;

        private void Start()
        {
            StartCoroutine(CountScore());
        }

        public void Init(Hero hero)
        {
            if (IsInitialized == true)
                throw new InvalidOperationException("Score cannot be initialized more than once!");

            var heroObject = hero.gameObject;
            _heroTransform = heroObject.transform;
        }

        private IEnumerator CountScore()
        {
            while (true)
            {
                yield return new WaitUntil(
                    () => IsInitialized == true
                    );

                var heroY = _heroTransform.position.y;
                var heroYRounded = (int)Mathf.Round(heroY);

                ScoreCount = heroYRounded;

                OnScoreChanged?.Invoke(ScoreCount);

                yield return new WaitForSeconds(_scoreUpdateIntervalInSeconds);
            }
        }
    }
}