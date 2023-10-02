using System.Threading.Tasks;
using CapybaraAdventure.UI;
using CapybaraAdventure.Game;
using Zenject;
using UnityEditor;
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

        [Inject]
        public PauseScreenProvider(
            Canvas canvas,
            PauseManager pauseManager,
            GameUI inGameUI,
            LoadingScreenProvider loadingScreenProvider)
        {
            _canvas = canvas;
            _pauseManager = pauseManager;
            _gameUI = inGameUI;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public async Task<PauseScreen> Load()
        {
            var canvasTransform = _canvas.transform;
            PauseScreen screen = await LoadInternal<PauseScreen>(PauseScreen, canvasTransform);

            screen.Init(_pauseManager, _gameUI, _loadingScreenProvider);

            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}