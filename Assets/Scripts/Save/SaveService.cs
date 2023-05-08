using UnityEngine;
using CapybaraAdventure.Player;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
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

            data.HighScore = _score.HighScore;
            data.Coins = _playerData.Coins;
            data.MaxDistance = _playerData.MaxDistance;
            data.DistanceUpgradeCost = _playerData.DistanceUpgradeCost;
            data.FoodBonus = _playerData.FoodBonus;
            data.FoodUpgradeCost = _playerData.FoodUpgradeCost;
            
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