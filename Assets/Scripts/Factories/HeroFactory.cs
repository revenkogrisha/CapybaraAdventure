using CapybaraAdventure.Other;
using Core.Player;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly Vector3 _spawnPosition;
        private readonly Transform _parent;
        private readonly HeroSkins _heroSkins;

        public HeroFactory(
            Hero prefab,
            Vector3 spawnPosition,
            HeroSkins heroSkins,
            Transform parent = null)
        {
            _heroPrefab = prefab;
            _spawnPosition = spawnPosition;
            _parent = parent;
            _heroSkins = heroSkins;
        }

        public Hero Create()
        {
            Hero hero = DIContainerRef.Container.InstantiatePrefabForComponent<Hero>(
                _heroPrefab, 
                _spawnPosition, 
                Quaternion.identity, 
                _parent);
            
            // NEW (HeroSkins)
            if (_heroSkins.Current.Name != SkinName.Capybuddy)
                hero.SkinSetter.Set(_heroSkins.Current);

            return hero;
        }
    }
}