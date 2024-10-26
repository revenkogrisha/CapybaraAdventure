using System.Linq;
using CapybaraAdventure.Player;
using CapybaraAdventure.Save;
using Zenject;

namespace Core.Player
{
    public class HeroSkins
    {
        private readonly SkinsCollection _skins;
        private SkinName _boughtSkins;
        private readonly PlayerData _playerData;

        public SkinPreset Current { get; private set; }
        public SkinName BoughtSkins => _boughtSkins;

        [Inject]
        public HeroSkins(PlayerData playerData, SkinsCollection skins)
        {
            _playerData = playerData;
            _skins = skins;
        }

        public void Load(SaveData data)
        {
            _boughtSkins = data.BoughtHeroSkins;
            Current = GetByName(data.CurrentHeroSkin);
        }

        public void SetCurrent(SkinPreset preset)
        {
            Current = preset;
        }

        public SkinPreset[] GetSortedPresets()
        {
            return _skins.Presets
                .OrderByDescending(item => item.Name == Current.Name)
                .ThenByDescending(item => _boughtSkins.HasFlag(item.Name) == true)
                .ThenBy(item => item.FoodCost)
                .ToArray();
        }

        public void Buy(SkinPreset preset)
        {
            _boughtSkins |= preset.Name;

            _playerData.TrySubstractFood(preset.FoodCost);
        }

        public SkinAvailability GetAvailability(SkinName skinName)
        {
            if (_boughtSkins.HasFlag(skinName) == false)
                return SkinAvailability.Buyable;
            else if (skinName == Current.Name)
                return SkinAvailability.Selected;
            else
                return SkinAvailability.Selectable;
        }

        public bool CanBuy(SkinPreset preset) =>
            preset.FoodCost <= _playerData.Food;

        public SkinPreset GetByName(SkinName skinName) => 
            _skins.Presets.Single(preset => preset.Name == skinName);
    }
}