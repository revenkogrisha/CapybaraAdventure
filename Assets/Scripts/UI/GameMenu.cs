using System;
using CapybaraAdventure.UI;
using UnityEngine;
using UnityTools.Buttons;
using TMPro;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        private const string HighScoreOriginalText = "HighScore:";

        [SerializeField] private UIButton _playButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;

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
            GameUI inGameUI,
            Score score)
        {
            _gameStartup = gameStartup;
            _inGameUI = inGameUI;
            _isInitialized = true;

            var highScore = score.HighScore;
            _highScoreText.text = $"{HighScoreOriginalText} {highScore}";
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