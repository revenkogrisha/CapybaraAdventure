using System.Threading.Tasks;
using CapybaraAdventure.Level;
using CapybaraAdventure.Other;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using Core.Audio;
using UnityEngine;
using Zenject;

namespace CapybaraAdventure.UI
{
    public class GameFinishedScreenProvider : LocalAssetLoader
    {
        public const string GameFinishedScreen = nameof(GameFinishedScreen);

        private readonly Canvas _canvas;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly SaveService _saveService;
        private readonly LevelPlaythrough _levelPlaythrough;
        private readonly LevelNumberHolder _levelNumberHolder;
        private readonly IAudioHandler _audioHandler;

        [Inject]
        public GameFinishedScreenProvider(
            Canvas canvas,
            LoadingScreenProvider loadingScreenProvider,
            SaveService saveService,
            LevelPlaythrough levelPlaythrough,
            LevelNumberHolder levelNumberHolder,
            IAudioHandler audioHandler)
        {
            _canvas = canvas;
            _loadingScreenProvider = loadingScreenProvider;
            _saveService = saveService;
            _levelPlaythrough = levelPlaythrough;
            _levelNumberHolder = levelNumberHolder;
            _audioHandler = audioHandler;
        }

        public async Task<GameFinishedScreen> Load()
        {
            Transform canvasTransform = _canvas.transform;
            GameFinishedScreen gameOverScreen = await 
                LoadInternal<GameFinishedScreen>(GameFinishedScreen, canvasTransform);

            gameOverScreen.Init(_saveService, _loadingScreenProvider, _levelPlaythrough, _levelNumberHolder);
            gameOverScreen.InitAudioHandler(_audioHandler);

            return gameOverScreen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}