using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

public class HeroInstaller : MonoInstaller
{
    [SerializeField] private HeroSpawnMarker _heroSpawnMarker;
    [SerializeField] private MainCamera _mainCamera;

    public override void InstallBindings()
    {
        BindHeroSpawnMarker();
        BindMainCamera();
    }

    private void BindHeroSpawnMarker()
    {
        Container
            .Bind<HeroSpawnMarker>()
            .FromInstance(_heroSpawnMarker)
            .AsSingle()
            .NonLazy();
    }

    private void BindMainCamera()
    {
        Container
            .Bind<MainCamera>()
            .FromInstance(_mainCamera)
            .AsSingle()
            .NonLazy();
    }
}