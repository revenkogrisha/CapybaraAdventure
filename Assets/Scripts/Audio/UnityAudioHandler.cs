using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Audio
{
    public class UnityAudioHandler : MonoBehaviour, IAudioHandler
    {
        [Header("Assets")]
        [SerializeField] private AudioCollection _collection;

        [Header("Components")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundsSource;
        [SerializeField] private AudioSource _uiSource;

        [Header("Mixer Groups")]
        [SerializeField] private AudioMixerGroup _masterGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioMixerGroup _soundsGroup;

        private Dictionary<AudioName, AudioClip> _clips;

        public void Initialize()
        {
            _clips = _collection.Audio.ToDictionary(audio => audio.Name, audio => audio.Clip);

            _musicSource.loop = true;
            _soundsSource.loop = false;
        }

        public void StartMusic(AudioName name)
        {
            _musicSource.clip = _clips[name];
            _musicSource.Play();
        }

        public void PlaySound(AudioName name, bool isUI = false)
        {   
            if (isUI == true)
                _uiSource.PlayOneShot(_clips[name]);
            else
                _soundsSource.PlayOneShot(_clips[name]);
        }

        public void SetMasterVolume(float volume) =>
            _masterGroup.audioMixer.SetFloat(_masterGroup.name, volume);

        public void SetMusicVolume(float volume) => 
            _musicGroup.audioMixer.SetFloat(_musicGroup.name, volume);


        public void SetSoundsVolume(float volume) => 
            _soundsGroup.audioMixer.SetFloat(_soundsGroup.name, volume);
    }
}