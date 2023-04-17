using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Other;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameOverScreenProvider : LocalAssetLoader
    {
        public const string GameOverScreen = nameof(GameOverScreen);

        private readonly Canvas _canvas;
        private  GameMenu _gameMenu;
        private LoadingScreenProvider _loadingScreenProvider;

        public GameOverScreenProvider(
            Canvas canvas,
            LoadingScreenProvider loadingScreenProvider)
        {
            _canvas = canvas;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public async Task<GameOverScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameOverScreen gameOverScreen = await 
                LoadInternal<GameOverScreen>(GameOverScreen, canvasTransform);

            gameOverScreen.Init(_loadingScreenProvider);

            return gameOverScreen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}