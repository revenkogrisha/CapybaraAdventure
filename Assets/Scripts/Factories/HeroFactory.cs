using CapybaraAdventure.Other;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly Vector3 _spawnPosition;
        private readonly Transform _parent;

        public HeroFactory(
            Hero prefab,
            Vector3 spawnPosition,
            Transform parent = null)
        {
            _heroPrefab = prefab;
            _spawnPosition = spawnPosition;
            _parent = parent;
        }

        public Hero Create()
        {
            Hero hero = DIContainerRef.Container
                .InstantiatePrefabForComponent<Hero>(
                    _heroPrefab, 
                    _spawnPosition, 
                    Quaternion.identity, 
                    _parent);

            return hero;
        }
    }
}
