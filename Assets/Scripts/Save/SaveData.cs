using System;

namespace CapybaraAdventure.Save
{
    [Serializable]
    public class SaveData
    {
        public int HighScore = 0;
        public int Coins = 0;
        public float MaxDistance = 15f;
        public int DistanceUpgradeCost = 15;
    }
}