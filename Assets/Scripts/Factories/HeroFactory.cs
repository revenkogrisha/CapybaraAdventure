using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly Vector3 _spawnPosition;

        public HeroFactory(Hero prefab, Vector3 spawnPosition)
        {
            _heroPrefab = prefab;
            _spawnPosition = spawnPosition;
        }

        public Hero Create()
        {
            var diContainer = new DiContainer();
            var hero = diContainer
                .InstantiatePrefabForComponent<Hero>(_heroPrefab, _spawnPosition, Quaternion.identity, null);

            return hero;
        }
    }
}
