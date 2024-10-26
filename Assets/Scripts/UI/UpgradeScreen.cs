using UnityEngine;
using CapybaraAdventure.Player;
using System;
using System.Collections.Generic;
using CapybaraAdventure.Save;
using CapybaraAdventure.Other;
using CapybaraAdventure.Ad;
using Core.Audio;
using Core.Player;
using Core.UI;

namespace CapybaraAdventure.UI
{
    public class UpgradeScreen : UIBase
    {
        [Header("UI Elements")]
        [SerializeField] private UIButton _backButton;
        [SerializeField] private UpgradeBlock _jumpDistanceUpgrade;
        [SerializeField] private UpgradeBlock _foodBonusUpgrade;
        // [SerializeField] private ResetProgressService _resetService;
        
        [Header("Skins Components")]
        [SerializeField] private SkinsPanel _skinsPanel;
        [SerializeField] private SkinPlacementPanel _skinPlacement;
        [SerializeField] private ResourcePanel _resourcePanel;

        [Header("Ads Settings")]
        [SerializeField] private AdInterstitial _interstitialAd;
        [SerializeField] private AdRewarded _rewardedCoins;
        [SerializeField] private Vector2Int _minMaxRewardedCoins = new Vector2Int(5, 31);

        private PlayerData _playerData;
        private SaveService _saveService;
        private IAudioHandler _audioHandler;
        private HeroSkins _heroSkins;
        private HeroMenuPresenter _presenter;
        private SkinPreset _displayedSkin;
        private SkinPreset _selectedSkin;

        public event Action OnScreenClosed;

        #region MonoBehaviour

        private void Awake()
        {
            transform.SetAsFirstSibling();
            _presenter = new(_playerData, _heroSkins, this);
        }

        private void OnEnable()
        {
            _backButton.OnClicked += OnBackButtonClickedHandler;
            _rewardedCoins.OnRewardGotten += AddRewardedCoins;

            // _jumpDistanceUpgrade.Button.OnClicked += TryUpdgradeJumpDistance;
            // _foodBonusUpgrade.Button.OnClicked += TryUpgradeFoodBonus;

            // NEW
            _skinsPanel.ItemDisplayCommand += DisplayItem;
            _skinPlacement.BuyButtonCommand += OnSkinBuyButtonClicked;
            _skinPlacement.SelectButtonCommand += OnSelectButtonClicked;
            
            // –––––Reveal–––––
            
            _presenter.OnViewReveal();
            _presenter.SetPanelsByAvailability(_displayedSkin.Name);

            DisplayItem(_displayedSkin.Name);
            UpdateView();
        }

        private void OnDisable()
        {
            _backButton.OnClicked -= OnBackButtonClickedHandler;
            _rewardedCoins.OnRewardGotten -= AddRewardedCoins;

            // _jumpDistanceUpgrade.Button.OnClicked -= TryUpdgradeJumpDistance;
            // _foodBonusUpgrade.Button.OnClicked -= TryUpgradeFoodBonus;
            
            // NEW
            _skinsPanel.ItemDisplayCommand -= DisplayItem;
            _skinPlacement.BuyButtonCommand -= OnSkinBuyButtonClicked;
            _skinPlacement.SelectButtonCommand -= OnSelectButtonClicked;
        }

        #endregion

        public void Init(
            PlayerData playerData,
            SaveService saveService,
            LoadingScreenProvider loadingScreenProvider,
            HeroSkins heroSkins,
            IAudioHandler audioHandler)
        {
            _playerData = playerData;
            _saveService = saveService;
            _heroSkins = heroSkins;
            _audioHandler = audioHandler;
            
            _resourcePanel.Init(_playerData);

            // _jumpDistanceUpgrade.Init(_playerData.DistanceUpgradeCost);
            // _foodBonusUpgrade.Init(_playerData.FoodUpgradeCost);
            // _resetService.Init(
            //     saveService,
            //     loadingScreenProvider);
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
        
        // –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––
        
        
        public void InitializeSkins(SkinPreset current, IEnumerable<SkinPreset> presets)
        {
            _skinsPanel.CreateItems(presets);

            _displayedSkin = current;
            _skinsPanel.CommandItemDisplay(current.Name);
        }

        public void SetSkinBuyable(bool canBuy) => 
            _skinPlacement.SetBuyState(_displayedSkin.FoodCost, canBuy);

        public void SetSkinSelectableState() => 
            _skinPlacement.SetSelectableState();

        public void SetSkinSelectedState()
        {
            _skinPlacement.SetSelectedState();

            if (_selectedSkin != null)
                _skinsPanel.SetSelected(_selectedSkin.Name, false);

            _selectedSkin = _displayedSkin;

            _skinsPanel.SetSelected(_displayedSkin.Name, true);
        }

        public SkinPreset GetDisplayedSkin() =>
            _displayedSkin;

        [Obsolete]
        public void UpdateLevelData(int cost, int heroLevel)
        {
            // _costTMP.SetText(string.Format(CostFormat, cost));
            // _heroLevelTMP.SetText(string.Format(LevelFormat, heroLevel));
        }

        [Obsolete]
        public void UpdateUpgradeButton(bool canUpgradeHero)
        {
            // _heroUpgradeButton.Interactable = canUpgradeHero;
            //
            // if (canUpgradeHero == true)
            //     _costTMP.color = _costAvailableColor;
            // else
            //     _costTMP.color = _costLockedColor;
        }

        [Obsolete]
        private void OnUpgradeButtonClicked()
        {
            _audioHandler.PlaySound(AudioName.CoinsSpent);
            
            UpgradeHero();
            // _mediationService.ShowInterstitial();
        }

        [Obsolete]
        private void ForceUpgrade()
        {
            _presenter.UpgradeHero(true);
            
            UpdateView();
        }

        private void UpdateView()
        {
            UpdateLevelData();
            ValidateUpgradeButton();
        }

        [Obsolete]
        private void UpgradeHero()
        {
            _presenter.UpgradeHero();
            
            UpdateView();
        }

        private void UpdateLevelData()
        {
            _resourcePanel.DisplayResources();
            
            _presenter.OnUpdateLevelData();
        }

        private void ValidateUpgradeButton() => 
            _presenter.ValidateUpgradeButton();

        private void DisplayItem(SkinName name)
        {
            SkinPreset preset = _presenter.GetPresetByName(name);
            _displayedSkin = preset;
            
            _skinPlacement.DisplayPreset(preset);

            _presenter.SetPanelsByAvailability(name);
        }

        private void OnSkinBuyButtonClicked()
        {
            _audioHandler.PlaySound(AudioName.FoodBite);
            
            _presenter.OnSkinBuyButtonClicked(_displayedSkin);
            _resourcePanel.DisplayResources();
            
            _saveService.Save();
        }

        private void OnSelectButtonClicked()
        {
            _presenter.OnSelectButtonClicked(_displayedSkin);
            
            _saveService.Save();
        }
    }
}