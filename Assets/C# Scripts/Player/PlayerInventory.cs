using NUnit.Framework;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] itemSlots; //array of inventory spaces

    public void FindSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++) //loop through itemSlots
        {
            ItemDisplay itemSlotScript = itemSlots[i].GetComponent<ItemDisplay>();
            ItemData itemSlotData = itemSlotScript.itemData;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GroundItem"))
        {
            //plops the item into the first Item Slot only
            ItemDisplay itemSlotScript = itemSlots[0].GetComponent<ItemDisplay>();
            itemSlotScript.itemData = other.GetComponent<ItemData>();
            itemSlotScript.DisplayItem();

            Destroy(other.gameObject);
        }
    }

}
