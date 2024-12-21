using System;
using CapybaraAdventure.Player;
using UnityEngine;

namespace CapybaraAdventure.UI
{
    public class ShopMenuScreen : UIBase
    {
        [SerializeField] private UIButton _backButton;
        [SerializeField] private ResourcePanel _resourcePanel;
        
        private PlayerData _playerData;

        public event Action OnScreenClosed;

        private void Awake()
        {
            transform.SetAsFirstSibling();
        }

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;
        }

        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
            
            _resourcePanel.Init(_playerData);
        }
        
        private void OnBackButtonClickedHandler()
        {
            OnScreenClosed?.Invoke();
        }
    }
}