using System.IO;
using UnityEngine;

namespace CapybaraAdventure.Save
{
    public class JsonSaveSystem : ISaveSystem
    {
        public const string FileName = "/CapySave.json";

        public string FilePath { get; private set; }

        public JsonSaveSystem()
        {
            FilePath = Application.persistentDataPath + FileName;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);

            var writer = new StreamWriter(FilePath);
            using (writer)
                writer.Write(json);
        }

        public SaveData Load()
        {
            if (File.Exists(FilePath) == false)
                return new SaveData();

            string json = "";

            var reader = new StreamReader(FilePath);
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