using System;
using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        public const string HighScore = nameof(HighScore);
        public const string Coins = nameof(Coins);
        public const string MaxDistance = nameof(MaxDistance);
        public const string DistanceUpgradeCost = nameof(DistanceUpgradeCost);

        private ISaveSystem _saveSystem;

        public SaveData Data { get; private set; } = new();

        public event Action OnDataLoaded;

        #region MonoBehaviour

        private void Awake()
        {
            _saveSystem = new JsonSaveSystem();

            Load();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        #endregion

        public void ResetProcess()
        {
            var emptyData = new SaveData();
            _saveSystem.Save(emptyData);
            Load();
        }

        public void Save()
        {
            var data = new SaveData();

            data.HighScore = PlayerPrefs.GetInt(HighScore);
            data.Coins = PlayerPrefs.GetInt(Coins);
            data.MaxDistance = PlayerPrefs.GetFloat(MaxDistance);
            data.DistanceUpgradeCost = PlayerPrefs.GetInt(DistanceUpgradeCost);

            Data = data;
            _saveSystem.Save(data);
        }

        private void Load()
        {
            Data = _saveSystem.Load();
            OnDataLoaded?.Invoke();
            print(Data.MaxDistance);
            print(Data.DistanceUpgradeCost);
        }
    }
}