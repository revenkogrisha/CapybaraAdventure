using Zenject;

namespace CapybaraAdventure.Other
{
    // Of course not the best solution, but works for this project size
    // and makes initialization a little bit more neat.
    public static class DIContainerRef
    {
        private static DiContainer container;

        public static DiContainer Container
        {
            get => container;
            set => container ??= value;
        }
    }
}
