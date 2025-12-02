using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData itemData;

    [Header("Display Objects")]
    public TextMeshProUGUI nameSlot;
    public Image iconSlot;
    public Image raritySlot;

    public void Start()
    {
        DisplayItem();
    }
    public void DisplayItem()
    {
        if(itemData != null)
        {
            nameSlot.text = itemData.itemName;
            iconSlot.sprite = itemData.icon;
            
        }
        else
        {
            return;
        }
    }
}
