using System.Threading.Tasks;
using CapybaraAdventure.Game;
using CapybaraAdventure.Other;
using CapybaraAdventure.Save;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameOverScreenProvider : LocalAssetLoader
    {
        public const string GameOverScreen = nameof(GameOverScreen);

        private readonly Canvas _canvas;
        private readonly  GameMenu _gameMenu;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly SaveService _saveService;

        public GameOverScreenProvider(
            Canvas canvas,
            LoadingScreenProvider loadingScreenProvider,
            SaveService saveService)
        {
            _canvas = canvas;
            _loadingScreenProvider = loadingScreenProvider;
            _saveService = saveService;
        }

        public async Task<GameOverScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameOverScreen gameOverScreen = await 
                LoadInternal<GameOverScreen>(GameOverScreen, canvasTransform);

            gameOverScreen.Init(_saveService, _loadingScreenProvider);

            return gameOverScreen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}