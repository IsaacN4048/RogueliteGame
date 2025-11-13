using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public static PlayerRaycast instance; //SINGLETON

    public Transform firepoint;
    public Camera cameraMain;
    public PlayerWeapon playerWeaponScript;

    public void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //CastRay();
    }

    public void CastRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("Test"))
            {
                Debug.DrawRay(cameraMain.transform.position, cameraMain.transform.forward * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(cameraMain.transform.position, cameraMain.transform.forward * hit.distance, Color.yellow);
            }
           
        }

    }

    public void RayCastByTag(string tag)
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(tag))
            {
                if(tag == "Test")
                {

                }
                if(tag == "Enemy")
                {
                    //Instantiate(playerWeaponScript.mainAttack, firepoint);
                }
                Debug.DrawRay(cameraMain.transform.position, cameraMain.transform.forward * hit.distance, Color.blue);
            }
            else
            {
                return;
            }

        }

    }
}
