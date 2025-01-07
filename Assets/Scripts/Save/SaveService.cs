using CapybaraAdventure.Ad;
using UnityEngine;
using CapybaraAdventure.Player;
using Cysharp.Threading.Tasks;
using CapybaraAdventure.Other;
using CapybaraAdventure.Game;
using CapybaraAdventure.Level;
using Core.Player;
using Zenject;

namespace CapybaraAdventure.Save
{
    [DefaultExecutionOrder(1)]
    public class SaveService : MonoBehaviour
    {
        private const float AutoSaveIntervalInSeconds = 10f;

        [SerializeField] private Score _score;

        private PlayerData _playerData;
        private ISaveSystem _saveSystem;
        private LocalizationManager _localization;
        private HeroSkins _heroSkins;
        private LevelNumberHolder _levelNumberHolder;

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
            HeroSkins heroSkins,
            LevelNumberHolder levelNumberHolder)
        {
            _playerData = playerData;
            _localization = localization;
            _saveSystem = saveSystem;
            _heroSkins = heroSkins;
            _levelNumberHolder = levelNumberHolder;
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
                Food = _playerData.Food,
                MaxDistance = _playerData.MaxDistance,
                DistanceUpgradeCost = _playerData.DistanceUpgradeCost,
                FoodBonusNEW = _playerData.FoodBonus,
                CommonUpgradeCost = _playerData.CommonUpgradeCost,
                IsCutsceneWatched = _playerData.IsCutsceneWatched,
                LanguageIndex = _localization.CurrentLanguageIndex,
                BoughtHeroSkins = _heroSkins.BoughtSkins,
                CurrentHeroSkin = _heroSkins.Current.Name,
                Level = _levelNumberHolder.Level,
                NoAds = AdTimer.Instance.NoAds
            };

            _saveSystem.Save(data);
        }

        private async void Load()
        {
            SaveData data = _saveSystem.Load();

            _playerData.LoadData(data);
            _score.LoadHighScore(data.HighScore);
            // NEW
            _heroSkins.Load(data);
            _levelNumberHolder.Load(data);
            AdTimer.Instance.SetNoAds(data.NoAds);
            // ––––
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