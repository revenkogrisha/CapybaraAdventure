using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.UI;
using CapybaraAdventure.Game;

namespace CapybaraAdventure.Other
{
    public class PauseScreenProvider : LocalAssetLoader
    {
        public const string PauseScreen = nameof(PauseScreen);

        private readonly Canvas _canvas;
        private readonly PauseManager _pauseManager;
        private readonly GameUI _inGameUI;
        private readonly LoadingScreenProvider _loadingScreenProvider;

        public PauseScreenProvider(
            Canvas canvas,
            PauseManager pauseManager,
            GameUI inGameUI,
            LoadingScreenProvider loadingScreenProvider)
        {
            _canvas = canvas;
            _pauseManager = pauseManager;
            _inGameUI = inGameUI;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public async Task<PauseScreen> Load()
        {
            var canvasTransform = _canvas.transform;
            PauseScreen screen = await LoadInternal<PauseScreen>(PauseScreen, canvasTransform);

            screen.Init(_pauseManager, _inGameUI, _loadingScreenProvider);

            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}