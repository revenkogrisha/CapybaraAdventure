using UnityEngine;

namespace Core.Audio
{
    [CreateAssetMenu(fileName = "Audio Config", menuName = "Configs/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField, Range(-80f, 20f)] private float _volumeOffValue = -80f;

        [Space]
        [SerializeField, Range(-80f, 20f)] private float _musicVolume = -15f;
        [SerializeField, Range(-80f, 20f)] private float _soundsVolume = 0f;

        public float VolumeOffValue => _volumeOffValue;

        public float MusicVolume => _musicVolume;
        public float SoundsVolume => _soundsVolume;
    }
}