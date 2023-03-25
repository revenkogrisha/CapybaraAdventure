using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private GameOverHandler _gameOverHandler;
        private GameOverScreenProvider _screenProvider;
        private readonly Score _score;

        public GameOverHandlerPresenter(
            GameOverHandler handler,
            GameOverScreenProvider provider,
            Score score)
        {
            _gameOverHandler = handler;
            _screenProvider = provider;
            _score = score;
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
            await LoadScreen();
            _score.StopCount();
        }

        private async Task LoadScreen()
        {
            var screen = await _screenProvider.Load();
            screen.Reveal();
        }
    }
}