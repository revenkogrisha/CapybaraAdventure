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
        private readonly GameStartup _gameStartup;
        private readonly GameUI _inGameUI;

        public MenuProvider(
            Canvas canvas,
            GameStartup gameStartup,
            GameUI inGameUI)
        {
            _canvas = canvas;
            _gameStartup = gameStartup;
            _inGameUI = inGameUI;
        }

        public async Task<GameMenu> Load()
        {
            var canvasTransform = _canvas.transform;
            var menu = await LoadInternal<GameMenu>(GameMenu, canvasTransform);

            menu.Init(_gameStartup, _inGameUI);
            return menu;
        }

        public void Unload()
        {
            UnlodadInternalIfCached();
        }
    }
}
