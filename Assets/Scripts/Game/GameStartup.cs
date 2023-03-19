using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;
using Cinemachine;

namespace CapybaraAdventure
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private Hero _heroPrefab;
        [SerializeField] private CinemachineVirtualCamera _mainCamera;

        private HeroSpawnMarker _heroSpawnMarker;
        private DiContainer _diContainer;

        #region MonoBehaviour

        private void Start()
        {
            //  generate level
            var hero = CreateHero();
            SetupCamera(hero);
        }

        #endregion

        [Inject]
        private void Construct(
            HeroSpawnMarker spawnMarker,
            DiContainer container)
        {
            _heroSpawnMarker = spawnMarker;
            _diContainer = container;
        }

        private Hero CreateHero()
        {
            var spawnMarkerTransform = _heroSpawnMarker.transform;
            var spawnPosition = spawnMarkerTransform.position;
            
            var heroFactory = 
                new HeroFactory(_diContainer, _heroPrefab, spawnPosition);
                
            var hero = heroFactory.Create();
            return hero;
        }

        private void SetupCamera(Hero hero)
        {
            var heroTransform = hero.transform;
            _mainCamera.Follow = heroTransform;
        }
    }
}
