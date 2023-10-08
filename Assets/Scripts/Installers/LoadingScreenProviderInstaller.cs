using CapybaraAdventure.Other;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class LoadingScreenProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<LoadingScreenProvider>()
                .FromNew()
                .AsSingle();
        }
    }
}
