using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;
using CapybaraAdventure.Other;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Installers
{
    public class GameOverScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private LoadingScreenProvider _loadingScreenProvider;

        private SaveService _saveService;

        public override void InstallBindings()
        {
            var provider = new GameOverScreenProvider(_canvas, _loadingScreenProvider, _saveService);
            
            Container
                .Bind<GameOverScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        [Inject]
        private void Construct(SaveService saveService)
        {
            _saveService = saveService;
        }
    }
}
