using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;
using CapybaraAdventure.Game;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Installers
{
    public class PauseScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameUI _inGameUI;
        [SerializeField] private LoadingScreenProvider _loadingScreenProvider;
        
        private PauseManager _pauseManager;

        public override void InstallBindings()
        {
            var provider = new PauseScreenProvider(
                _canvas,
                _pauseManager,
                _inGameUI,
                _loadingScreenProvider);
            
            Container
                .Bind<PauseScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }
    }
}
