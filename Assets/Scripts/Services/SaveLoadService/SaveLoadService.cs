using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        public void Save<T>(string path, T value)
        {
            var bf = new BinaryFormatter();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                bf.Serialize(stream, value);
            }
        }

        public T Load<T>(string path)
        {
            var bf = new BinaryFormatter();
            T data;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                data = (T)bf.Deserialize(stream);
            }

            return data;
        }
    }
}