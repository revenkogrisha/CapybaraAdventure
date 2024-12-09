using System;
using CapybaraAdventure.Level;
using CapybaraAdventure.UI;
using UnityEngine;
using TMPro;
using CapybaraAdventure.Player;
using CapybaraAdventure.Other;
using UnityEngine.Localization.Components;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        [SerializeField] private UIButton _playButton;
        [SerializeField] private UIButton _updgradeButton;
        [SerializeField] private UIButton _languageButton;

        [Space]
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private Transform _logo;
        [SerializeField] private LocalizeStringEvent _highScoreLocalization;
        
        [Header("Level Progression")]
        [SerializeField] private TMP_Text _levelsToCompleteText;
        [SerializeField] private Slider _locationProgressBar;

        private GameStartup _gameStartup;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private UpgradeScreen _upgradeScreen;
        private Score _score;
        private LocalizationManager _localization;
        private bool _isInitialized = false;
        private LevelNumberHolder _levelNumberHolder;

        public event Action OnMenuWorkHasOver;

        #region MonoBehaviour

        private void OnEnable()
        {
            _playButton.SetActive(false);
            _updgradeButton.SetActive(false);
            _languageButton.SetActive(false);
            
            _playButton.OnClicked += StartGame;
            _updgradeButton.OnClicked += LoadAndRevealUpgradeScreen;
            _languageButton.OnClicked += ChangeLanguage;
            _highScoreLocalization.OnUpdateString.AddListener(SetupScoreText);
        }

        private void OnDisable()
        {
            _playButton.OnClicked -= StartGame;
            _updgradeButton.OnClicked -= LoadAndRevealUpgradeScreen;
            _languageButton.OnClicked -= ChangeLanguage;
            _highScoreLocalization.OnUpdateString.RemoveListener(SetupScoreText);
        }

        #endregion
        
        public void Init(
            GameStartup gameStartup,
            UpgradeScreenProvider upgradeScreenProvider,
            Score score,
            LocalizationManager localization,
            LevelNumberHolder levelNumberHolder)
        {
            _gameStartup = gameStartup;
            _upgradeScreenProvider = upgradeScreenProvider;
            _isInitialized = true;
            _score = score;
            _localization = localization;
            _levelNumberHolder = levelNumberHolder;
        }

        public override void Reveal()
        {
            base.Reveal();
            
            _levelsToCompleteText.text = _levelNumberHolder.LevelsToComplete.ToString();
            _locationProgressBar.maxValue = _levelNumberHolder.LevelsToComplete;
            _locationProgressBar.value = _levelNumberHolder.Level - 1;
            if (_levelNumberText != null)
                _levelNumberText.text = (_levelNumberHolder.Level - 1).ToString();
            
            _playButton.SetActive(true);
            _updgradeButton.SetActive(true);
            _languageButton.SetActive(true);
            TweenElements();
        }

        private async void SetupScoreText(string text)
        {
            // TODO: remake with MVP
            await UniTask.WaitUntil(() => _score != null);
            // int highScore = _score.HighScore;
            // _highScoreText.text = string.Format(text, highScore);
            
            // NEW
            int levelNumber = _levelNumberHolder.Level;
            _highScoreText.text = string.Format(text, levelNumber);
        }

        private void TweenElements()
        {
            var tweener = new ScreenTweener();
            
            tweener.TweenLogo(_logo);
            tweener.TweenButton(_playButton.transform);
            tweener.TweenButton(_updgradeButton.transform);
            tweener.TweenButton(_languageButton.transform);
        }

        private async void LoadAndRevealUpgradeScreen()
        {
            _upgradeScreen = await _upgradeScreenProvider.Load();
            _upgradeScreen.OnScreenClosed += HandleOnUpgradeScreenClosed;

            Conceal();
        }

        private void HandleOnUpgradeScreenClosed()
        {
            _upgradeScreen.OnScreenClosed -= HandleOnUpgradeScreenClosed;
            _upgradeScreenProvider.Unload();
            Reveal();
        }

        private void StartGame()
        {
            if (_isInitialized == false)
                throw new NullReferenceException("The instance of the class haven't been initialized yet! Call Init(...) first before calling StartGame()");

            // TODO: remake with EventBus/MVP...
            _gameStartup.StartGame();
            Conceal();

            OnMenuWorkHasOver?.Invoke();
        }

        private void ChangeLanguage()
        {
            _localization.NextLanguage();
        }
    }
}