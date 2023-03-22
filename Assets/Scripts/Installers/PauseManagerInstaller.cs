using Zenject;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.Installers
{
    public class PauseManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var pauseManager = new PauseManager();

            Container
                .Bind<PauseManager>()
                .FromInstance(pauseManager)
                .AsSingle();
        }
    }
}