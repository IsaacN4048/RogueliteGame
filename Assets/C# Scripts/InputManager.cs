using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance; //SINGLETON

    public Vector2 NavigationInput { get; set; }

    private InputAction _navigationAction;

    public static PlayerInput PlayerInput { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();
        _navigationAction = PlayerInput.actions["Navigate"];
    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
    }
}
