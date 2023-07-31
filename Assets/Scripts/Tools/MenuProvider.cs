using CapybaraAdventure.Other;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.UI
{
    public class MenuProvider : LocalAssetLoader
    {
        public const string GameMenu = nameof(GameMenu);
        
        private readonly Canvas _canvas;
        private readonly GameStartup _gameStartup;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private readonly Score _score;

        public MenuProvider(
            Canvas canvas,
            GameStartup gameStartup,
            Score score)
        {
            _canvas = canvas;
            _gameStartup = gameStartup;
            _score = score;
        }

        public void Init(UpgradeScreenProvider upgradeScreenProvider)
        {
            _upgradeScreenProvider = upgradeScreenProvider;
        }

        public async Task<GameMenu> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameMenu menu = await LoadInternal<GameMenu>(GameMenu, canvasTransform);

            menu.Init(_gameStartup, _upgradeScreenProvider, _score);
            return menu;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}
