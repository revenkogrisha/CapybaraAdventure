using UnityEngine;
using Zenject;
using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Installers
{
    public class UpgradeScreenProviderInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        private PlayerData _playerData;
        private SaveService _saveService;

        public override void InstallBindings()
        {
            var instance = new UpgradeScreenProvider(_canvas, _playerData, _saveService);
            Container
                .Bind<UpgradeScreenProvider>()
                .FromInstance(instance)
                .AsSingle();
        }

        [Inject]
        private void Construct(PlayerData playerData, SaveService saveService)
        {
            _playerData = playerData;
            _saveService = saveService;
        }
    }
}
