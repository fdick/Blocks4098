using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIRefs : MonoBehaviour
{
    [field: SerializeField] public GameObject BotGO { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PriceValueText { get; private set; }
}
