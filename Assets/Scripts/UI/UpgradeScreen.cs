using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Player;
using System;
using CapybaraAdventure.Save;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;

namespace CapybaraAdventure.UI
{
    public class UpgradeScreen : UIBase
    {
        [SerializeField] private UIButton _backButton;
        [SerializeField] private ResetProgressService _resetService;
        [SerializeField] private UpgradeBlock _jumpDistanceUpgrade;
        [SerializeField] private UpgradeBlock _foodBonusUpgrade;
        [SerializeField] private AppodealInterstitial _interstitialAd;
        [SerializeField] private AppodealRewarded _rewardedCoins;
        [SerializeField] private Vector2Int _minMaxRewardedCoins = new(5, 31);

        private PlayerData _playerData;
        private SaveService _saveService;

        public event Action OnScreenClosed;

        #region MonoBehaviour

        private void Awake()
        {
            transform.SetAsFirstSibling();
        }

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;
            _rewardedCoins.OnRewardGotten += AddRewardedCoins;

            _jumpDistanceUpgrade.Button.OnClicked += TryUpdgradeJumpDistance;
            _foodBonusUpgrade.Button.OnClicked += TryUpgradeFoodBonus;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;
            _rewardedCoins.OnRewardGotten -= AddRewardedCoins;

            _jumpDistanceUpgrade.Button.OnClicked -= TryUpdgradeJumpDistance;
            _foodBonusUpgrade.Button.OnClicked -= TryUpgradeFoodBonus;
        }

        #endregion

        public void Init(
            PlayerData playerData,
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider)
        {
            _playerData = playerData;
            _saveService = saveService;

            _jumpDistanceUpgrade.Init(_playerData.DistanceUpgradeCost);
            _foodBonusUpgrade.Init(_playerData.FoodUpgradeCost);
            _resetService.Init(
                saveService,
                loadingScreenProvider);
        }

        private void OnBackButtonClickedHandler()
        {
            OnScreenClosed?.Invoke();
        }

        private void AddRewardedCoins()
        {
            int minInclusive = _minMaxRewardedCoins.x;
            int maxExclusive = _minMaxRewardedCoins.y;

            _playerData.AddRandomAdCoins(minInclusive, maxExclusive);
        }

        private void TryUpdgradeJumpDistance()
        {
            bool isOperationSucceeded = _playerData
                .TrySubstractCoins(_jumpDistanceUpgrade);

            if (isOperationSucceeded == false)
                return;

            _jumpDistanceUpgrade.HandleUpgrade();
            _playerData.UpgradeJumpDistance(_jumpDistanceUpgrade);

            HandleUpgrade();
        }

        private void TryUpgradeFoodBonus()
        {
            bool isOperationSucceeded = _playerData
                .TrySubstractCoins(_foodBonusUpgrade);

            if (isOperationSucceeded == false)
                return;

            _foodBonusUpgrade.HandleUpgrade();
            _playerData.UpgradeFoodBonus(_foodBonusUpgrade);

            HandleUpgrade();
        }

        private void HandleUpgrade()
        {
            _saveService.Save();
            _interstitialAd.TryShowWithChance();
        }
    }
}