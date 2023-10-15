using System.IO;
using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class JsonSaveSystem : ISaveSystem
    {
        public const string SaveFilePath = "/CapySave.json";

        public string FilePath { get; private set; }

        public JsonSaveSystem()
        {
            FilePath = Application.persistentDataPath + SaveFilePath;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);

            using (var writer = new StreamWriter(FilePath))
                writer.Write(json);
        }

        public SaveData Load()
        {
            if (File.Exists(FilePath) == false)
                return new SaveData();

            string json = "";

            using (var reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    json += line;

                if (string.IsNullOrEmpty(json))
                    return new SaveData();

                return JsonUtility.FromJson<SaveData>(json);
            }
        }
    }
}