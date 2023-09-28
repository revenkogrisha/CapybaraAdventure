using Zenject;

namespace CapybaraAdventure.Other
{
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
