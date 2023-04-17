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

        public async Task LoadSceneAsync()
        {
            SceneLoader loader = await Load();
            StartCoroutine(LoadScene(loader));
        }

        public async Task<SceneLoader> Load()
        {
            Transform canvasTransform = _canvas.transform;
            SceneLoader loader = await LoadInternal<SceneLoader>(LoadingScreen, canvasTransform);

            return loader;
        }

        public void Unload() => UnlodadInternalIfCached();

        private IEnumerator LoadScene(SceneLoader loader)
        {
            AsyncOperation operation = loader.Load();
            while (operation.isDone == false)
                yield return null;

            Unload();
        }
    }
}
