using UnityEngine;

namespace CapybaraAdventure.Level
{
    public abstract class SpawnMarker : MonoBehaviour
    {
        [Tooltip("Fill in value as percent")]
        [SerializeField, Range(0, 101)] private int _spawnChance = 35;

        private Transform _transform;

        public Vector3 Position => _transform.position;
        public int SpawnChance => _spawnChance;
        public GameObject SpawnedObject { get; set; }
        public bool HasActiveObject => SpawnedObject != null && SpawnedObject.activeSelf == true;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
