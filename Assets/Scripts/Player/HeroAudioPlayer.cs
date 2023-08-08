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

        public void PlayJump()
        {
            if (_jumpSound == null || _jumpSound.isPlaying == true)
                return;

            _jumpSound.Play();
        }

        public void PlayEat()
        {
            if (_eatSound == null)
                return;

            _eatSound.Play();
        }

        public void PlayCollected()
        {
            if (_collectedSound == null || _swordSound.isPlaying == true)
                return;

            _collectedSound.Play();
        }

        public void PlayGameOver()
        {
            if (_gameOverSound == null || _gameOverSound.isPlaying == true)
                return;

            _gameOverSound.Play();
        }

        public void PlaySword()
        {
            if (_swordSound == null)
                return;

            _swordSound.Play();
        }
    }
}