using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerRaycast raycastScript;
    public GameObject mainAttack;

    [Header("Ammo")]

    public int currentAmmo;
    public int maxAmmo;
    public int reserveAmmo; //SHOULD BE DETERMINED BY AMMO WITHIN INVENTORY

    public void Start()
    {
        raycastScript = PlayerRaycast.instance;
        currentAmmo = maxAmmo; //starts you with max ammo every round
    }


    public void MainAttack()
    {
        if(mainAttack != null && currentAmmo >= 1)
        {
            Instantiate(mainAttack, raycastScript.firepoint.position, Camera.main.transform.rotation); //too much referencing?
            currentAmmo = currentAmmo - 1;
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

    public void Reload() 
    {
        if(reserveAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo; //add max bullets from reserve
            reserveAmmo = reserveAmmo - maxAmmo; //remove added bullets from reserve
        }
        else if(reserveAmmo >= 1 && reserveAmmo < maxAmmo)
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }
        else
        {
            return;
        }
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
