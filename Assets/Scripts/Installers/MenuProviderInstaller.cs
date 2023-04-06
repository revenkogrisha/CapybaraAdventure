using Zenject;
using UnityEngine;
using CapybaraAdventure.Game;
using CapybaraAdventure.UI;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Installers
{
    public class MenuProviderInstaller : MonoInstaller 
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private GameUI _inGameUI;
        [Inject] private Score _score;

        public override void InstallBindings()
        {
            var provider = new MenuProvider(_canvas, _gameStartup, _inGameUI, _score);

            Container
                .Bind<MenuProvider>()
                .FromInstance(provider)
                .AsSingle();
        }
    }
}
