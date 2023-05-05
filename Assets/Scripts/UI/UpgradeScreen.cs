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
        [SerializeField] private AdRewardGranter _rewardedCoins;

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
            _rewardedCoins.OnRewardGranted += AddRewardedCoins;

            _jumpDistanceUpgrade.Button.OnClicked += TryUpdgradeJumpDistance;
            _foodBonusUpgrade.Button.OnClicked += TryUpgradeFoodBonus;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;
            _rewardedCoins.OnRewardGranted -= AddRewardedCoins;

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

            _jumpDistanceUpgrade.Init(_saveService.Data.DistanceUpgradeCost);
            _foodBonusUpgrade.Init(_saveService.Data.FoodUpgradeCost);
            _resetService.Init(
                _saveService,
                loadingScreenProvider);
        }

        private void OnBackButtonClickedHandler()
        {
            OnScreenClosed?.Invoke();
        }

        private void AddRewardedCoins()
        {
            _playerData.AddSimpleChestCoins();
        }

        private void TryUpdgradeJumpDistance()
        {
            bool isOperationSucceeded = _playerData
                .TrySubstractCoins(_jumpDistanceUpgrade);

            if (isOperationSucceeded == false)
                return;

            _jumpDistanceUpgrade.HandleUpgrade();
            _playerData.UpgradeJumpDistance(_jumpDistanceUpgrade);

            _saveService.Save();

            _interstitialAd.TryShowWithChance();
        }

        private void TryUpgradeFoodBonus()
        {
            bool isOperationSucceeded = _playerData
                .TrySubstractCoins(_foodBonusUpgrade);

            if (isOperationSucceeded == false)
                return;

            _foodBonusUpgrade.HandleUpgrade();
            _playerData.UpgradeFoodBonus(_foodBonusUpgrade);

            _saveService.Save();

            _interstitialAd.TryShowWithChance();
        }
    }
}