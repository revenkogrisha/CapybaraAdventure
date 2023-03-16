using CapybaraAdventure.Player;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private Hero _heroPrefab;

        private HeroSpawnMarker _heroSpawnMarker;
        private DiContainer _diContainer;

        #region MonoBehaviour

        private void Start()
        {
            //  generate level

            CreateHero();
        }

        #endregion

        [Inject]
        public void Construct(
            HeroSpawnMarker spawnMarker,
            DiContainer container)
        {
            _heroSpawnMarker = spawnMarker;
            _diContainer = container;
        }

        private void CreateHero()
        {
            var spawnMarkerTransform = _heroSpawnMarker.transform;
            var spawnPosition = spawnMarkerTransform.position;
            
            var heroFactory = 
                new HeroFactory(_diContainer, _heroPrefab, spawnPosition);
            heroFactory.Create();
        }
    }
}
