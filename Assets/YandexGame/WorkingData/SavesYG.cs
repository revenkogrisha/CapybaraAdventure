using CapybaraAdventure.Save;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public int HighScore = 0;
        public int Coins = 0;
        public float MaxDistance = 15f;
        public int DistanceUpgradeCost = 15;
        public float FoodBonus = 0;
        public int FoodUpgradeCost = 15;


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }

        public static SavesYG operator+(SavesYG savesYG, SaveData saveData)
        {
            savesYG.HighScore = saveData.HighScore;
            savesYG.Coins = saveData.Coins;
            savesYG.MaxDistance = saveData.MaxDistance;
            savesYG.DistanceUpgradeCost = saveData.DistanceUpgradeCost;
            savesYG.FoodBonus = saveData.FoodBonus;
            savesYG.FoodUpgradeCost = saveData.FoodUpgradeCost;
            return savesYG;
        }
    }
}
