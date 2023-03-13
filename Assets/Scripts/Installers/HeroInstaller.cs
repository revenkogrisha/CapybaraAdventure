using UnityEngine;
using Zenject;
using CapybaraAdventure.Player;

public class MyPlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<HeroJump>().AsSingle();
    }
}