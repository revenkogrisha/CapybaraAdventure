using Zenject;
using UnityEngine;
using CapybaraAdventure.Other;
using CapybaraAdventure.UI;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.Installers
{
    public class UIProvidersInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private GameStartup _gameStartup;
        
        public override void InstallBindings()
        {
            BindUpgradeScreenProvider();
            BindShopScreenProvider();
            BindGameOverScreenProvider();
            BindGameFinishedScreenProvider();
            BindPauseScreenProvider();
            BindMenuProvider();
        }

        private void BindUpgradeScreenProvider()
        {
            Container
                .Bind<UpgradeScreenProvider>()
                .FromNew()
                .AsSingle();
        }
        
        private void BindShopScreenProvider()
        {
            Container
                .Bind<ShopScreenProvider>()
                .FromNew()
                .AsSingle();
        }

        private void BindGameOverScreenProvider()
        {
            Container
                .Bind<GameOverScreenProvider>()
                .FromNew()
                .AsSingle();
        }
        
        private void BindGameFinishedScreenProvider()
        {
            Container
                .Bind<GameFinishedScreenProvider>()
                .FromNew()
                .AsSingle();
        }

        private void BindPauseScreenProvider()
        {
            Container
                .Bind<PauseScreenProvider>()
                .FromNew()
                .AsSingle();
        }

        private void BindMenuProvider()
        {
            Container
                .Bind<MenuProvider>()
                .FromNew()
                .AsSingle();
        }
    }
}