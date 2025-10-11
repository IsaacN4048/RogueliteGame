using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HUDcontroller : MonoBehaviour
{
    public static HUDcontroller instance; //this is a singleton

    private void Awake() //this is the rest of the singleton
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;
    [SerializeField] GameObject inventoryPanel;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (E)"; //create keybind functionality later, and implement here to show what button to press
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        /*if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }

        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
        } */
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
