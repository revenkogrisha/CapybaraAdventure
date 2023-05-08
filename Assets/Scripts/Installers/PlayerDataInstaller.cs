using Zenject;
using CapybaraAdventure.Player;
using UnityEngine;

namespace CapybaraAdventure.Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        [SerializeField] private PlayerData _instance;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .FromInstance(_instance)
                .AsSingle();
        }
    }
}