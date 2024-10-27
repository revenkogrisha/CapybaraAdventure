using System.Threading.Tasks;
using CapybaraAdventure.Other;
using CapybaraAdventure.Save;
using Core.Audio;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameOverScreenProvider : LocalAssetLoader
    {
        public const string GameOverScreen = nameof(GameOverScreen);

        private readonly Canvas _canvas;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly SaveService _saveService;
        private readonly IAudioHandler _audioHandler;

        [Inject]
        public GameOverScreenProvider(
            Canvas canvas,
            LoadingScreenProvider loadingScreenProvider,
            SaveService saveService,
            IAudioHandler audioHandler)
        {
            _canvas = canvas;
            _loadingScreenProvider = loadingScreenProvider;
            _saveService = saveService;
            _audioHandler = audioHandler;
        }

        public async Task<GameOverScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameOverScreen gameOverScreen = await 
                LoadInternal<GameOverScreen>(GameOverScreen, canvasTransform);

            gameOverScreen.Init(_saveService, _loadingScreenProvider);
            gameOverScreen.InitAudioHandler(_audioHandler);

            return gameOverScreen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}