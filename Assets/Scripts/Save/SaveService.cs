using UnityEngine;
using CapybaraAdventure.Player;
using Cysharp.Threading.Tasks;
using CapybaraAdventure.Other;
using CapybaraAdventure.Game;
using Core.Player;
using Zenject;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        private const float AutoSaveIntervalInSeconds = 10f;

        [SerializeField] private Score _score;

        private PlayerData _playerData;
        private ISaveSystem _saveSystem;
        private LocalizationManager _localization;
        private HeroSkins _heroSkins;

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
            PlayerData playerData,
            LocalizationManager localization,
            HeroSkins heroSkins)
        {
            _playerData = playerData;
            _localization = localization;
            _saveSystem = saveSystem;
            _heroSkins = heroSkins;
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
                IsCutsceneWatched = _playerData.IsCutsceneWatched,
                LanguageIndex = _localization.CurrentLanguageIndex,
                BoughtHeroSkins = _heroSkins.BoughtSkins,
                CurrentHeroSkin = _heroSkins.Current.Name
            };

            _saveSystem.Save(data);
        }

        private async void Load()
        {
            SaveData data = _saveSystem.Load();

            _playerData.LoadData(data);
            _score.LoadHighScore(data.HighScore);
            _heroSkins.Load(data);
            await _localization.SetLanguage(data.LanguageIndex);
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