using System.IO;
using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class JsonSaveSystem : ISaveSystem
    {
        public const string FileName = "/CapySave.json";

        private readonly string _filePath;

        public JsonSaveSystem()
        {
            _filePath = Application.persistentDataPath + FileName;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);

            var writer = new StreamWriter(_filePath);
            using (writer)
                writer.Write(json);
        }

        public SaveData Load()
        {
            if (File.Exists(_filePath) == false)
                return new SaveData();

            string json = "";

            var reader = new StreamReader(_filePath);
            using (reader)
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