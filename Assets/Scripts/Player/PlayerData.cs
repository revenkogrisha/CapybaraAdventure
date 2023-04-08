using System;
using CapybaraAdventure.Save;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        private readonly SaveService _saveService;

        public int Coins { get; private set; } = 0;

        public PlayerData(SaveService saveService)
        {
            _saveService = saveService;

            Init();
        }

        public void AddSimpleChestCoins()
        { 
            var amount = SimpleChest.CoinsInsideAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amoint was given!");

            Coins += amount;
            PlayerPrefs.SetInt(SaveService.Coins, Coins);
        }

        private void Init()
        {
            Coins = _saveService.CoinsValue;
        }
    }
}