using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class LoadingScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenProvider _provider;

        public override void InstallBindings()
        {
            Container
                .Bind<LoadingScreenProvider>()
                .FromInstance(_provider)
                .AsSingle();
        }
    }
}
