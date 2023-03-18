using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CapybaraAdventure
{
    public class LoadingScreen : MonoBehaviour
    {
        private const int GameSceneBuildIndex = 1;

        public event Action<float> OnLoadingProgressChanged;

        #region MonoBehaviour

        private void Start()
        {
            StartCoroutine(LoadGameScene());
        }

        #endregion

        private IEnumerator LoadGameScene()
        {
            var loadOperation = SceneManager
                .LoadSceneAsync(GameSceneBuildIndex);

            while (loadOperation.isDone == false)
            {
                var progress = loadOperation.progress;
                OnLoadingProgressChanged?.Invoke(progress);

                yield return null;
            }
        }
    }
}
