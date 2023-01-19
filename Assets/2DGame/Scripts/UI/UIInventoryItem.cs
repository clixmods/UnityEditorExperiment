using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image icon;

    public void GenerateButton(ItemScriptableObject item, int itemAmount)
    {
        if(itemAmount > 1)
        {
            textAmount.text = itemAmount.ToString();
        }
        else
        {
            textAmount.text = string.Empty;
        }
        textName.text = item.name;
        icon.sprite = item.icon;
    }
}
