using UnityEngine;
using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Game
{
    public class SceneLoader : MonoBehaviour
    {
        private const int GameSceneID = 1;

        public AsyncOperation Load()
        {
            var handle = SceneManager.LoadSceneAsync(GameSceneID);
            return handle;
        }
    }
}