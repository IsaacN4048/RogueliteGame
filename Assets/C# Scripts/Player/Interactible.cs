using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{
    //Outline outline;
    public string popupText;
    public UnityEvent onInteraction;

    private void Start()
    {
        //outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        //outline.enabled = false;
    }
    public void EnableOutline()
    {
        //outline.enabled = true;
    }

    public void RemoveThis() //works if script is on parent object
    {
        Destroy(gameObject);
    }
}
