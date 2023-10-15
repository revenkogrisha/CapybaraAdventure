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
            BindSaveSystem();
            BindPauseManager();
            BindScore();
            BindPlayerData();
            BindLocalizationManager();
            BindSaveService();
        }

        private void BindSaveSystem()
        {
            Container
                .Bind<ISaveSystem>()
                .To<JsonSaveSystem>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }

        private void BindPauseManager()
        {
            Container
                .Bind<PauseManager>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }

        private void BindScore()
        {
            Container
                .Bind<Score>()
                .FromInstance(_score)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerData()
        {
            Container
                .Bind<PlayerData>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindLocalizationManager()
        {
            Container
                .Bind<LocalizationManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindSaveService()
        {
            Container
                .Bind<SaveService>()
                .FromInstance(_saveService)
                .AsSingle()
                .NonLazy();
        }
    }
}
