using Zenject;
using CapybaraAdventure.Player;

public class HeroInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<HeroJump>()
            .AsSingle();
    }
}