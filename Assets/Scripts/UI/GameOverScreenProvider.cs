using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Tools;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameOverScreenProvider : LocalAssetLoader
    {
        public const string GameOverScreen = nameof(GameOverScreen);

        private readonly Canvas _canvas;
        private  GameMenu _gameMenu;

        public GameOverScreenProvider(Canvas canvas)
        {
            _canvas = canvas;
        }

        [Inject]
        private void Construct(GameMenu gameMenu)
        {
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