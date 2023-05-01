using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
    {
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private Transform _logoText;
        [SerializeField] private float _UIShowDuration = 0.3f;
        [SerializeField] private AppodealInterstitial _interstitialAd;

        private LoadingScreenProvider _loadingScreenProvider;

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

        public void Init(LoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }

        public override void Reveal()
        {
            _interstitialAd.TryShowWithChance();

            base.Reveal();

            var tweener = new ScreenTweener(_UIShowDuration);
            tweener.ScaleTweenLogo(_logoText);
            tweener.TweenButtonWithoutDelay(_restartButton.transform);
        }

        private async void RestartGame() => await _loadingScreenProvider.LoadSceneAsync();
    }
}