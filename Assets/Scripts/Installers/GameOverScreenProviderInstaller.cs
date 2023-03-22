using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure
{
    public class GameOverScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        public override void InstallBindings()
        {
            var provider = new GameOverScreenProvider(_canvas);
            
            Container
                .Bind<GameOverScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }
    }
}
