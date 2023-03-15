using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;

        public HeroFactory(Hero prefab)
        {
            _heroPrefab = prefab;
        }

        public Hero Create()
        {
            var diContainer = new DiContainer();
            var hero = diContainer
                .InstantiatePrefabForComponent<Hero>(_heroPrefab);

            return hero;
        }
    }
}
