using CapybaraAdventure.Game;
using CapybaraAdventure.Level;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using Core.Audio;
using Core.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private Score _score;
        [SerializeField] private SaveService _saveService;
        
        [Space]
        [SerializeField] private SkinsCollection _skinsCollection;
        [SerializeField] private UnityAudioHandler _unityAudioHandler;
        
        public override void InstallBindings()
        {
            // NEW
            BindLevelNumberHolder();
            
            BindSaveSystem();
            BindPauseManager();
            // NEW
            BindLevelPlaythrough();
            BindScore();
            BindPlayerData();
            
            BindLocalizationManager();
            BindSaveService();
            
            // NEW
            BindHeroSkins();
            BindAudioHandler();
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

        private void BindLevelNumberHolder()
        {
            Container
                .Bind<LevelNumberHolder>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseManager()
        {
            Container
                .Bind<PauseManager>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }
        
        private void BindLevelPlaythrough()
        {
            Container
                .Bind<LevelPlaythrough>()
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

        private void BindHeroSkins()
        {
            Container
                .Bind<HeroSkins>()
                .FromNew()
                .AsSingle()
                .WithArguments(_skinsCollection)
                .NonLazy();
        }
        
        private void BindAudioHandler()
        {
            Container
                .Bind<IAudioHandler>()
                .To<UnityAudioHandler>()
                .FromInstance(_unityAudioHandler)
                .AsSingle()
                .Lazy();
        }
    }
}
