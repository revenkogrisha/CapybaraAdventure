using CapybaraAdventure.Tools;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class PauseScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        public override void InstallBindings()
        {
            var provider = new PauseScreenProvider(_canvas);
            
            Container
                .Bind<PauseScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }
    }
}
