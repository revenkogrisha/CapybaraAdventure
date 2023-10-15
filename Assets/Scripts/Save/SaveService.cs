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

        private PlayerData _playerData;
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
        private void Construct(
            ISaveSystem saveSystem, 
            PlayerData playerData)
        {
            _saveSystem = saveSystem;
            _playerData = playerData;
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
print(_playerData);
print(data);
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