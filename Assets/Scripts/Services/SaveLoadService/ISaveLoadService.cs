namespace Code.Services
{
    public interface ISaveLoadService
    {
        public void Save<T>(string path, T value);
        public T Load<T>(string path);
    }
}