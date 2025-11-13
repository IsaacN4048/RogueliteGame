using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public PlayerWeapon weaponScript;
    public Image icon;

    public void Update()
    {
        /*
        if(weaponScript.weaponIcon != null)
        {
            icon.color = Color.white;
            icon.sprite = weaponScript.weaponIcon; //happens too early, there is no sprite detected
        }
        if (weaponScript.currentWeapon == null)
        {
            icon.color = Color.clear;
        }
        */
    }
}
