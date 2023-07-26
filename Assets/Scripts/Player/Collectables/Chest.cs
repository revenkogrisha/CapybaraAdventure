using UnityEngine;

namespace CapybaraAdventure.Player
{
    public abstract class Chest : MonoBehaviour
    {
        private const string Opened = nameof(Opened);
        private const float DeactivateDelay = 2f;

        [SerializeField] private ParticlesPlayer _openParticles;
        [SerializeField] private Animator _animator;

        private readonly int _openedId = Animator.StringToHash(Opened);

        public virtual void Open()
        {
            _openParticles.SpawnTillDuration();
            _animator.SetTrigger(_openedId);
            
            Invoke(nameof(Deactivate), DeactivateDelay);
        }

        private void Deactivate() => gameObject.SetActive(false);
    }
}
