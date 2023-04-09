using UnityEngine;

namespace CapybaraAdventure.Level
{
    public class ChestSpawnMarker : MonoBehaviour
    {
        [Tooltip("Fill in value as percent")]
        [SerializeField] private int _spawnChance = 60;

        private Transform _transform;

        public Vector3 Position => _transform.position;
        public int SpawnChance => _spawnChance;

        private void Awake()
        {
            _transform = transform;
        }
    }
}