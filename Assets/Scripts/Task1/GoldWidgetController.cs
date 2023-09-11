using UnityEngine;

public class GoldWidgetController : MonoBehaviour
{
    public int GoldCurrency { get; private set; }
    [SerializeField] private GoldWidgetUIRefs _data;

    public void AddValue(int value)
    {
        GoldCurrency += value;
        _data.GoldText.text = GoldCurrency.ToString();
    }

    public void SetValue(int value)
    {
        GoldCurrency = value;
        _data.GoldText.text = GoldCurrency.ToString();
    }
}