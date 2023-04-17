using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Installers
{
    public class GameOverScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private LoadingScreenProvider _loadingScreenProvider;

        public override void InstallBindings()
        {
            var provider = new GameOverScreenProvider(_canvas, _loadingScreenProvider);
            
            Container
                .Bind<GameOverScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }
    }
}
