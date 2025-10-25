using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
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

        #endregion

    }

}
