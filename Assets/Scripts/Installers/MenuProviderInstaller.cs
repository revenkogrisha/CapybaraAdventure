using Zenject;
using UnityEngine;
using CapybaraAdventure.Game;
using CapybaraAdventure.UI;
using CapybaraAdventure.Player;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.Installers
{
    public class MenuProviderInstaller : MonoInstaller 
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameStartup _gameStartup;
        [SerializeField] private GameUI _inGameUI;
        [SerializeField] private UpgradeScreenProvider _upgradeScreenProvider;
        
        private Score _score;

        public override void InstallBindings()
        {
            var provider = new MenuProvider(
                _canvas,
                _gameStartup,
                _inGameUI,
                _score);

            Container
                .Bind<MenuProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        [Inject]
        private void Construct(Score score)
        {
            _score = score;
        }
    }
}