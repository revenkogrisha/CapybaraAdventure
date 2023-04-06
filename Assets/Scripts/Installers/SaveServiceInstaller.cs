using UnityEngine;
using Zenject;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Installers
{
    public class SaveServiceInstaller : MonoInstaller
    {
        [SerializeField] private SaveService _instance;

        public override void InstallBindings()
        {
            Container
                .Bind<SaveService>()
                .FromInstance(_instance)
                .AsSingle();
        }
    }
}