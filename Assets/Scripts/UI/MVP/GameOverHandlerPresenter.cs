using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private readonly GameOverHandler _gameOverHandler;
        private readonly GameUI _gameUI;
        private readonly GameOverScreenProvider _screenProvider;
        private readonly Score _score;
        private GameOverScreen _screen;

        private bool WasGameContinuedBefore => _gameOverHandler.WasGameContinuedBefore;

        public GameOverHandlerPresenter(
            GameOverHandler handler,
            GameUI inGameUI,
            GameOverScreenProvider screenProvider,
            Score score)
        {
            _gameOverHandler = handler;
            _gameUI = inGameUI;
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

            if (_screen != null)
                _screen.OnGameContinued -= OnGameHasContinuedHandler;
        }

        private async void OnGameHasOverHandler()
        {
            _gameUI.Conceal();
            _score.StopCount();
            await LoadScreen();

            if (WasGameContinuedBefore)
                _screen.BlockContinuing();
        }

        private async Task LoadScreen()
        {
            _screen = await _screenProvider.Load();
            _screen.Reveal();

            _screen.OnGameContinued += OnGameHasContinuedHandler;
        }

        private void OnGameHasContinuedHandler()
        {
            if (WasGameContinuedBefore == true)
                return;

            _gameUI.Reveal();
            _score.StartCount();
            _screenProvider.Unload();
            _gameOverHandler.HandleHeroRevival();

            _screen.OnGameContinued -= OnGameHasContinuedHandler;
        }
    }
}