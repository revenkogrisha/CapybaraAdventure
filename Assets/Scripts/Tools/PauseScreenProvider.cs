using System.Threading.Tasks;
using CapybaraAdventure.UI;
using CapybaraAdventure.Game;
using Core.Audio;
using Zenject;
using UnityEngine;

namespace CapybaraAdventure.Other
{
    public class PauseScreenProvider : LocalAssetLoader
    {
        public const string PauseScreen = nameof(PauseScreen);

        private readonly Canvas _canvas;
        private readonly PauseManager _pauseManager;
        private readonly GameUI _gameUI;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly IAudioHandler _audioHandler;

        [Inject]
        public PauseScreenProvider(
            Canvas canvas,
            PauseManager pauseManager,
            GameUI inGameUI,
            LoadingScreenProvider loadingScreenProvider,
            IAudioHandler audioHandler)
        {
            _canvas = canvas;
            _pauseManager = pauseManager;
            _gameUI = inGameUI;
            _loadingScreenProvider = loadingScreenProvider;
            _audioHandler = audioHandler;
        }

        public async Task<PauseScreen> Load()
        {
            var canvasTransform = _canvas.transform;
            PauseScreen screen = await LoadInternal<PauseScreen>(PauseScreen, canvasTransform);

            screen.Init(_pauseManager, _gameUI, _loadingScreenProvider);
            screen.InitAudioHandler(_audioHandler);

            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}