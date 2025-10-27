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
        #endregion


        #region Unity Methods

        void OnValidate()
        {
            if(fpController == null) fpController = GetComponent<FirstPersonController>();
        }

        void Start()
        {
            SetFirstPersonCursor();
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
