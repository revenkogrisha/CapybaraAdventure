using CapybaraAdventure.Player;

namespace CapybaraAdventure.Game
{
    public class GameOverHandler
    {
        private readonly Hero _hero;
        private readonly PauseManager _pauseManager;

        public bool IsPaused => _pauseManager.IsPaused;

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

        private void HandleHeroDeath()
        {
            if (IsPaused == false)
                _pauseManager.SetPaused(true);
        }
    }
}