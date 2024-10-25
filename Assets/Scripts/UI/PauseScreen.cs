using CapybaraAdventure.Other;
using UnityEngine;
using System;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.UI
{
    public class PauseScreen : UIBase
    {
        [SerializeField] private UIButton _backToGameButton;
        [SerializeField] private UIButton _restartButton;

        private PauseManager _pauseManager;
        private GameUI _gameUI;
        private LoadingScreenProvider _loadingScreenProvider;
        private bool isInitialized = false;

        #region MonoBehaviour

        private void OnEnable()
        {
            _backToGameButton.OnClicked += ReturnToGame;
            _restartButton.OnClicked += RestartGame;
        }

        private void OnDisable()
        {
            _backToGameButton.OnClicked -= ReturnToGame;
            _restartButton.OnClicked -= RestartGame;
        }

        #endregion
        
        public void Init(
            PauseManager pauseManager,
            GameUI gameUI,
            LoadingScreenProvider loadingScreenProvider)
        {
            _pauseManager = pauseManager;
            _gameUI = gameUI;
            _loadingScreenProvider = loadingScreenProvider;

            isInitialized = true;
        }

        private void ReturnToGame()
        {
            if (isInitialized == false)
                throw new InvalidOperationException("The component wasn't initialized! Call Init() first.");

            _pauseManager.SetPaused(false);

            Conceal();
            _gameUI.Reveal();
        }

        private async void RestartGame() =>
            await _loadingScreenProvider.LoadGameAsync();
    }
}
