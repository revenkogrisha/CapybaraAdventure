using System;
using System.Collections;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class Score : MonoBehaviour
    {
        private const float _scoreUpdateIntervalInSeconds = 0.5f;
        
        private Transform _heroTransform;
        private IEnumerator _countCoroutine;

        public bool IsInitialized => _heroTransform != null;

        public int ScoreCount { get; private set; }
        private bool HasCountCoroutineInitialized => _countCoroutine != null;

        public event Action<int> OnScoreChanged;

        public void Init(Hero hero)
        {
            if (IsInitialized == true)
                throw new InvalidOperationException("Score cannot be initialized more than once!");

            GameObject heroObject = hero.gameObject;
            _heroTransform = heroObject.transform;
        }

        public void StartCount()
        {
            if (HasCountCoroutineInitialized == true)
                throw new InvalidOperationException("Count coroutine has already started! You cannot start it more than once");

            _countCoroutine = CountScore();
            StartCoroutine(_countCoroutine);
        }
        
        public void StopCount()
        {
            if (HasCountCoroutineInitialized == false)
                return;

            StopCoroutine(_countCoroutine);
            _countCoroutine = null;
        }

        private IEnumerator CountScore()
        {
            while (true)
            {
                if (IsInitialized == false)
                    throw new NullReferenceException("The class hasn't been initialized! Call Init(Hero) first");

                float heroX = _heroTransform.position.x;
                if (heroX < 0f || heroX < ScoreCount)
                    yield return null;

                int heroXRounded = (int)Mathf.Round(heroX);
                ScoreCount = heroXRounded;

                OnScoreChanged?.Invoke(ScoreCount);

                yield return new WaitForSeconds(_scoreUpdateIntervalInSeconds);
            }
        }
    }
}