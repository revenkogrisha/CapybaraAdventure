using Zenject;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        private SaveService _saveService;

        public override void InstallBindings()
        {
            var data = new PlayerData(_saveService);

            Container
                .Bind<PlayerData>()
                .FromInstance(data)
                .AsSingle();
        }

        [Inject]
        private void Construct(SaveService saveService)
        {
            _saveService = saveService;
        }
    }
}