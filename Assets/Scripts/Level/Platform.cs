using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private FoodSpawnMarker[] _foodMarkers;

        public FoodSpawnMarker[] FoodMarkers => _foodMarkers;
    }
}
