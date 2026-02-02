using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance; //SINGLETON

    public GameObject player;

   
    public static PlayerInput playerInput { get; set; } //THE KEYBINDING COMPONENT ON THE PLAYER
    public bool MenuOpenCloseInput { get; private set; }
    public Vector2 NavigationInput { get; set; }
    private InputAction _menuOpenCloseAction; //is Tab
    private InputAction _navigationAction; //allows keyboard navigation between buttons


    [Header("Holding Keys")]
    [SerializeField] public bool holdingLeft; //set bool here, reference it in PlayerWeapon
    [SerializeField] public bool holdingRight;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        playerInput = player.GetComponent<PlayerInput>();
        _navigationAction = playerInput.actions["Navigate"];

        _menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }

    private void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();

        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();


        holdingLeft = playerInput.currentActionMap["Attack"].ReadValue<float>() > 0;
        holdingRight = playerInput.currentActionMap["SecondaryAttack"].ReadValue<float>() > 0;

    }
}
