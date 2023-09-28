using UnityEngine;
using CapybaraAdventure.Player;
using System.Collections;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        private const float AutoSaveInterval = 10f;

        [SerializeField] private Score _score;
        [SerializeField] private PlayerData _playerData;

        private ISaveSystem _saveSystem;

        #region MonoBehaviour

        private void Awake()
        {
            _saveSystem = new JsonSaveSystem();

            Load();

            StartCoroutine(AutoSave());
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
            var data = new SaveData
            {
                HighScore = _score.HighScore,
                Coins = _playerData.Coins,
                MaxDistance = _playerData.MaxDistance,
                DistanceUpgradeCost = _playerData.DistanceUpgradeCost,
                FoodBonus = _playerData.FoodBonus,
                FoodUpgradeCost = _playerData.FoodUpgradeCost,
                IsCutsceneWatched = _playerData.IsCutsceneWatched
            };

            _saveSystem.Save(data);
        }

        private void Load()
        {
            SaveData data = _saveSystem.Load();

            _playerData.LoadData(data);
            _score.LoadHighScore(data);
        }
        
        private IEnumerator AutoSave()
        {
            while (true)
            {
                Save();
                yield return new WaitForSeconds(AutoSaveInterval);
            }
        }
    }
}