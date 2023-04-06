using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        public const string HighScore = nameof(HighScore);

        private ISaveSystem _saveSystem;

        public int HighScoreValue { get; private set; } = 0;

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

            _saveSystem.Save(data);
            print("Saved " + data.HighScore);
        }

        private void Load()
        {
            var data = _saveSystem.Load();

            HighScoreValue = data.HighScore;
            print("Loaded " + HighScoreValue);
        }
    }
}