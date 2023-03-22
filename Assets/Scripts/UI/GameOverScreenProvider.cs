using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Tools;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    public class GameOverScreenProvider : LocalAssetLoader
    {
        public const string GameOverScreen = nameof(GameOverScreen);

        private readonly Canvas _canvas;
        private readonly GameMenu _gameMenu;

        public GameOverScreenProvider(
            Canvas canvas,
            GameMenu gameMenu)
        {
            _canvas = canvas;
            _gameMenu = gameMenu;
        }

        public async Task<GameOverScreen> Load()
        {
            var canvasTransform = _canvas.transform;
            var gameOverScreen = await 
                LoadInternal<GameOverScreen>(GameOverScreen, canvasTransform);

            //  init
            return gameOverScreen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}