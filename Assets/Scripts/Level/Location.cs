using UnityEngine;
using System;
using Core.Level;

namespace CapybaraAdventure.Level
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Location", menuName = "Location")]
    public class Location : ScriptableObject
    {
        [SerializeField] private bool _useDefaultBackground = true;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private int _levelsToComplete = 4;
        [SerializeField] private SimplePlatform _startPlatform;
        [SerializeField] private SimplePlatform[] _simplePlatforms;
        [SerializeField] private SpecialPlatform[] _specialPlatforms;
        [SerializeField] private BackgroundPreset _backgroundPreset;

        [Space]
        [SerializeField] private GameObject _locationMenuPresentationPrefab;

        public bool UseDefaultBackground => _useDefaultBackground;
        public Color BackgroundColor => _backgroundColor;
        public SimplePlatform[] SimplePlatforms => _simplePlatforms;
        public SpecialPlatform[] SpecialPlatforms => _specialPlatforms;
        public BackgroundPreset BackgroundPreset => _backgroundPreset;
        public int LevelsToComplete => _levelsToComplete;
        public SimplePlatform StartPlatform => _startPlatform;

        public GameObject LocationMenuPresentationPrefab => _locationMenuPresentationPrefab;
    }
}
