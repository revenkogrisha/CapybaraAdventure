using System;
using CapybaraAdventure.UI;
using UnityEngine;
using UnityTools.Buttons;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        [SerializeField] private UIButton _playButton;

        private GameStartup _gameStartup;
        private GameUI _inGameUI;
        private bool _isInitialized = false;

        public event Action OnMenuWorkHasOver;

        #region MonoBehaviour

        private void OnEnable()
        {
            _playButton.OnClicked += StartGame;
        }

        private void OnDisable()
        {
            _playButton.OnClicked -= StartGame;
        }

        #endregion
        
        public void Init(
            GameStartup gameStartup,
            GameUI inGameUI)
        {
            _gameStartup = gameStartup;
            _inGameUI = inGameUI;
            _isInitialized = true;
        }

        private void StartGame()
        {
            if (_isInitialized == false)
                throw new NullReferenceException("The instance of the class haven't been initialized yet! Call Init(...) first before calling StartGame()");

            _gameStartup.StartGame();
            Conceal();
            _inGameUI.Reveal();

            OnMenuWorkHasOver?.Invoke();
        }
    }
}