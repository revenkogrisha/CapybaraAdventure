using CapybaraAdventure.Other;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public abstract class Chest : MonoBehaviour
    {
        private const string Opened = nameof(Opened);

        [SerializeField] private ParticlesPlayer _openParticles;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _deactivateDelay = 2f;

        private readonly int _openedId = Animator.StringToHash(Opened);

        public virtual void Open()
        {
            _openParticles.SpawnTillDuration();
            _animator.SetTrigger(_openedId);
            
            Invoke(nameof(Deactivate), _deactivateDelay);
        }

        private void Deactivate() => gameObject.SetActive(false);
    }
}
