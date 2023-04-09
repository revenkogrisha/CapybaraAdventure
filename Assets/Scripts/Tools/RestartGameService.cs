using UnityEngine.SceneManagement;

namespace CapybaraAdventure.Other
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