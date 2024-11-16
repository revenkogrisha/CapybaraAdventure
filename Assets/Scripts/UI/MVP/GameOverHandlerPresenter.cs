using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class GameOverHandlerPresenter
    {
        private readonly GameOverHandler _gameOverHandler;
        private readonly GameUI _gameUI;
        private readonly GameOverScreenProvider _overScreenProvider;
        private readonly GameFinishedScreenProvider _finishedScreenProvider;
        private readonly Score _score;
        private GameOverScreen _screen;

        private bool WasGameContinuedBefore => _gameOverHandler.WasGameContinuedBefore;

        public GameOverHandlerPresenter(
            GameOverHandler handler,
            GameUI inGameUI,
            GameOverScreenProvider screenProvider,
            GameFinishedScreenProvider screenProvider2,
            Score score)
        {
            _gameOverHandler = handler;
            _gameUI = inGameUI;
            _overScreenProvider = screenProvider;
            _finishedScreenProvider = screenProvider2;
            _score = score;
        }

        public void Enable()
        {
            _gameOverHandler.OnGameHasOver += OnGameHasOverHandler;
            _gameOverHandler.OnGameFinished += OnGameFinishedHandler;
            _gameOverHandler.OnGameUIConcealPrompted += ConcealGameUI;
        }

        public void Disable()
        {
            _gameOverHandler.OnGameHasOver -= OnGameHasOverHandler;
            _gameOverHandler.OnGameFinished -= OnGameFinishedHandler;
            _gameOverHandler.OnGameUIConcealPrompted -= ConcealGameUI;

            if (_screen != null)
                _screen.OnGameContinued -= OnGameHasContinuedHandler;
        }

        private async void OnGameHasOverHandler()
        {
            _gameUI.Conceal();
            _score.StopCount();
            await LoadOverScreen();

            if (WasGameContinuedBefore == true)
                _screen.BlockContinuing();
        }

        private async void OnGameFinishedHandler()
        {
            // _gameUI.Conceal();
            _score.StopCount();
            await LoadFinishedScreen();
        }

        private async Task LoadOverScreen()
        {
            _screen = await _overScreenProvider.Load();
            _screen.Reveal();

            _screen.OnGameContinued += OnGameHasContinuedHandler;
        }
        
        private async Task LoadFinishedScreen()
        {
            var screen = await _finishedScreenProvider.Load();
            screen.Reveal();
        }

        private void OnGameHasContinuedHandler()
        {
            if (WasGameContinuedBefore == true)
                return;

            _gameUI.Reveal();
            _score.StartCount();
            _overScreenProvider.Unload();
            _gameOverHandler.HandleHeroRevival();

            _screen.OnGameContinued -= OnGameHasContinuedHandler;
        }

        private void ConcealGameUI()
        {
            _gameUI.Conceal();
        }
    }
}