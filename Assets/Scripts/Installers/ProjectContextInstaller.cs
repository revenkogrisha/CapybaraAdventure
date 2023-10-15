using CapybaraAdventure.Game;
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
        
        public override void InstallBindings()
        {
            Container
                .Bind<ISaveSystem>()
                .To<JsonSaveSystem>()
                .FromNew()
                .AsSingle()
                .Lazy();

            Container
                .Bind<PauseManager>()
                .FromNew()
                .AsSingle()
                .Lazy();

            Container
                .Bind<Score>()
                .FromInstance(_score)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PlayerData>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<SaveService>()
                .FromInstance(_saveService)
                .AsSingle()
                .NonLazy();
        }
    }
}
