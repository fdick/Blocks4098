using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ShopPriceList
{
    public class Price
    {
        public Price(int usedProductPrice, int newProductPrice)
        {
            UsedProductPrice = usedProductPrice;
            NewProductPrice = newProductPrice;
        }

        public int UsedProductPrice { get; private set; }
        public int NewProductPrice { get; private set; }
    }

    private Dictionary<string, Price> PriceList_d { get; set; } = new Dictionary<string, Price>();
    private AsyncOperationHandle<TextAsset> _asyncHandle;

    private const string PRICE_FILENAME = "shopPriceList";


    public bool IsLoaded => _asyncHandle.IsDone;

    public void Release()
    {
        PriceList_d.Clear();
        if (_asyncHandle.IsValid())
            Addressables.Release(_asyncHandle);
    }

    public Price GetPrice(string itemName)
    {
        if (!IsLoaded)
            return null;
        if (PriceList_d.TryGetValue(itemName, out var price))
            return price;
        return null;
    }

    public IEnumerator LoadPriceListFromFile(Action onLoaded)
    {
        _asyncHandle = Addressables.LoadAssetAsync<TextAsset>(PRICE_FILENAME);

        yield return _asyncHandle;

        if (_asyncHandle.Status == AsyncOperationStatus.Succeeded)
        {
            MemoryStream mStream = new MemoryStream(_asyncHandle.Result.bytes);
            StreamReader stream = new StreamReader(mStream);
            var asyncRead = stream.ReadToEndAsync();

            yield return asyncRead;

            mStream.Close();
            stream.Close();

            ParseLoadedFile(asyncRead.Result);

            onLoaded?.Invoke();
            Debug.Log("PriceList is inited");
        }
        else
            Debug.Log("Cant load price list!");
    }

    private void ParseLoadedFile(string stringFile)
    {
        var step1 = stringFile.Split('-', '\n');

        for (int i = 0; i < step1.Length; i += 3)
            PriceList_d.Add(
                step1[i],
                new Price(Convert.ToInt32(step1[i + 2]), Convert.ToInt32(step1[i + 1])));
    }
}