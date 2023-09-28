using CapybaraAdventure.Other;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly Vector3 _spawnPosition;

        public HeroFactory(
            Hero prefab,
            Vector3 spawnPosition)
        {
            _heroPrefab = prefab;
            _spawnPosition = spawnPosition;
        }

        public Hero Create()
        {
            Hero hero = DIContainerRef.Container
                .InstantiatePrefabForComponent<Hero>(
                    _heroPrefab, 
                    _spawnPosition, 
                    Quaternion.identity, 
                    null);

            return hero;
        }
    }
}
