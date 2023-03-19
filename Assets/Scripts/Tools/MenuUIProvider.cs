using CapybaraAdventure.Game;
using System.Threading.Tasks;

namespace CapybaraAdventure.Tools
{
    public class MenuUIProvider : LocalAssetLoader
    {
        public const string GameMenu = nameof(GameMenu);

        public Task<GameMenu> Load()
        {
            return LoadInternal<GameMenu>(GameMenu);
        }

        public void Unload()
        {
            UnlodadInternalIfCached();
        }
    }
}
