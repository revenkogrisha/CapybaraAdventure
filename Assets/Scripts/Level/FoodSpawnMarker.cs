using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class FoodSpawnMarker : MonoBehaviour
    {
        private Transform _transform;

        public Vector3 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
        }
    }
}