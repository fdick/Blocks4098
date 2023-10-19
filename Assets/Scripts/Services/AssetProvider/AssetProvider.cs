using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string path)
        {
            return Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
        }

        public async Task<T> LoadAsync<T>(string path)
        {
            var t = Addressables.LoadAssetAsync<T>(path);
            await t.Task;
            return t.Result;
        }
    }
}