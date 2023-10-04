using UnityEngine;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CapybaraAdventure.Other
{
    public class LoadingScreenProvider : LocalAssetLoader
    {
        public const string LoadingScreen = nameof(LoadingScreen);

        private readonly Canvas _canvas;

        [Inject]
        public LoadingScreenProvider(Canvas canvas)
        {
            _canvas = canvas;
        }

        public async Task LoadGameAsync()
        {
            SceneLoader loader = await Load();
            await LoadGame(loader);
        }

        public async Task LoadPregameCutsceneAsync()
        {
            SceneLoader loader = await Load();
            await LoadPregameCutscene(loader);
        }

        public async Task<SceneLoader> Load()
        {
            Transform canvasTransform = _canvas.transform;
            SceneLoader loader = await LoadInternal<SceneLoader>(LoadingScreen, canvasTransform);

            return loader;
        }

        public void Unload()
        {
            UnlodadInternalIfCached();
        }

        private async UniTask LoadGame(SceneLoader loader)
        {
            await loader.LoadGame();
            Unload();
        }

        private async UniTask LoadPregameCutscene(SceneLoader loader)
        {
            await loader.LoadPregameCutscene();
            Unload();
        }
    }
}
