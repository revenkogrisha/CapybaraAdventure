using UnityEngine;

namespace Core.Level
{
    public class Background : MonoBehaviour
    {
        [SerializeField, Tooltip("Sky")] private SpriteRenderer[] _layer0;
        [SerializeField, Tooltip("Sky Pattern")] private SpriteRenderer[] _layer1;
        [SerializeField, Tooltip("Middle, main decor")] private SpriteRenderer[] _layer2;
        [SerializeField, Tooltip("Middle-Fore Pattern")] private SpriteRenderer[] _layer3;
        [SerializeField] private SpriteRenderer[] _undercover;

        public void ApplyPreset(BackgroundPreset preset)
        {
            foreach (SpriteRenderer renderer in _layer0)
                renderer.sprite = preset.Layer0;
                
            foreach (SpriteRenderer renderer in _layer1)
                renderer.sprite = preset.Layer1;

            foreach (SpriteRenderer renderer in _layer2)
                renderer.sprite = preset.Layer2;

            foreach (SpriteRenderer renderer in _layer3)
                renderer.sprite = preset.Layer3;

            foreach (SpriteRenderer renderer in _undercover)
                renderer.color = preset.UndercoverColor;
        }
    }
}