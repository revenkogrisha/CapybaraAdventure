using System;
using Core.Player;

namespace CapybaraAdventure.Save
{
    [Serializable]
    public class SaveData
    {
        public int HighScore = 0;
        public int Coins = 0;
        public int Food = 0;
        public float MaxDistance = 16f;
        public int DistanceUpgradeCost = 15;
        public float FoodBonus = 0.25f;
        public int FoodUpgradeCost = 15;
        public bool IsCutsceneWatched = false;
        public int LanguageIndex = 0;
        
        // NEW
        public SkinName BoughtHeroSkins = SkinName.Capybuddy;
        public SkinName CurrentHeroSkin = SkinName.Capybuddy;
        public int Level = 1;
    }
}