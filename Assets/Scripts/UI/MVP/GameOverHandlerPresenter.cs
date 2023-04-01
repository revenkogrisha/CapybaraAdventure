using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private readonly GameOverHandler _gameOverHandler;
        private readonly GameUI _inGameUI;
        private readonly GameOverScreenProvider _screenProvider;
        private readonly Score _score;

        public GameOverHandlerPresenter(
            GameOverHandler handler,
            GameUI inGameUI,
            GameOverScreenProvider screenProvider,
            Score score)
        {
            _gameOverHandler = handler;
            _inGameUI = inGameUI;
            _screenProvider = screenProvider;
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
            _inGameUI.Conceal();
            _score.StopCount();
            await LoadScreen();
        }

        private async Task LoadScreen()
        {
            GameOverScreen screen = await _screenProvider.Load();
            screen.Reveal();
        }
    }
}