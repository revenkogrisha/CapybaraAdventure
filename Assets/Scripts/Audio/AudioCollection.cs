using System.Linq;
using UnityEngine;

namespace Core.Audio
{
    [CreateAssetMenu(fileName = "Audio Collection", menuName = "Collections/Audio")]
    public class AudioCollection : ScriptableObject
    {
        [SerializeField] private NamedAudio[] _audio;

        public NamedAudio[] Audio => _audio;

        private void OnValidate()
        {
            bool isUnique = _audio.GroupBy(x => x.Name).All(grouping => grouping.Count() == 1);

            if (isUnique == false)
                throw new UnityException("Audio collection contains duplicates.");
        }
    }
}
