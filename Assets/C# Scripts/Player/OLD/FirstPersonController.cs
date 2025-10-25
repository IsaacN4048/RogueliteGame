using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using TMPro;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Looking Parameters")]
        public Vector2 LookSensitivity = new Vector2(0.1f, 0.1f);

        public float PitchLimit = 85f; //limits 360 movement, breaking your neck

        [SerializeField] float currentPitch = 0f;

        public float CurrentPitch
        {
            get => currentPitch;

            set
            {
                currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
            }
        }

        [Header("Movement Parameters")]
        public float maxSpeed = 3.5f;
        public float acceleration = 15f;

        public Vector3 currentVelocity { get; private set; }
        public float currentSpeed { get; private set; }

        [Header("Input")]
        public Vector2 MoveInput;
        public Vector2 LookInput;


        [Header("Components")]
        [SerializeField]CharacterController characterController;
        [SerializeField] CinemachineCamera fpCamera;
        public TextMeshProUGUI speedometer;



        #region Unity Methods

        private void OnValidate()
        {
            if(characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        void Update()
        {
            MoveUpdate();
            LookUpdate();
            speedometer.text = currentSpeed.ToString(); //FOR TESTING PURPOSES
        }

        #endregion


        #region Controller Methods

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
            float verticalVelocity = Physics.gravity.y * 20f * Time.deltaTime;

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

        #endregion














    }

}
