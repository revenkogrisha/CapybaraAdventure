using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class Platform : MonoBehaviour
    {
        [Header("Spawn position markers")]
        [SerializeField] private FoodSpawnMarker[] _foodMarkers;
        [SerializeField] private ChestSpawnMarker[] _chestMarkers;

        public FoodSpawnMarker[] FoodMarkers => _foodMarkers;
        public ChestSpawnMarker[] ChestMarkers => _chestMarkers;
    }
}
