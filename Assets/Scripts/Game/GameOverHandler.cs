using System;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Game
{
    public class GameOverHandler
    {
        private readonly Hero _hero;
        private readonly PauseManager _pauseManager;

        public bool IsPaused => _pauseManager.IsPaused;
        public bool WasGameContinuedBefore { get; private set; } = false;
 
        public event Action OnGameHasOver;

        public GameOverHandler(Hero hero, PauseManager pause)
        {
            _hero = hero;
            _pauseManager = pause;
        }

        public void Enable()
        {
            _hero.OnDeath += HandleHeroDeath;
        }

        public void Disable()
        {
            _hero.OnDeath -= HandleHeroDeath;
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
    }
}