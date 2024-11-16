using Core.Audio;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _jumpSound;
        [SerializeField] private AudioSource _eatSound;
        [SerializeField] private AudioSource _collectedSound;
        [SerializeField] private AudioSource _gameOverSound;
        [SerializeField] private AudioSource _swordSound;
        
        public IAudioHandler AudioHandler { get; set; }

        // NEW
        public void PlayGameFinished()
        {
            AudioHandler.PlaySound(AudioName.GameWin);
        }
        
        public void PlayJump()
        {
            // if (_jumpSound == null || _jumpSound.isPlaying == true)
            //     return;
            //
            // _jumpSound.Play();
            
            AudioHandler.PlaySound(AudioName.HeroJump);
        }

        public void PlayEat()
        {
            // if (_eatSound == null)
            //     return;
            //
            // _eatSound.Play();
            
            AudioHandler.PlaySound(AudioName.FoodBite);
        }

        public void PlayCollected()
        {
            // if (_collectedSound == null || _swordSound.isPlaying == true)
            //     return;
            //
            // _collectedSound.Play();
            
            AudioHandler.PlaySound(AudioName.HeroCollect);
        }

        public void PlayGameOver()
        {
            // if (_gameOverSound == null || _gameOverSound.isPlaying == true)
            //     return;
            //
            // _gameOverSound.Play();
            
            AudioHandler.PlaySound(AudioName.GameLost);
        }

        public void PlaySword()
        {
            // if (_swordSound == null)
            //     return;
            //
            // _swordSound.Play();
            
            AudioHandler.PlaySound(AudioName.HeroGrapple);
        }
    }
}