using System;
using CapybaraAdventure.Player;
using Cysharp.Threading.Tasks;

namespace CapybaraAdventure.Game
{
    public class GameOverHandler
    {
        private readonly Hero _hero;
        private readonly PauseManager _pauseManager;

        public bool IsPaused => _pauseManager.IsPaused;
        public bool WasGameContinuedBefore { get; private set; } = false;
 
        public event Action OnGameHasOver;
        public event Action OnGameFinished;
        public event Action OnGameUIConcealPrompted;

        public GameOverHandler(Hero hero, PauseManager pause)
        {
            _hero = hero;
            _pauseManager = pause;
        }

        public void Enable()
        {
            _hero.OnDeath += HandleHeroDeath;
            _hero.OnLevelFinished += HandleHeroFinished;
        }

        public void Disable()
        {
            _hero.OnDeath -= HandleHeroDeath;
            _hero.OnLevelFinished -= HandleHeroFinished;
        }

        public void HandleHeroRevival()
        {
            if (IsPaused == true)
                _pauseManager.SetPaused(false);

            _hero.Revive();
            WasGameContinuedBefore = true;
        }

        private void HandleHeroDeath()
        {
            if (IsPaused == false)
                _pauseManager.SetPaused(true);

            OnGameHasOver?.Invoke();
        }
        
        private void HandleHeroFinished(float delay)
        {
            OnGameUIConcealPrompted?.Invoke();
            
            HandleHeroFinishedAsync(delay).Forget();
        }

        private async UniTask HandleHeroFinishedAsync(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            if (IsPaused == false)
                _pauseManager.SetPaused(true);
            
            OnGameFinished?.Invoke();
        }
    }
}