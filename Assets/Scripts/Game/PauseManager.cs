using System.Collections.Generic;

namespace CapybaraAdventure.Game
{
    public class PauseManager : IPauseHandler
    {
        private readonly List<IPauseHandler> _handlers =
            new List<IPauseHandler>();

        public void Register(IPauseHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Unregister(IPauseHandler handler)
        {
            _handlers.Remove(handler);
        }

        public bool IsPaused { get; private set; }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;

            foreach (var handler in _handlers)
                handler.SetPaused(isPaused);
        }
    }
}
