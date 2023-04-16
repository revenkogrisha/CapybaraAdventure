using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
    {
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private Transform _logoText;
        [SerializeField] private float _UIShowDuration = 0.3f;

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

        public override void Reveal()
        {
            base.Reveal();

            var tweener = new ScreenTweener(_UIShowDuration);
            tweener.ScaleTweenLogo(_logoText);
            tweener.TweenButtonWithoutDelay(_restartButton.transform);
        }

        private void RestartGame() => _restartService.Restart();
    }
}