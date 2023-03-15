using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller
{
    [SerializeField] private HeroSpawnMarker _heroSpawnMarker;

    public override void InstallBindings()
    {
        BindHeroSpawnMarker();
    }

    private void BindHeroSpawnMarker()
    {
        Container
            .Bind<HeroSpawnMarker>()
            .FromInstance(_heroSpawnMarker)
            .AsSingle()
            .NonLazy();
    }
}