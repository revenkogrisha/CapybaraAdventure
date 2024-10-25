namespace Core.Audio
{
    public interface IAudioHandler
    {
        public void Initialize();
        public void StartMusic(AudioName name);
        public void PlaySound(AudioName name, bool isUI = false);
        public void SetMasterVolume(float volume);
        public void SetMusicVolume(float volume);
        public void SetSoundsVolume(float volume);
    }
}
