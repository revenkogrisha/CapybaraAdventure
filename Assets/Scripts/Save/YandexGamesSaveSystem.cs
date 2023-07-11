using YG;

namespace CapybaraAdventure.Save
{
    public class YandexGamesSaveSystem : ISaveSystem
    {
        public void Save(SaveData data)
        {
            YandexGame.savesData += data;
            YandexGame.SaveProgress();
        }

        public SaveData Load()
        {
            return (SaveData)YandexGame.savesData;
        }
    }
}