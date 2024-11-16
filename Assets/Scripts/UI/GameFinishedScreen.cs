using UnityEngine;
using TMPro;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;
using System;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.UI
{
    public class GameFinishedScreen : UIBase
    {
        [Header("UI Elements")]
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private Transform _logoText;
        [SerializeField] private CanvasGroup _backgroundCanvasGroup;
        
        [Header("Rewards")]
        [SerializeField] private Transform _rewardsLayout;
        [SerializeField] private RewardBlock _rewardBlockPrefab;

        [Header("UI Settings")]
        [SerializeField] private float _UIShowDuration = 0.3f;

        [Header("Ads Settings")]
        [SerializeField] private AdInterstitial _interstitialAd;

        private SaveService _saveService;
        private LoadingScreenProvider _loadingScreenProvider;
        private LevelPlaythrough _levelPlaythrough;

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

        public void Init(
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider,
            LevelPlaythrough levelPlaythrough)
        {
            _saveService = saveService;
            _loadingScreenProvider = loadingScreenProvider;
            _levelPlaythrough = levelPlaythrough;
        }

        public override void Reveal()
        {
            _levelPlaythrough.LogAll();
            LevelPlaythrough.Reward[] rewards = _levelPlaythrough.GetRewards();
            foreach (LevelPlaythrough.Reward reward in rewards)
            {
                RewardBlock block = Instantiate(_rewardBlockPrefab, _rewardsLayout);
                block.Initialize(reward);
            }

            rewards = null;
            
            base.Reveal();

            var tweener = new ScreenTweener(_UIShowDuration);
            tweener.ScaleTweenLogo(_logoText);
            tweener.TweenButtonWithoutDelay(_restartButton.transform);

            tweener.FadeIn(_backgroundCanvasGroup);
        }

        private async void RestartGame()
        {
            _saveService.Save();

            if (_interstitialAd.TryShowWithChance() == true)
                return;
            
            // TODO: level ++?
            
            await _loadingScreenProvider.LoadGameAsync();
        }
    }
}