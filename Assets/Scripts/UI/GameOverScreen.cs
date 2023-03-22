using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Tools;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private UIButton _restartButton;

        private RestartGameService _restartService = new RestartGameService();

        #region MonoBehaviour

        private void OnEnable()
        {
            _restartButton.OnClicked += RestartGame;
        }

        private void OnDisable()
        {
            _restartButton.OnClicked -= RestartGame;
        }

        #endregion

        public void Reveal()
        {
            gameObject.SetActive(true);
            //  tweening...
        }

        private void RestartGame() => _restartService.Restart();
    }
}