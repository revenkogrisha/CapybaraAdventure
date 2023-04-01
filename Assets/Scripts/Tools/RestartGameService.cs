using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Tools
{
    public class RestartGameService
    {
        public const int GameSceneBuildIndex = 0;

        public void Restart()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(buildIndex);
        }
    }
}