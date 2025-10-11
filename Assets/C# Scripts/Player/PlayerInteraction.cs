using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    private Interactible currentInteractible;
    public Camera cameraMain;

    [Header("Keybind")]
    public KeyCode interact;
    // Update is called once per frame
    void Update()
    {
        //CheckInteraction();
        if(Input.GetKeyDown(interact) && currentInteractible != null)
        {
            currentInteractible.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraMain.transform.position, cameraMain.transform.forward);

        if(Physics.Raycast(ray, out hit, playerReach)) //only looks for interactible script if object hit is within range
        {
            if(hit.collider.gameObject.GetComponent<Interactible>() != null) //if the object hit has an interactible script on it
            {
                Interactible newInteractible = hit.collider.GetComponent<Interactible>();

                //if the currentInteractible is not the newest Interactible being looked at
                if(currentInteractible && newInteractible != currentInteractible)
                {
                    currentInteractible.DisableOutline();
                }

                if(newInteractible.enabled)
                {
                    SetNewCurrentInteractible(newInteractible);
                }
                else //if interactible is disabled
                {
                    DisableCurrentInteractible();
                }
            }
            else //if doesnt have interactibe script
            {
                DisableCurrentInteractible();
            }
        }
        else //if nothing is within reach
        {
            DisableCurrentInteractible();
        }
    }

    private void SetNewCurrentInteractible(Interactible newInteractible)
    {
        currentInteractible = newInteractible;
        currentInteractible.EnableOutline();
        HUDcontroller.instance.EnableInteractionText(currentInteractible.popupText);
    }

    void DisableCurrentInteractible()
    {
        HUDcontroller.instance.DisableInteractionText();
        if(currentInteractible)
        {
            currentInteractible.DisableOutline();
            currentInteractible = null;
        }
    }
}
