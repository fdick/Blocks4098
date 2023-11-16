using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        public readonly string SAVE_FILE_NAME = "save";
        public void Save<T>(string fileName, T value)
        {
            var dir = $@"{Application.persistentDataPath}\{fileName}.s";
            var bf = new BinaryFormatter();
            using (var stream = new FileStream(dir, FileMode.OpenOrCreate))
            {
                bf.Serialize(stream, value);
            }
        }

        public T Load<T>(string fileName)
        {
            var dir = $@"{Application.persistentDataPath}\{fileName}.s";

            var bf = new BinaryFormatter();
            T data;
            if (!File.Exists(dir))
                return default;
            using (var stream = new FileStream(dir, FileMode.Open))
            {
                data = (T)bf.Deserialize(stream);
            }

            return data;
        }
    }
}