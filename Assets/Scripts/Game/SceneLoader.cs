using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Game
{
    public class SceneLoader : MonoBehaviour
    {
        private const int PregameCutsceneSceneID = 1;
        private const int GameSceneID = 2;

        public async UniTask LoadPregameCutscene()
        {
            await SceneManager.LoadSceneAsync(PregameCutsceneSceneID);
        }

        public async UniTask LoadGame()
        {
            await SceneManager.LoadSceneAsync(GameSceneID);
        }
    }
}