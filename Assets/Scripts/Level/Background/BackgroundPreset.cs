using UnityEngine;

namespace Core.Level
{
    [CreateAssetMenu(fileName = "New Background Preset", menuName = "Presets/Background")]
    public class BackgroundPreset : ScriptableObject
    {
        [Header("Sky")]
        [SerializeField] private Sprite _layer0;

        [Header("Sky Pattern")]
        [SerializeField] private Sprite _layer1;

        [Header("Middle, main decor")]
        [SerializeField] private Sprite _layer2;

        [Header("Middle-Fore Pattern")]
        [SerializeField] private Sprite _layer3;

        [Header("Undercover")]
        [SerializeField] private Color _undercoverColor = Color.white;

        public Sprite Layer0 => _layer0;
        public Sprite Layer1 => _layer1;
        public Sprite Layer2 => _layer2;
        public Sprite Layer3 => _layer3;
        public Color UndercoverColor => _undercoverColor;
    }
}