using CapybaraAdventure.Player;
using UnityEngine;

namespace CapybaraAdventure
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private Hero _heroPrefab;
        [SerializeField] private HeroSpawnMarker _heroSpawnMarker;

        #region MonoBehaviour

        private void Start()
        {
            //  generate level

            CreateHero();
        }

        #endregion

        private void CreateHero()
        {
            var spawnMarkerTransform = _heroSpawnMarker.transform;
            var spawnPosition = spawnMarkerTransform.position;
            
            var heroFactory = new HeroFactory(_heroPrefab, spawnPosition);
            heroFactory.Create();
        }
    }
}
