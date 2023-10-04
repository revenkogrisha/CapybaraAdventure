using CapybaraAdventure.Other;
using Zenject;

namespace CapybaraAdventure
{
    public class SceneContextInstaller : MonoInstaller
    {
        [Inject] DiContainer container;

        public override void InstallBindings()
        {
            DIContainerRef.Container = container;
        }
    }
}
