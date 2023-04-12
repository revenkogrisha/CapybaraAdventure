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

        private PlayerData _playerData;
        private SaveService _saveService;

        public event Action OnScreenClosed;

        #region MonoBehaviour

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;

            _jumpDistanceUpgrade.Button.OnClicked += TryUpdgradeJumpDistance;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;

            _jumpDistanceUpgrade.Button.OnClicked -= TryUpdgradeJumpDistance;
        }

        #endregion

        public void Init(PlayerData playerData, SaveService saveService)
        {
            _playerData = playerData;
            _saveService = saveService;

            _jumpDistanceUpgrade.Init(_saveService.Data.DistanceUpgradeCost);
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
    }
}