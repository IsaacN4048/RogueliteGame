using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance; //SINGLETON

    public GameObject player;

    public Vector2 NavigationInput { get; set; }

    private InputAction _navigationAction; //allows keyboard navigation between buttons

    public static PlayerInput _playerInput { get; set; }


    public bool MenuOpenCloseInput { get; private set; }
    private InputAction _menuOpenCloseAction;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        _playerInput = player.GetComponent<PlayerInput>();
        _navigationAction = _playerInput.actions["Navigate"];

        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();

        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
