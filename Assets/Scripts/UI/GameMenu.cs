using System;
using CapybaraAdventure.UI;
using UnityEngine;
using UnityTools.Buttons;
using TMPro;
using CapybaraAdventure.Player;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        private const string HighScoreOriginalText = "HighScore:";

        [Header("UI Elements")]
        [SerializeField] private UIButton _playButton;
        [SerializeField] private UIButton _updgradeButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Transform _logo;

        [Header("Ads Settings")]
        [SerializeField] private AdInterstitial _interstitialAd;

        private GameStartup _gameStartup;
        private GameUI _inGameUI;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private UpgradeScreen _upgradeScreen;
        private bool _isInitialized = false;

        public event Action OnMenuWorkHasOver;

        #region MonoBehaviour

        private void OnEnable()
        {
            _playButton.OnClicked += StartGame;
            _updgradeButton.OnClicked += LoadAndRevealUpgradeScreen;
        }

        private void OnDisable()
        {
            _playButton.OnClicked -= StartGame;
            _updgradeButton.OnClicked -= LoadAndRevealUpgradeScreen;
        }

        #endregion
        
        public void Init(
            GameStartup gameStartup,
            UpgradeScreenProvider upgradeScreenProvider,
            GameUI inGameUI,
            Score score)
        {
            _gameStartup = gameStartup;
            _upgradeScreenProvider = upgradeScreenProvider;
            _inGameUI = inGameUI;
            _isInitialized = true;

            var highScore = score.HighScore;
            _highScoreText.text = $"{HighScoreOriginalText} {highScore}";
        }

        public override void Reveal()
        {
            base.Reveal();
            TweenElements();
        }

        private void TweenElements()
        {
            var tweener = new ScreenTweener();
            
            tweener.TweenLogo(_logo);
            tweener.TweenButton(_playButton.transform);
            tweener.TweenButton(_updgradeButton.transform);
        }

        private async void LoadAndRevealUpgradeScreen()
        {
            _upgradeScreen = await _upgradeScreenProvider.Load();
            _upgradeScreen.OnScreenClosed += OnUpgradeScreenClosedHandler;

            Conceal();

            _interstitialAd.TryShowWithChance();
        }

        private void OnUpgradeScreenClosedHandler()
        {
            _upgradeScreen.OnScreenClosed -= OnUpgradeScreenClosedHandler;
            _upgradeScreenProvider.Unload();
            Reveal();
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