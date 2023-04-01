using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroAnimator
    {
        public const string IsJumping = nameof(IsJumping);
        
        private Animator _animator;
        private readonly int _isJumpingHash = Animator.StringToHash(IsJumping);

        public HeroAnimator(Animator heroAnimator)
        {
            _animator = heroAnimator;
        }

        public void SetAsJumping() => _animator.SetBool(_isJumpingHash, true);
        public void EndJumping() => _animator.SetBool(_isJumpingHash, false);
    }
}
