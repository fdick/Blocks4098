using System.Threading.Tasks;

namespace Code.Services
{
    public interface IAssetProvider
    {
        public T Load<T>(string path);
        public Task<T> LoadAsync<T>(string path);
    }
}