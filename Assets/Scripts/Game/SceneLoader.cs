using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Game
{
    public class SceneLoader : MonoBehaviour
    {
        private const int PregameCutsceneSceneID = 1;
        private const int GameSceneID = 2;

        public async UniTask LoadGame(CancellationToken cancellationToken = default)
        {
            await SceneManager
                .LoadSceneAsync(GameSceneID)
                .WithCancellation(cancellationToken);
        }

        public async UniTask LoadPregameCutscene(CancellationToken cancellationToken = default)
        {
            await SceneManager
                .LoadSceneAsync(PregameCutsceneSceneID)
                .WithCancellation(cancellationToken);
        }
    }
}