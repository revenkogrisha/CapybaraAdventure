using System;
using CapybaraAdventure.Level;
using CapybaraAdventure.UI;
using UnityEngine;
using TMPro;
using CapybaraAdventure.Player;
using CapybaraAdventure.Other;
using UnityEngine.Localization.Components;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CapybaraAdventure.Game
{
    public class GameMenu : UIBase
    {
        [SerializeField] private UIButton _playButton;
        [FormerlySerializedAs("_updgradeButton")] [SerializeField] private UIButton _upgradeButton;
        [SerializeField] private UIButton _shopButton;
        [SerializeField] private UIButton _languageButton;

        [Space]
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _levelNumberText;
        [SerializeField] private Transform _logo;
        [SerializeField] private LocalizeStringEvent _highScoreLocalization;
        
        [Header("Level Progression")]
        [SerializeField] private TMP_Text _levelsToCompleteText;
        [SerializeField] private Slider _locationProgressBar;
        
        [Header("Location Presentations")]
        [SerializeField] private LocationsCollection _locations;
        [SerializeField] private Transform _presentationsContainer;

        private GameStartup _gameStartup;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private ShopScreenProvider _shopScreenProvider;
        private UpgradeScreen _upgradeScreen;
        private ShopMenuScreen _shopScreen;
        private Score _score;
        private LocalizationManager _localization;
        private bool _isInitialized = false;
        private LevelNumberHolder _levelNumberHolder;
        private bool _locationPresented = false;

        public event Action OnMenuWorkHasOver;

        #region MonoBehaviour

        private void OnEnable()
        {
            _playButton.SetActive(false);
            _upgradeButton.SetActive(false);
            _languageButton.SetActive(false);
            _shopButton.SetActive(false);
            
            _playButton.OnClicked += StartGame;
            _upgradeButton.OnClicked += LoadAndRevealUpgradeScreen;
            _shopButton.OnClicked += LoadAndRevealShopScreen;
            _languageButton.OnClicked += ChangeLanguage;
            _highScoreLocalization.OnUpdateString.AddListener(SetupScoreText);
        }

        private void OnDisable()
        {
            _playButton.OnClicked -= StartGame;
            _upgradeButton.OnClicked -= LoadAndRevealUpgradeScreen;
            _shopButton.OnClicked -= LoadAndRevealShopScreen;
            _languageButton.OnClicked -= ChangeLanguage;
            _highScoreLocalization.OnUpdateString.RemoveListener(SetupScoreText);
        }

        #endregion
        
        public void Init(
            GameStartup gameStartup,
            UpgradeScreenProvider upgradeScreenProvider,
            ShopScreenProvider shopScreenProvider,
            Score score,
            LocalizationManager localization,
            LevelNumberHolder levelNumberHolder)
        {
            _gameStartup = gameStartup;
            _upgradeScreenProvider = upgradeScreenProvider;
            _shopScreenProvider = shopScreenProvider;
            _isInitialized = true;
            _score = score;
            _localization = localization;
            _levelNumberHolder = levelNumberHolder;
        }

        public override void Reveal()
        {
            _levelsToCompleteText.text = _levelNumberHolder.LevelsToComplete.ToString();
            _locationProgressBar.maxValue = _levelNumberHolder.LevelsToComplete;
            _locationProgressBar.value = _levelNumberHolder.Level - 1;
            if (_levelNumberText != null)
                _levelNumberText.text = (_levelNumberHolder.Level - 1).ToString();

            if (_locationPresented == false)
            {
                LocationPresentation locationPresentation =
                    _locations.Collection[_levelNumberHolder.LocationNumber].LocationMenuPresentationPrefab;
                
                Instantiate(locationPresentation, _presentationsContainer);
                _locationPresented = true;
            }
            
            base.Reveal();
            
            _playButton.SetActive(true);
            _upgradeButton.SetActive(true);
            _languageButton.SetActive(true);
            // _shopButton.SetActive(true);
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
            tweener.TweenButton(_upgradeButton.transform);
            tweener.TweenButton(_languageButton.transform);
            
            // NEW
            tweener.TweenButton(_presentationsContainer.transform);
            // tweener.TweenButton(_shopButton.transform);
            tweener.TweenProgressBar(_locationProgressBar.transform);
        }

        private async void LoadAndRevealUpgradeScreen()
        {
            _upgradeScreen = await _upgradeScreenProvider.Load();
            _upgradeScreen.OnScreenClosed += HandleOnUpgradeScreenClosed;

            Conceal();
        }
        
        private async void LoadAndRevealShopScreen()
        {
            _shopScreen = await _shopScreenProvider.Load();
            _shopScreen.OnScreenClosed += HandleOnShopScreenClosed;

            Conceal();
        }

        private void HandleOnUpgradeScreenClosed()
        {
            _upgradeScreen.OnScreenClosed -= HandleOnUpgradeScreenClosed;
            _upgradeScreenProvider.Unload();
            Reveal();
        }
        
        private void HandleOnShopScreenClosed()
        {
            _shopScreen.OnScreenClosed -= HandleOnShopScreenClosed;
            _shopScreenProvider.Unload();
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