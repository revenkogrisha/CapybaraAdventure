using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;
using Cinemachine;
using CapybaraAdventure.UI;
using System;
using CapybaraAdventure.Level;
using System.Threading.Tasks;
using Core.Audio;
using Core.Player;

namespace CapybaraAdventure.Game
{
    [DisallowMultipleComponent]
    public class GameStartup : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Hero _heroPrefab;

        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera _mainCamera;

        [Header("Level Elements")]
        [SerializeField] private FollowerObject _deadlyYBorder;

        [Header("UI Elements")]
        [SerializeField] private UIText _scoreText;
        [SerializeField] private CoinsUIContainer _coinsUIContainer;

        #region Injected Variables
        private MenuProvider _menuProvider;
        private GameMenu _menu;
        private Score _score;
        private LevelGenerator _levelGenerator;
        private HeroSpawnMarker _heroSpawnMarker;
        private PauseManager _pauseManager;
        private GameUI _gameUI;
        private GameOverScreenProvider _gameOverScreenProvider;
        private PlayerData _playerData;
        #endregion

        private ScorePresenter _scorePresenter;
        private GameOverHandler _gameOverHandler;
        private GameOverHandlerPresenter _gameOverHandlerPresenter;
        private HeroPresenter _heroPresenter;
        private CoinsPresenter _coinsPresenter;
        private QuestPresenter _questPresenter;
        private HeroSkins _heroSkins;
        private IAudioHandler _audioHandler;

        #region MonoBehaviour

        private void OnDisable()
        {
            _menu.OnMenuWorkHasOver -= UnloadMenu;
            
            _gameOverHandler?.Disable();
            _heroPresenter?.Disable();
            _gameOverHandlerPresenter?.Disable();
            _scorePresenter?.Disable();
            _coinsPresenter?.Disable();
            _questPresenter?.Disable();
        }

        private async void Start()
        {
            _audioHandler.StartMusic(AudioName.MainTheme);
            
            _levelGenerator.CreateBackground();
            
            await LoadAndRevealMenu();
            _menu.OnMenuWorkHasOver += UnloadMenu;

            SetupCoins();

            GenerateLevel();
        }

        #endregion

        [Inject]
        private void Construct(
            MenuProvider menuProvider,
            Score score,
            LevelGenerator levelGenerator,
            HeroSpawnMarker spawnMarker,
            PauseManager pauseManager,
            GameUI gameUI,
            GameOverScreenProvider gameOverScreenProvider,
            PlayerData playerData,
            HeroSkins heroSkins,
            IAudioHandler audioHandler)
        {
            _menuProvider = menuProvider;
            _score = score;
            _levelGenerator = levelGenerator; 
            _heroSpawnMarker = spawnMarker;
            _pauseManager = pauseManager;
            _gameUI = gameUI;
            _gameOverScreenProvider = gameOverScreenProvider;
            _playerData = playerData;
            _heroSkins = heroSkins;
            _audioHandler = audioHandler;
        }

        public void StartGame()
        {
            Hero hero = CreateHero();

            _heroPresenter = new HeroPresenter(hero, _gameUI);
                 
            _heroPresenter.Enable();

            SetupCamera(hero);
            SetupScore(hero);

            _deadlyYBorder.Init(hero.transform);

            _levelGenerator.InitHeroTransform(hero);

            SetupGameOverSystem(hero);

            _pauseManager.SetPaused(false);

            _questPresenter = new QuestPresenter(_levelGenerator, _gameUI);
            _questPresenter.Enable();
            _gameUI.Reveal();
        }

        private async Task LoadAndRevealMenu()
        {
            _menu = await _menuProvider.Load();
            _menu.Reveal();
        }

        private void GenerateLevel()
        {
            _levelGenerator.SpawnStartPlatform();
            _levelGenerator.GenerateDefaultAmount();
        }

        private void UnloadMenu()
        {
            _menuProvider.Unload();
        }

        private Hero CreateHero()
        {
            Transform spawnMarkerTransform = _heroSpawnMarker.transform;
            Vector3 spawnPosition = spawnMarkerTransform.position;
            
            var heroFactory = 
                new HeroFactory(_heroPrefab, spawnPosition, _heroSkins);
                
            return heroFactory.Create();
        }

        private void SetupCamera(Hero hero)
        {
            Transform heroTransform = hero.transform;
            _mainCamera.Follow = heroTransform;
        }

        private void SetupScore(Hero hero)
        {
            _score.InitHero(hero);
            _score.StartCount();

            _scorePresenter = new ScorePresenter(_score, _scoreText);
            _scorePresenter.Enable();
        }

        private void SetupCoins()
        {
            _coinsPresenter = new(_playerData, _coinsUIContainer);
            _coinsPresenter.Init();
            _coinsPresenter.Enable();
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
                _gameUI,
                _gameOverScreenProvider,
                _score);

            _gameOverHandlerPresenter.Enable();
        }
    }
}