using CapybaraAdventure.Game;
using CapybaraAdventure.UI;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure
{
    public class GameOverScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        private GameMenu _gameMenu;

        public override void InstallBindings()
        {
            var provider = new GameOverScreenProvider(_canvas, _gameMenu);
            
            Container
                .Bind<GameOverScreenProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        [Inject]
        private void Construct(GameMenu gameMenu)
        {
            _gameMenu = gameMenu;
        }
    }
}
