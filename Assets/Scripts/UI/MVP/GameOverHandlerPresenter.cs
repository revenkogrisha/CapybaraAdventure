using CapybaraAdventure.Game;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private GameOverHandler _gameOverHandler;
        private GameOverScreenProvider _screenProvider;

        public GameOverHandlerPresenter(GameOverHandler handler)
        {
            _gameOverHandler = handler;
        }

        public void Enable()
        {
            _gameOverHandler.OnGameHasOver += OnGameHasOverHandler;
        }

        public void Disable()
        {
            _gameOverHandler.OnGameHasOver -= OnGameHasOverHandler;
        }

        [Inject]
        private void Construct(GameOverScreenProvider provider)
        {
            _screenProvider = provider;
        }

        private async void OnGameHasOverHandler()
        {
            await _screenProvider.Load();
        }
    }
}