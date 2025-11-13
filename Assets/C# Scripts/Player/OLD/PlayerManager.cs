using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance; //SINGLETON

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        [Header("Components")]
        [SerializeField] FirstPersonController fpController;
        [SerializeField] PlayerWeapon playerWeapon;


        #region Input Handling
        void OnMove(InputValue value)
        {
            fpController.MoveInput = value.Get<Vector2>();
        }
        void OnLook(InputValue value)
        {
            fpController.LookInput = value.Get<Vector2>();
        }

        void OnSprint(InputValue value)
        {
            fpController.SprintInput = value.isPressed;
        }

        void OnJump(InputValue value)
        {
            if(value.isPressed)
            {
                fpController.TryJump();
            }
        }

        //POTENTIALLY

        void OnAttack(InputValue value) //currently calls PlayerWeapon method, instead of directly referencing playerRaycast... FIX
        { 
            if(value.isPressed)
            {
                playerWeapon.MainAttack();
                Debug.Log("TRIED TO ATTACK");
            }
        }
        #endregion


        #region Unity Methods

        void OnValidate()
        {
            if(fpController == null) fpController = GetComponent<FirstPersonController>();
        }

        void Start()
        {
            SetFirstPersonCursor();
            playerWeapon = GetComponent<PlayerWeapon>();
        }

        public void SetFirstPersonCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void FreeCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        #endregion

    }

}
