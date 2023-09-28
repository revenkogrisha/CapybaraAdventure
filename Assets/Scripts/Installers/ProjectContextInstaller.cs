using CapybaraAdventure.Game;
using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private Score _score;
        [SerializeField] private SaveService _saveService;
        [SerializeField] private PlayerData _playerData;

        [Inject] DiContainer container;
        
        public override void InstallBindings()
        {
            DIContainerRef.Container = container;

            var pauseManager = new PauseManager();
            Container
                .Bind<PauseManager>()
                .FromInstance(pauseManager)
                .AsSingle();

            Container
                .Bind<Score>()
                .FromInstance(_score)
                .AsSingle();

            Container
                .Bind<SaveService>()
                .FromInstance(_saveService)
                .AsSingle();

            Container
                .Bind<PlayerData>()
                .FromInstance(_playerData)
                .AsSingle();
        }
    }
}
