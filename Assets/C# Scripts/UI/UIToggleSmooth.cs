using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleSmooth : MonoBehaviour
{
    public float speed;
    [Header("Keybinds")]
    public KeyCode toggle;

    [Header("Positions")]
    public Transform offscreenPos;
    public Transform onscreenPos;

    [Header("UI Objects")]
    public GameObject UIobject;

    private void Update()
    {
        if(Input.GetKeyDown(toggle))
        {
            TranslateUI();
        }
    }
    public void TranslateUI()
    {
        if(UIobject.transform.position == offscreenPos.position)
        {
            UIobject.transform.position = onscreenPos.position;
        }
        else if (UIobject.transform.position == onscreenPos.position)
        {
            UIobject.transform.position = offscreenPos.position;
        }
    }

}
