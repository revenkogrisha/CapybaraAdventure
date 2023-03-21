using Zenject;
using UnityEngine;
using CapybaraAdventure.Game;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Installers
{
    public class MenuProviderInstaller : MonoInstaller 
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private GameUI _inGameUI;

        public override void InstallBindings()
        {
            var provider = new MenuProvider(_canvas, _gameStartup, _inGameUI);

            Container
                .Bind<MenuProvider>()
                .FromInstance(provider)
                .AsSingle();
        }
    }
}
