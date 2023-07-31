using UnityEngine;
using System;

namespace CapybaraAdventure.Level
{
    [Serializable]
    public class Location : MonoBehaviour
    {
        [SerializeField] private SimplePlatform[] _simplePlatforms;
        [SerializeField] private SpecialPlatform[] _specialPlatforms;

        public SimplePlatform[] SimplePlatforms => _simplePlatforms;
        public SpecialPlatform[] SpecialPlatforms => _specialPlatforms;
    }
}
