using System;
using CapybaraAdventure.Player;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    public class ShopMenuScreen : UIBase
    {
        [SerializeField] private UIButton _backButton;
        [SerializeField] private UIButton _consumableButton;
        [SerializeField] private UIButton _nonConsumableButton;
        [SerializeField] private ResourcePanel _resourcePanel;
        
        private PlayerData _playerData;
        private PurchaseManager _purchaseManager;

        public event Action OnScreenClosed;

        private void Awake()
        {
            transform.SetAsFirstSibling();
        }

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;
            _consumableButton.OnClicked += OnConsumablePurchaseHandler;
            _nonConsumableButton.OnClicked += OnNonConsumablePurchaseHandler;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;
            _consumableButton.OnClicked += OnConsumablePurchaseHandler;
            _nonConsumableButton.OnClicked += OnNonConsumablePurchaseHandler;
        }

        public void Init(PlayerData playerData, PurchaseManager purchaseManager)
        {
            _playerData = playerData;
            _purchaseManager = purchaseManager;
            
            _resourcePanel.Init(_playerData);
        }
        
        private void OnBackButtonClickedHandler()
        {
            OnScreenClosed?.Invoke();
        }
        
        private void OnConsumablePurchaseHandler()
        {
            _purchaseManager.InitiateConsumablePurchase();
            UpdateResources();
        }
        
        private void OnNonConsumablePurchaseHandler()
        {
            _purchaseManager.InitiateNonConsumablePurchase();
            UpdateResources();
        }

        private void UpdateResources()
        {
            _resourcePanel.DisplayResources();
        }
    }
}