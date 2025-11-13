using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float testNumber;
    public float testNumber2;
    public Sprite icon;

    public PlayerWeapon playerWeaponScript;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerWeaponScript = player.GetComponent<PlayerWeapon>();
    }

    public void Equip()
    {
        //playerWeaponScript.currentWeapon = this.gameObject;
        //playerWeaponScript.GetWeaponData();
    }
}
