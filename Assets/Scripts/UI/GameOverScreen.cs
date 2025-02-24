using UnityEngine;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;
using System;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
    {
        [Header("UI Elements")]
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private UIButton _continueButton;
        [SerializeField] private Transform _logoText;
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;

        [Header("UI Settings")]
        [SerializeField] private float _UIShowDuration = 0.3f;

        [Header("Ads Settings")]
        [SerializeField] private AdInterstitial _interstitialAd;
        [SerializeField] private AdRewarded _rewardedHeroRevival;

        private SaveService _saveService;
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

        public void Init(
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider)
        {
            _saveService = saveService;
            _loadingScreenProvider = loadingScreenProvider;
        }

        public override void Reveal()
        {
            base.Reveal();

            var tweener = new ScreenTweener(_UIShowDuration);
            tweener.ScaleTweenLogo(_logoText);
            tweener.TweenButtonWithoutDelay(_restartButton.transform);
            tweener.TweenButtonWithoutDelay(_continueButton.transform);

            tweener.FadeIn(_backgroundCanvasGroup);
        }

        public void BlockContinuing() => 
            _continueButton.Interactable = false;

        private async void RestartGame()
        {
            _saveService.Save();
            
            if (_interstitialAd.TryShowWithChance() == true)
                return;
            
            await _loadingScreenProvider.LoadGameAsync();
        }

        private void InvokeGameContinuing()
        {
            _saveService.Save();
            OnGameContinued?.Invoke();
        }
    }
}