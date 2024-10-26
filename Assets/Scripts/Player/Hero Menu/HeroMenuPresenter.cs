using System;
using CapybaraAdventure.Player;
using CapybaraAdventure.UI;
using Core.Player;

namespace Core.UI
{
    public class HeroMenuPresenter
    {
        private readonly PlayerData _playerData;
        private readonly HeroSkins _heroSkins;
        private readonly UpgradeScreen _view;

        public HeroMenuPresenter(
            PlayerData playerData, 
            HeroSkins heroSkins, 
            UpgradeScreen view)
        {
            _playerData = playerData;
            _heroSkins = heroSkins;
            _view = view;
        }

        public void OnViewReveal()
        {
            _view.InitializeSkins(_heroSkins.Current, _heroSkins.GetSortedPresets());
        }

        public SkinPreset GetPresetByName(SkinName skinName) => 
            _heroSkins.GetByName(skinName);

        public void OnSkinBuyButtonClicked(SkinPreset skin)
        {
            _heroSkins.Buy(skin);
            SetPanelsByAvailability(skin.Name);
        }

        public void OnSelectButtonClicked(SkinPreset skin)
        {
            _heroSkins.SetCurrent(skin);
            SetPanelsByAvailability(skin.Name);
        }

        public void SetPanelsByAvailability(SkinName skinName)
        {
            switch (_heroSkins.GetAvailability(skinName))
            {
                case SkinAvailability.Buyable:
                    _view.SetSkinBuyable(_heroSkins.CanBuy(_view.GetDisplayedSkin()));
                    break;

                case SkinAvailability.Selectable:
                    _view.SetSkinSelectableState();
                    break;

                case SkinAvailability.Selected:
                    _view.SetSkinSelectedState();
                    break;

                default:
                    _view.SetSkinSelectedState();
                    break;
            }
        }

        [Obsolete]
        public void UpgradeHero(bool force = false)
        {
        } 

        [Obsolete]
        public void OnUpdateLevelData()
        {
            // _view.UpdateLevelData(_playerData.Cost, _playerData.HeroLevel);
        }

        [Obsolete]
        public void ValidateUpgradeButton()
        {
            // _view.UpdateUpgradeButton(_playerData.CanUpgradeHero);
        }
    }
}