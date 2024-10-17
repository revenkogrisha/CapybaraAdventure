using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "Skins Collection", menuName = "Collections/Skins")]
    public class SkinsCollection : ScriptableObject
    {
        [SerializeField] private SkinPreset[] _presets;

        public SkinPreset[] Presets => _presets;
    }
}