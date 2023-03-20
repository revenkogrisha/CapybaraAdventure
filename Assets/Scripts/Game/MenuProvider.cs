using CapybaraAdventure.Tools;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class MenuProvider : LocalAssetLoader
    {
        public const string GameMenu = nameof(GameMenu);
        
        private readonly Canvas _canvas;
        private readonly DiContainer _diContainer;

        public MenuProvider(
            Canvas canvas,
            DiContainer diContainer)
        {
            _canvas = canvas;
            _diContainer = diContainer;
        }

        public async Task<GameMenu> Load()
        {
            var canvasTransform = _canvas.transform;
            var menu = await LoadInternal<GameMenu>(GameMenu, canvasTransform);

            menu.InjectFields(_diContainer);
            return menu;
        }

        public void Unload()
        {
            UnlodadInternalIfCached();
        }
    }
}
