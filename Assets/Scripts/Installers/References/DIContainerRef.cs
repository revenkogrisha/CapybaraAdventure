using UnityEngine;
using Zenject;

namespace CapybaraAdventure.Other
{
    // Of course not the best solution, but works for this project size
    // and makes initialization a little bit more neat.
    public class DIContainerRef : MonoBehaviour
    {
        private static DiContainer container;

        public static DiContainer Container
        {
            get => container;
            set => container ??= value;
        }

        private void OnDestroy()
        {
            container = null;
        }
    }
}
