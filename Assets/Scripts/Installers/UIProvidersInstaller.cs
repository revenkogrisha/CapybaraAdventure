using Zenject;
using UnityEngine;
using CapybaraAdventure.Other;
using CapybaraAdventure.Save;
using CapybaraAdventure.UI;
using CapybaraAdventure.Player;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.Installers
{
    public class UIProvidersInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private LoadingScreenProvider _loadingScreenProvider;

        private SaveService _saveService;
        private PlayerData _playerData;
        private PauseManager _pauseManager;
        private Score _score;
        
        public override void InstallBindings()
        {
            BindLoadingScreenProvider();
            BindUpgradeScreenProvider();
            BindGameOverScreenProvider();
            BindPauseScreenProvider();
            BindMenuProvider();
        }

        [Inject]
        private void Construct(
            SaveService saveService, 
            PlayerData playerData,
            PauseManager pauseManager,
            Score score)
        {
            _saveService = saveService;
            _playerData = playerData;
            _pauseManager = pauseManager;
            _score = score;
        }

        public void BindLoadingScreenProvider()
        {
            Container
                .Bind<LoadingScreenProvider>()
                .FromInstance(_loadingScreenProvider)
                .AsSingle();
        }

        public void BindUpgradeScreenProvider()
        {
            var instance = new UpgradeScreenProvider(
                _canvas,
                _playerData,
                _saveService,
                _loadingScreenProvider);
                
            Container
                .Bind<UpgradeScreenProvider>()
                .FromInstance(instance)
                .AsSingle();
        }

        public void BindGameOverScreenProvider()
        {
            var provider = new GameOverScreenProvider(_canvas, _loadingScreenProvider, _saveService);
            
            Container
                .Bind<GameOverScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        public void BindPauseScreenProvider()
        {
            var provider = new PauseScreenProvider(
                _canvas,
                _pauseManager,
                _gameUI,
                _loadingScreenProvider);
            
            Container
                .Bind<PauseScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        public void BindMenuProvider()
        {
            var provider = new MenuProvider(
                _canvas,
                _gameStartup,
                _score);

            Container
                .Bind<MenuProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

    }
}