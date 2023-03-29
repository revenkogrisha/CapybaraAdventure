using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;
using Cinemachine;
using CapybaraAdventure.UI;
using System;
using CapybaraAdventure.Level;
using System.Threading.Tasks;

namespace CapybaraAdventure.Game
{
    [DisallowMultipleComponent]
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private Hero _heroPrefab;
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private FollowerObject _deadlyYBorder;
        [SerializeField] private ScoreText _scoreText;

        #region Injected Variables
        private JumpButton _jumpButton;
        private JumpSlider _jumpSlider;
        private MenuProvider _menuProvider;
        private GameMenu _menu;
        private Score _score;
        private LevelGenerator _levelGenerator;
        private HeroSpawnMarker _heroSpawnMarker;
        private PauseManager _pauseManager;
        private GameUI _inGameUI;
        private DiContainer _diContainer;
        #endregion

        private GameOverScreenProvider _gameOverScreenProvider;
        private ScorePresenter _scorePresenter;
        private GameOverHandler _gameOverHandler;
        private GameOverHandlerPresenter _gameOverHandlerPresenter;
        private HeroPresenter _heroPresenter;

        #region MonoBehaviour

        private void OnDisable()
        {
            _menu.OnMenuWorkHasOver -= UnloadMenu;
            _gameOverHandler?.Disable();
            _heroPresenter?.Disable();
            _gameOverHandlerPresenter?.Disable();
            _scorePresenter?.Disable();
        }

        private async void Start()
        {
            await LoadAndRevealMenu();
            _levelGenerator.SpawnStartPlatform();
            _levelGenerator.GenerateDefaultAmount();
        }

        #endregion

        [Inject]
        private void Construct(
            JumpButton button,
            JumpSlider slider,
            MenuProvider menuProvider,
            Score score,
            LevelGenerator levelGenerator,
            HeroSpawnMarker spawnMarker,
            PauseManager pauseManager,
            GameUI inGameUI,
            GameOverScreenProvider gameOverScreenProvider,
            DiContainer diContainer)
        {
            _jumpButton = button;
            _jumpSlider = slider;
            _menuProvider = menuProvider;
            _score = score;
            _levelGenerator = levelGenerator; 
            _heroSpawnMarker = spawnMarker;
            _pauseManager = pauseManager;
            _inGameUI = inGameUI;
            _gameOverScreenProvider = gameOverScreenProvider;
            _diContainer = diContainer;
        }

        public void StartGame()
        {
            _pauseManager.SetPaused(false);

            var hero = CreateHero();

            _heroPresenter = new HeroPresenter(hero, _jumpButton, _jumpSlider);
            _heroPresenter.Enable();

            SetupCamera(hero);
            SetupScore(hero);

            SetupDeadlyYBorder(hero);

            _levelGenerator.InitHeroTransform(hero);

            SetupGameOverSystem(hero);
        }

        private async Task LoadAndRevealMenu()
        {
            _menu = await _menuProvider.Load();
            _menu.OnMenuWorkHasOver += UnloadMenu;

            _menu.Reveal();
        }

        private void UnloadMenu() => _menuProvider.Unload();

        private Hero CreateHero()
        {
            var spawnMarkerTransform = _heroSpawnMarker.transform;
            var spawnPosition = spawnMarkerTransform.position;
            
            var heroFactory = 
                new HeroFactory(_diContainer, _heroPrefab, spawnPosition);
                
            return heroFactory.Create();
        }

        private void SetupCamera(Hero hero)
        {
            var heroTransform = hero.transform;
            _mainCamera.Follow = heroTransform;
        }

        private void SetupScore(Hero hero)
        {
            _score.Init(hero);
            _score.StartCount();

            _scorePresenter = new ScorePresenter(_score, _scoreText);
            _scorePresenter.Enable();
        }

        private void SetupDeadlyYBorder(Hero hero)
        {
            var heroObject = hero.gameObject;
            _deadlyYBorder.Init(heroObject);
        }

        private void SetupGameOverSystem(Hero hero)
        {
            InitGameOverHandler(hero);
            InitGameOverHandlerPresenter();
        }

        private void InitGameOverHandler(Hero hero)
        {
            _gameOverHandler = new GameOverHandler(hero, _pauseManager);
            _gameOverHandler.Enable();
        }

        private void InitGameOverHandlerPresenter()
        {
            if (_gameOverHandler == null)
                throw new NullReferenceException("GameOverHandler instance is null! Init it first because it's needed in GameOverHandlerPresenter initialization.");

            _gameOverHandlerPresenter = new GameOverHandlerPresenter(
                _gameOverHandler,
                _inGameUI,
                _gameOverScreenProvider,
                _score);

            _gameOverHandlerPresenter.Enable();
        }
    }
}