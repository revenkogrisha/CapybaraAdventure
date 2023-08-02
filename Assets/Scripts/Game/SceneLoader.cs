using UnityEngine;
using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Game
{
    public class SceneLoader : MonoBehaviour
    {
        private const int PregameCutsceneSceneID = 1;
        private const int GameSceneID = 2;

        public AsyncOperation LoadPregameCutscene()
        {
            var handle = SceneManager.LoadSceneAsync(PregameCutsceneSceneID);
            return handle;
        }

        public AsyncOperation LoadGame()
        {
            var handle = SceneManager.LoadSceneAsync(GameSceneID);
            return handle;
        }
    }
}