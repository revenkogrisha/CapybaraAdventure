using Zenject;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        private SaveService _saveService;
        private PlayerData _instance;

        private void OnDisable()
        {
            _instance.Disable();
        }

        public override void InstallBindings()
        {
            _instance = new(_saveService);

            Container
                .Bind<PlayerData>()
                .FromInstance(_instance)
                .AsSingle();
        }

        [Inject]
        private void Construct(SaveService saveService)
        {
            _saveService = saveService;
        }
    }
}