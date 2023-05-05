using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;
using System;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
    {
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private UIButton _continueButton;
        [SerializeField] private Transform _logoText;
        [SerializeField] private float _UIShowDuration = 0.3f;
        [SerializeField] private AppodealInterstitial _interstitialAd;
        [SerializeField] private AppodealRewarded _rewardedHeroRevival;

        private LoadingScreenProvider _loadingScreenProvider;

        public event Action OnGameContinued;

        #region MonoBehaviour

        private void OnEnable()
        {
            _restartButton.OnClicked += RestartGame;
            _rewardedHeroRevival.OnRewardGotten += InvokeGameContinuing;
        }

        private void OnDisable()
        {
            _restartButton.OnClicked -= RestartGame;
            _rewardedHeroRevival.OnRewardGotten -= InvokeGameContinuing;
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
            tweener.TweenButtonWithoutDelay(_continueButton.transform);
        }

        public void BlockContinuing() => 
            _continueButton.OriginalButton.interactable = false;

        private async void RestartGame() => await _loadingScreenProvider.LoadSceneAsync();

        private void InvokeGameContinuing() => OnGameContinued?.Invoke();
    }
}