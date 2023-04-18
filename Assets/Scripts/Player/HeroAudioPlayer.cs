using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _jumpSound;
        [SerializeField] private AudioSource _eatSound;

        public void PlayJump()
        {
            if (_jumpSound == null
                || _jumpSound.isPlaying)
                return;

            _jumpSound.Play();
        }

        public void PlayEat()
        {
            if (_eatSound == null)
                return;

            _eatSound.Play();
        }
    }
}
