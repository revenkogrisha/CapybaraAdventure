using UnityEngine;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        public const string HighScore = nameof(HighScore);
        public const string Coins = nameof(Coins);
        public const string MaxDistance = nameof(MaxDistance);
        public const string DistanceUpgradeCost = nameof(DistanceUpgradeCost);
        public const string FoodBonus = nameof(FoodBonus);
        public const string FoodUpgradeCost = nameof(FoodUpgradeCost);

        [SerializeField] private Score _score;
        [SerializeField] private PlayerData _playerData;

        private ISaveSystem _saveSystem;

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
            data.FoodBonus = PlayerPrefs.GetFloat(FoodBonus);
            data.FoodUpgradeCost = PlayerPrefs.GetInt(FoodUpgradeCost);
            
            _saveSystem.Save(data);
        }

        private void Load()
        {
            SaveData data = _saveSystem.Load();

            _playerData.LoadData(data);
            _score.LoadHighScore(data);
        }
    }
}