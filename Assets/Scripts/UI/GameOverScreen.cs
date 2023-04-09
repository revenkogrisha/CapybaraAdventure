using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
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

        private void RestartGame() => _restartService.Restart();
    }
}