using System;
using CapybaraAdventure.Save;

namespace CapybaraAdventure.Level
{
    public class LevelNumberHolder
    {
        private int _level = 1;
        private int _locationNumber = 0;
        
        public int Level => _level;

        public int LocationNumber
        {
            get => _locationNumber;
            set => _locationNumber = value;
        }

        public bool AreLocationsInitialized { get; set; } = false;

        public event Action<int> OnLevelChanged;

        public void NextLevel()
        {
            _level++;
            OnLevelChanged?.Invoke(_level);
        }

        public void Load(SaveData saveData)
        {
            _level = saveData.Level;
        }
    }
}