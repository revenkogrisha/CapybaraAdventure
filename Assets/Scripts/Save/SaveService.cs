using System;
using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        public const string HighScore = nameof(HighScore);
        public const string Coins = nameof(Coins);

        private ISaveSystem _saveSystem;

        public int HighScoreValue { get; private set; } = 0;
        public int CoinsValue { get; private set; } = 0;

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

        private void Save()
        {
            var data = new SaveData();

            data.HighScore = PlayerPrefs.GetInt(HighScore);
            data.Coins = PlayerPrefs.GetInt(Coins);

            _saveSystem.Save(data);
        }

        private void Load()
        {
            var data = _saveSystem.Load();

            HighScoreValue = data.HighScore;
            CoinsValue = data.Coins;

            OnDataLoaded?.Invoke();
        }
    }
}