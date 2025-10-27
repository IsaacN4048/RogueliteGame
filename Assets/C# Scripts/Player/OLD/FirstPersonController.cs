using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Movement Parameters")]/////////////////////////////////////////////////////////////

        public float maxSpeed => SprintInput ? sprintSpeed : walkSpeed;
        public float acceleration = 0f;
        public float airAcceleration = 20f;
        public float groundAcceleration = 50f;

        [SerializeField] float walkSpeed;
        [SerializeField] float sprintSpeed;

        [SerializeField] float jumpHeight = 2f;
        public bool Sprinting
        {
            get
            {
                return SprintInput && currentSpeed > 0.1f;
            }
        }

        [Header("Physics Parameters")]/////////////////////////////////////////////////////////////

        [SerializeField] float gravityScale = 3f;

        public float verticalVelocity = 0f;
        public Vector3 currentVelocity { get; private set; }
        public float currentSpeed { get; private set; }

        public bool IsGrounded => characterController.isGrounded;



        [Header("Looking Parameters")]//////////////////////////////////////////////////////////////

        public Vector2 LookSensitivity = new Vector2(0.1f, 0.1f);
        public float PitchLimit = 85f; //limits camera rotation angle
        [SerializeField] float currentPitch = 0f;

        public float CurrentPitch
        {
            get => currentPitch;

            set
            {
                currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
            }
        }




        [Header("Camera Parameters")]///////////////////////////////////////////////////////////////

        [SerializeField] public float defaultFOV = 70f;
        [SerializeField] float sprintFOV = 85f;
        [SerializeField] float cameraFOVSmoothing = 1f;
        float TargetCameraFOV
        {
            get
            {
                return Sprinting ? sprintFOV : defaultFOV;
            }
        }


        [Header("Input")]///////////////////////////////////////////////////////////////////////////

        public Vector2 MoveInput;
        public Vector2 LookInput;
        public bool SprintInput;

       

        [Header("Components")]/////////////////////////////////////////////////////////////////////

        [SerializeField] CharacterController characterController;
        [SerializeField] CinemachineCamera fpCamera;
        [SerializeField] PlayerStats playerStats;
        public TextMeshProUGUI speedometer;

       

        



        #region Unity Methods

        private void OnValidate()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
            if (playerStats == null)
            {
                playerStats = GetComponent<PlayerStats>();
            }
        }

        void Update()
        {
            MoveUpdate();
            LookUpdate();
            CameraUpdate();
            CalculateSpeed();

            speedometer.text = currentSpeed.ToString(); //FOR TESTING PURPOSES

            /*
            if(currentPitch > PitchLimit)
            {
                currentPitch = PitchLimit;
            }
            else if(currentPitch < -PitchLimit)
            {
                currentPitch = -PitchLimit;
            }
            */
        }

        #endregion


        #region Controller Methods

        public void TryJump()
        {
            if(IsGrounded == false)
            {
                return;
            }

            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);

        }

        void MoveUpdate()
        {
            Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
            motion.y = 0f;
            motion.Normalize();

            
            if(motion.sqrMagnitude >= 0.01f) //if youre inputting movement keys
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, motion * maxSpeed, acceleration * Time.deltaTime);
            }
            else //if youre standing still
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
            }

            //HANDLING GRAVITY
            if (IsGrounded && verticalVelocity <= 0.01f) //keeps player grounded while not jumping
            {
                verticalVelocity = -3f;
                acceleration = groundAcceleration;
            }
            else //jumping logic
            {
                verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
                acceleration = airAcceleration;
            }

           

            Vector3 fullVelocity = new Vector3(currentVelocity.x, verticalVelocity, currentVelocity.z);

            characterController.Move(fullVelocity * Time.deltaTime);

            //updating speed
            currentSpeed = currentVelocity.magnitude;
        }

        void LookUpdate()
        {

            Vector2 input = new Vector2(LookInput.x * LookSensitivity.x, LookInput.y * LookSensitivity.y);

            //looking up and down
            
            currentPitch -= input.y;
            fpCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);


            //looking left and right
            transform.Rotate(Vector3.up * input.x);
        }

        void OnLook(InputValue value)
        {
            LookInput = value.Get<Vector2>();
        }

        void CameraUpdate()
        {

            float targetFOV = defaultFOV;

            sprintFOV = defaultFOV + 10f;

            if(Sprinting)
            {
                float speedRatio = currentSpeed / sprintSpeed;
                targetFOV = Mathf.Lerp(defaultFOV, sprintFOV, speedRatio);
            }

            fpCamera.Lens.FieldOfView = Mathf.Lerp(fpCamera.Lens.FieldOfView, targetFOV, cameraFOVSmoothing * Time.deltaTime);
        }

        void CalculateSpeed()
        {
            walkSpeed = playerStats.moveSpeed.totalValue;
            sprintSpeed = walkSpeed * 2.5f;
        }


      

        #endregion














    }

}
