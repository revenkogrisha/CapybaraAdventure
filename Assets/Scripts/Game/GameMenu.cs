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
        }

        private void StartGame()
        {
            _gameStartup.StartGame();
            Conceal();
            _inGameUI.Reveal();
        }
    }
}