using Unity.VisualScripting;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    public static PriceManager Instance { get; set; }


    public ShopPriceList ShopPriceList { get; private set; }

    private void Awake()
    {
        Instance = this;
        ShopPriceList = new ShopPriceList();
        StartCoroutine(ShopPriceList.LoadPriceListFromFile(null));
    }
}