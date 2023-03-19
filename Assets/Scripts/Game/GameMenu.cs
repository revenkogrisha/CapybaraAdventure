using CapybaraAdventure.UI;
using UnityEngine;
using UnityTools.Buttons;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private UIButton _playButton;
        [SerializeField] private GameUI _inGameUI;

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

        private void StartGame()
        {
            _gameStartup.StartGame();
            Conceal();
            _inGameUI.Reveal();
        }
    }
}