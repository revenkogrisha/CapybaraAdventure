using UnityEngine;
using UnityTools.Buttons;
using CapybaraAdventure.Player;
using System;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.UI
{
    public class UpgradeScreen : UIBase
    {
        [SerializeField] private UIButton _backButton;
        [SerializeField] private UpgradeBlock _jumpDistanceUpgrade;
        [SerializeField] private UpgradeBlock _foodBonusUpgrade;

        private PlayerData _playerData;
        private SaveService _saveService;

        public event Action OnScreenClosed;

        #region MonoBehaviour

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;

            _jumpDistanceUpgrade.Button.OnClicked += TryUpdgradeJumpDistance;
            _foodBonusUpgrade.Button.OnClicked += TryUpgradeFoodBonus;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;

            _jumpDistanceUpgrade.Button.OnClicked -= TryUpdgradeJumpDistance;
            _foodBonusUpgrade.Button.OnClicked -= TryUpgradeFoodBonus;
        }

        #endregion

        public void Init(PlayerData playerData, SaveService saveService)
        {
            _playerData = playerData;
            _saveService = saveService;

            _jumpDistanceUpgrade.Init(_saveService.Data.DistanceUpgradeCost);
            _foodBonusUpgrade.Init(_saveService.Data.FoodUpgradeCost);
        }

        private void OnBackButtonClickedHandler()
        {
            OnScreenClosed?.Invoke();
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
        }
    }
}