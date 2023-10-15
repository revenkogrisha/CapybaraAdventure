using CapybaraAdventure.Other;
using Zenject;

namespace CapybaraAdventure.Installers
{
    public class SceneContextInstaller : MonoInstaller
    {
        DiContainer _container;

        public override void InstallBindings()
        {
            DIContainerRef.Container = _container;
        }

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
    }
}
