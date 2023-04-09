using System.Threading.Tasks;
using UnityEngine;
using CapybaraAdventure.UI;

namespace CapybaraAdventure.Other
{
    public class PauseScreenProvider : LocalAssetLoader
    {
        public const string PauseScreen = nameof(PauseScreen);

        private readonly Canvas _canvas;

        public PauseScreenProvider(Canvas canvas)
        {
            _canvas = canvas;
        }

        public async Task<PauseScreen> Load()
        {
            var canvasTransform = _canvas.transform;
            PauseScreen screen = await LoadInternal<PauseScreen>(PauseScreen, canvasTransform);

            return screen;
        }

        public void Unload() => UnlodadInternalIfCached();
    }
}