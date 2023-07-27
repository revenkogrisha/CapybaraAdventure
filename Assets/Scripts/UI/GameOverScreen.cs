using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Other;
using System;
using CapybaraAdventure.Save;
using YG;

namespace CapybaraAdventure.UI
{
    public class GameOverScreen : UIBase
    {
        private const int ContinueAdId = 2;

        [Header("Components:")]
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private UIButton _continueButton;
        [SerializeField] private Transform _logoText;

        [Header("UI Settings")]
        [SerializeField] private float _UIShowDuration = 0.3f;

        private SaveService _saveService;
        private LoadingScreenProvider _loadingScreenProvider;

        public event Action OnGameContinued;

        #region MonoBehaviour

        private void OnEnable()
        {
            _restartButton.OnClicked += RestartGame;
            _continueButton.OnClicked += ShowContinueAd;

            YandexGame.RewardVideoEvent += InvokeGameContinuing;
        }

        private void OnDisable()
        {
            _restartButton.OnClicked -= RestartGame;
            _continueButton.OnClicked -= ShowContinueAd;
            
            YandexGame.RewardVideoEvent -= InvokeGameContinuing;
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
        }

        public void BlockContinuing() => 
            _continueButton.Lock();

        private async void RestartGame()
        {
            _saveService.Save();
            await _loadingScreenProvider.LoadSceneAsync();
        }

        private void InvokeGameContinuing(int id)
        {
            if (id != ContinueAdId)
                return;
                
            _saveService.Save();
            OnGameContinued?.Invoke();
        }

        private void ShowContinueAd()
        {
            YGAdsProvider.ShowRewardedAd(ContinueAdId);
        }
    }
}