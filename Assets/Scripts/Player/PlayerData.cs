using System;
using CapybaraAdventure.Save;
using UnityEngine;

namespace CapybaraAdventure.Player
{
    public class PlayerData
    {
        private readonly SaveService _saveService;

        public int Coins { get; private set; } = 0;

        public event Action<int> OnCoinsChanged;

        public PlayerData(SaveService saveService)
        {
            _saveService = saveService;
            _saveService.OnDataLoaded += Init;
        }

        public void Disable()
        {
            _saveService.OnDataLoaded -= Init;
        }

        public void AddSimpleChestCoins()
        { 
            var amount = SimpleChest.CoinsInsideAmount;

            if (amount <= 0)
                throw new ArgumentException("Wrong amount was given!");

            Coins += amount;
            PlayerPrefs.SetInt(SaveService.Coins, Coins);

            OnCoinsChanged?.Invoke(Coins);
        }

        private void Init()
        {
            Coins = _saveService.CoinsValue;
        }
    }
}