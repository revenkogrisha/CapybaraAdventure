using UnityEngine;
using Zenject;
using CapybaraAdventure.Level;

namespace CapybaraAdventure.Installers
{
    public class LevelGeneratorInstaller : MonoInstaller
    {
        [SerializeField] private LevelGenerator _instance;

        public override void InstallBindings()
        {
            Container   
                .Bind<LevelGenerator>()
                .FromInstance(_instance)
                .AsSingle();
        }
    }
}
