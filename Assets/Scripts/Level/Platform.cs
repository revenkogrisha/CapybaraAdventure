using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private FoodSpawnMarker[] _foodMarkers;
        [SerializeField] private ChestSpawnMarker[] _chestMarkers;
        [SerializeField] private EnemySpawnMarker[] _enemyMarkers;

        public FoodSpawnMarker[] FoodMarkers => _foodMarkers;
        public ChestSpawnMarker[] ChestMarkers => _chestMarkers;
        public EnemySpawnMarker[] EnemyMarkers => _enemyMarkers;
    }
}
