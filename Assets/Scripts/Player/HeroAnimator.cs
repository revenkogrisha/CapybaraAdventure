using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class HeroAnimator
    {
        public const string IsJumping = nameof(IsJumping);
        public const string GotSword = nameof(GotSword);
        public const string Hit = nameof(Hit);
        
        private Animator _animator;
        private readonly int _isJumpingHash = Animator.StringToHash(IsJumping);
        private readonly int _gotSwordHash = Animator.StringToHash(GotSword);
        private readonly int _hit = Animator.StringToHash(Hit);

        public HeroAnimator(Animator heroAnimator)
        {
            _animator = heroAnimator;
        }

        public void SetAsJumping() => _animator.SetBool(_isJumpingHash, true);

        public void EndJumping() => _animator.SetBool(_isJumpingHash, false);

        public void GetSword() => _animator.SetTrigger(_gotSwordHash);

        public void DoHit() => _animator.SetTrigger(_hit);
    }
}
