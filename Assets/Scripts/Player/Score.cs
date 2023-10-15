using System;
using System.Threading;
using CapybaraAdventure.Game;
using CapybaraAdventure.Other;
using CapybaraAdventure.Save;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Player
{
    public class Score : MonoBehaviour, IPauseHandler
    {
        private const float ScoreUpdateIntervalInSeconds = 0.5f;
        
        private Transform _heroTransform;
        private PauseManager _pauseManager;
        private bool _shouldBlockCount = false;
        private bool _isCounting = false;
        private CancellationToken _cancellationToken;

        public bool IsInitialized => _heroTransform != null;

        public int ScoreCount { get; private set; } = 0;
        public int HighScore { get; private set; } = 0;

        public event Action<int> OnScoreChanged;

        private void Awake()
        {
            _pauseManager.Register(this);
            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }

        [Inject]
        private void Construct(
            PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        public void InitHero(Hero hero)
        {
            GameObject heroObject = hero.gameObject;
            _heroTransform = heroObject.transform;
        }

        public void LoadHighScore(int value)
        {
            HighScore = value;
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused == true)
                StopCount();
            else
                StartCount();
        }

        public void StartCount()
        {
            if (_isCounting == true)
                return;

            _shouldBlockCount = false;
            CountScore(_cancellationToken).Forget();
        }
        
        public void StopCount()
        {
            if (_isCounting == false)
                return;

            _shouldBlockCount = true;
        }

        private async UniTask CountScore(CancellationToken token)
        {
            _isCounting = true;

            while (_shouldBlockCount == false && this != null)
            {
                if (IsInitialized == false)
                    throw new NullReferenceException("The class hasn't been initialized! Call Init(Hero) first");
                    
                float heroX = _heroTransform.position.x;
                if (heroX < 0f || heroX < ScoreCount)
                {
                    await UniTask.NextFrame(token);
                    continue;
                }

                int heroXRounded = (int)Mathf.Round(heroX);
                ScoreCount = heroXRounded;

                TrySaveHighScore();

                OnScoreChanged?.Invoke(ScoreCount);

                await MyUniTask.Delay(ScoreUpdateIntervalInSeconds, token);
            }

            _isCounting = false;
        }

        private void TrySaveHighScore()
        {
            if (ScoreCount <= HighScore)
                return;

            HighScore = ScoreCount;
        }
    }
}