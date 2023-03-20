using CapybaraAdventure.Game;
using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class GameStartupInstaller : MonoInstaller
    {
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private GameUI _inGameUI;

        public override void InstallBindings()
        {
            BindGameStartup();
            BindInGameUI();
        }

        private void BindGameStartup()
        {
            Container
                .Bind<GameStartup>()
                .FromInstance(_gameStartup)
                .AsSingle()
                .NonLazy();
        }

        private void BindInGameUI()
        {
            Container
                .Bind<GameUI>()
                .FromInstance(_inGameUI)
                .AsSingle()
                .NonLazy();
        }
    }
}