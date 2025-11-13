using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerRaycast raycastScript;
    public GameObject mainAttack;

    public void Start()
    {
        raycastScript = PlayerRaycast.instance;
    }


    public void MainAttack()
    {
        if(mainAttack != null)
        {
            Instantiate(mainAttack, raycastScript.firepoint.position, Camera.main.transform.rotation); //too much referencing?
        }
        else
        {
            Debug.Log("FAILED TO ATTACK");
            return;
        }


        /*
         if(mainAttack != null)
        {
            raycastScript.RayCastByTag("Enemy");
        }
        else
        {
            Debug.Log("FAILED TO ATTACK");
            return;
        }
         */

    }

















































    //OLD CODE IS HERE, REVAMPED FOR GAME IS ABOVE

    /*
     public GameObject currentWeapon;

     public float mainValue;
     public float secondValue;
     public Sprite weaponIcon; //HUD icon holder Object has script to reference this variable, and set the icon on the HUD

     public void Update()
     {
         if (Input.GetKeyDown(KeyCode.Mouse0))
             MainAttack();
         if (Input.GetKeyDown(KeyCode.Mouse1))
             SecondaryAttack();
     }


     private bool WeaponEquipped()
     {
         Debug.Log("Weapon Status Checked");
         if (currentWeapon != null)
         {
             GetWeaponData();
             return true;
         } 
         else
         {
             return false;
         }

     }

     public void MainAttack()
     {
         if (!WeaponEquipped()) //if no weapon is detected, do nothing
         {
             return;
         }
         else if (WeaponEquipped()) //if a weapon is detected, use it
         {

             Debug.Log("Main Attack" + mainValue);
         }
     }
     public void SecondaryAttack()
     {
         if (!WeaponEquipped()) //if no weapon is detected, do nothing
         {
             return;
         }
         else if (WeaponEquipped()) //if a weapon is detected, use it
         {

             Debug.Log("Secondary Attack" + secondValue);
         }
     }

     public void GetWeaponData()
     {
         Weapon weaponScript = currentWeapon.GetComponent<Weapon>(); //get the script with the weapon's data

         //then apply the weapon's values to this script, so when attacking, we can add other values to the base weapon's
         mainValue = weaponScript.testNumber;
         secondValue = weaponScript.testNumber2;
         weaponIcon = weaponScript.icon;

     }
    */
}
