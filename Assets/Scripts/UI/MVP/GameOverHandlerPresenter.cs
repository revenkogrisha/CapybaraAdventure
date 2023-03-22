using CapybaraAdventure.Game;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private GameOverHandler _gameOverHandler;
        private GameOverScreenProvider _screenProvider;

        public GameOverHandlerPresenter(
            GameOverHandler handler,
            GameOverScreenProvider provider)
        {
            _gameOverHandler = handler;
            _screenProvider = provider;
        }

        public void Enable()
        {
            _gameOverHandler.OnGameHasOver += OnGameHasOverHandler;
        }

        public void Disable()
        {
            _gameOverHandler.OnGameHasOver -= OnGameHasOverHandler;
        }

        private async void OnGameHasOverHandler()
        {
            var screen = await _screenProvider.Load();
            screen.Reveal();
        }
    }
}