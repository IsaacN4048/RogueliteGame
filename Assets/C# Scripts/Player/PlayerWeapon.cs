using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerRaycast raycastScript;
    public GameObject mainAttack;
    public Image mainAttackCooldownImage;
    public GameObject secondaryAttack;
    public Image secondaryAttackCooldownImage;

    [Header("Cooldowns")]

    public float mainCooldownTime;
    public bool canMainAttack = true;

    public float secondaryCooldownTime;
    public bool canSecondaryAttack = true;


    public void Start()
    {
        raycastScript = PlayerRaycast.instance;
        UpdateAttacks();
       
    }

    public void UpdateAttacks()
    {
        if (mainAttack != null && secondaryAttack != null)
        {
            //get cooldowns from the specific projectiles
            float mainCooldown = mainAttack.GetComponent<RangedProjectile>().cooldown;
            float secondaryCooldown = secondaryAttack.GetComponent<RangedProjectile>().cooldown;

            //apply them to the variables here, in the future, run them through a formula that multiplies by PlayerStats.cooldown
            mainCooldownTime = mainCooldown;
            secondaryCooldownTime = secondaryCooldown;
        }
        else
            return;
    }

    public void MainAttack()
    {
        if(canMainAttack && mainAttack != null)
        {
            Instantiate(mainAttack, raycastScript.firepoint.position, Camera.main.transform.rotation); //too much referencing?
            canMainAttack = false;
            StartCoroutine(MainAttackCooldown());
            
        }
        else
        {
            Debug.Log("FAILED TO ATTACK");
            return;
        }
    }

    private IEnumerator MainAttackCooldown()
    {
        //yield return new WaitForSeconds(mainCooldownTime);

        float timer = 0f;
        mainAttackCooldownImage.fillAmount = 1f;

        while(timer < mainCooldownTime)
        {
            timer += Time.deltaTime;
            mainAttackCooldownImage.fillAmount = 1f - (timer / mainCooldownTime);
            yield return null;
        }

        mainAttackCooldownImage.fillAmount = 0f;
        canMainAttack=true;
    }

    public void SecondaryAttack()
    {
        if (canSecondaryAttack && secondaryAttack != null)
        {
            Instantiate(secondaryAttack, raycastScript.firepoint.position, Camera.main.transform.rotation); //too much referencing?
            canSecondaryAttack = false;
            StartCoroutine(SecondaryAttackCooldown());
        }
        else
        {
            Debug.Log("FAILED TO ATTACK");
            return;
        }

    }

    private IEnumerator SecondaryAttackCooldown()
    {
        //yield return new WaitForSeconds(secondaryCooldownTime);

        float timer = 0f;
        secondaryAttackCooldownImage.fillAmount = 1f;

        while (timer < secondaryCooldownTime)
        {
            timer += Time.deltaTime;
            secondaryAttackCooldownImage.fillAmount = 1f - (timer / secondaryCooldownTime);
            yield return null;
        }

        secondaryAttackCooldownImage.fillAmount = 0f;
        canSecondaryAttack = true;
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
