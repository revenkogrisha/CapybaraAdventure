using UnityEngine;

namespace CapybaraAdventure.Level
{
    [CreateAssetMenu(fileName = "LocationsCollection", menuName = "Collections/Locations")]
    public class LocationsCollection : ScriptableObject
    {
        [SerializeField] private Location[] _collection;

        public Location[] Collection => _collection;
    }
}