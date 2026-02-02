using NUnit.Framework;
using Player;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] itemSlots; //array of inventory spaces
    public PlayerWeapon weaponScript;//the player's script for weapon managing

    public void Start()
    {
        weaponScript = PlayerManager.instance.gameObject.GetComponent<PlayerWeapon>(); //sets script to proper one
    }

    public void FindSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++) //loop through itemSlots
        {
            ItemDisplay itemSlotScript = itemSlots[i].GetComponent<ItemDisplay>();
            ItemData itemSlotData = itemSlotScript.itemData;
        }
    }



    private void OnTriggerEnter(Collider other) //NEEDS OVERHAUL
    {
        if(other.CompareTag("GroundItem"))
        {
            //plops the item into the first Item Slot only
            ItemDisplay itemSlotScript = itemSlots[0].GetComponent<ItemDisplay>();
            itemSlotScript.itemData = other.GetComponent<ItemData>();
            itemSlotScript.DisplayItem();

            if(other.GetComponent<Weapon>() != null)
            {
                weaponScript.equippedWeapon = other.gameObject;
                weaponScript.UpdateAttacks();
                return;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

}
