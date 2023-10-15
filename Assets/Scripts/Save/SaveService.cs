using UnityEngine;
using CapybaraAdventure.Player;
using Cysharp.Threading.Tasks;
using CapybaraAdventure.Other;
using Zenject;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        private const float AutoSaveIntervalInSeconds = 10f;

        [SerializeField] private Score _score;
        [SerializeField] private PlayerData _playerData;

        private ISaveSystem _saveSystem;

        #region MonoBehaviour

        private void Awake()
        {
            Load();
            AutoSave().Forget();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        #endregion

        [Inject]
        private void Construct(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

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
        
        private async UniTask AutoSave()
        {
            while (this != null)
            {
                Save();
                await MyUniTask.Delay(AutoSaveIntervalInSeconds);
            }
        }
    }
}