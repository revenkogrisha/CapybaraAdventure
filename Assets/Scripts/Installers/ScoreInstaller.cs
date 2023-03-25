using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure
{
    public class ScoreInstaller : MonoInstaller
    {
        [SerializeField] private Score _instance;

        public override void InstallBindings()
        {
            Container
                .Bind<Score>()
                .FromInstance(_instance)
                .AsSingle();
        }
    }
}