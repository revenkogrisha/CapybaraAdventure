using System;
using CapybaraAdventure.UI;
using UnityEngine;
using UnityTools.Buttons;
using Zenject;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        [SerializeField] private UIButton _playButton;

        private GameStartup _gameStartup;
        private GameUI _inGameUI;

        private bool _areFieldsInjected = false;

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
        
        public void InjectFields(DiContainer diContainer)
        {
            _gameStartup = diContainer.Resolve<GameStartup>();
            _inGameUI = diContainer.Resolve<GameUI>();

            if (_gameStartup == null && _inGameUI == null)
                throw new ZenjectException("Fields weren't injected successfully!");

            _areFieldsInjected = true;
        }

        private void StartGame()
        {
            if (_areFieldsInjected == false)
                throw new NullReferenceException("Fields weren't injected! Call InjectFields(...) first before calling StartGame()");

            _gameStartup.StartGame();
            Conceal();
            _inGameUI.Reveal();
        }
    }
}