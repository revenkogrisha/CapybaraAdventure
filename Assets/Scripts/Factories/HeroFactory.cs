using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly Vector3 _spawnPosition;
        private DiContainer _diContainer;

        public HeroFactory(
            DiContainer container,
            Hero prefab,
            Vector3 spawnPosition)
        {
            _diContainer = container;
            _heroPrefab = prefab;
            _spawnPosition = spawnPosition;
        }

        public Hero Create()
        {
            var hero = _diContainer
                .InstantiatePrefabForComponent<Hero>(_heroPrefab, _spawnPosition, Quaternion.identity, null);

            return hero;
        }
    }
}
