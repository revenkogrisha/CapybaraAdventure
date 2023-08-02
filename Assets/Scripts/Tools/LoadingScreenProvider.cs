using UnityEngine;
using CapybaraAdventure.Game;
using System.Threading.Tasks;
using System.Collections;

namespace CapybaraAdventure.Other
{
    public class LoadingScreenProvider : LocalAssetLoader
    {
        public const string LoadingScreen = nameof(LoadingScreen);

        [SerializeField] private Canvas _canvas;

        public async Task LoadGameAsync()
        {
            SceneLoader loader = await Load();
            StartCoroutine(LoadGame(loader));
        }

        public async Task LoadPregameCutsceneAsync()
        {
            SceneLoader loader = await Load();
            StartCoroutine(LoadPregameCutscene(loader));
        }

        public async Task<SceneLoader> Load()
        {
            Transform canvasTransform = _canvas.transform;
            SceneLoader loader = await LoadInternal<SceneLoader>(LoadingScreen, canvasTransform);

            return loader;
        }

        public void Unload() => UnlodadInternalIfCached();

        private IEnumerator LoadGame(SceneLoader loader)
        {
            AsyncOperation operation = loader.LoadGame();
            while (operation.isDone == false)
                yield return null;

            Unload();
        }

        private IEnumerator LoadPregameCutscene(SceneLoader loader)
        {
            AsyncOperation operation = loader.LoadPregameCutscene();
            while (operation.isDone == false)
                yield return null;

            Unload();
        }
    }
}
