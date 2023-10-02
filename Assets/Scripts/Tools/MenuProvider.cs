using CapybaraAdventure.Other;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.Player;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class MenuProvider : LocalAssetLoader
    {
        public const string GameMenu = nameof(GameMenu);
        
        private readonly Canvas _canvas;
        private readonly GameStartup _gameStartup;
        private UpgradeScreenProvider _upgradeScreenProvider;
        private readonly Score _score;

        [Inject]
        public MenuProvider(
            Canvas canvas,
            UpgradeScreenProvider upgradeScreenProvider,
            GameStartup gameStartup,
            Score score)
        {
            _canvas = canvas;
            _upgradeScreenProvider = upgradeScreenProvider;
            _gameStartup = gameStartup;
            _score = score;
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
