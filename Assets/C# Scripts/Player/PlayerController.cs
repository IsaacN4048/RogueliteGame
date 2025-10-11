using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider walkingCollider;

    public Stat speedStat;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public bool canSprint = true;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;
    public float airDrag;
    public float airGravity;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    private float startYscale;
    public bool canCrouch = true;
    public bool canUncrouch;
    public Collider crouchCollider;
    public bool isCrouching;

    [Header("Camera")]
    public Transform camTransform;
    public Transform walkingCam;
    public Transform crouchedCam;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask whatIsGround;
    public bool grounded;  //public for debug reasons

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    public bool exitingSlope;


    //TESTING
    public TextMeshProUGUI speedText;

    //public string currentSpeed; //TESTING

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state; //or currentState
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYscale = transform.localScale.y;
    }

    private void Update()
    {
        //TESTING
        speedText.text = (rb.linearVelocity.magnitude).ToString("0.0");

        //grounded check
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();
        CalculateSpeeds();

        //handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = airDrag;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void StateHandler()
    {

        //Mode - Sprinting
        if (canSprint && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            canSprint = true;
            canCrouch = true;
        }

        //Mode - Crouching
        
        //if(Input.GetKey(crouchKey) && canCrouch)
        if(isCrouching)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }


        //Mode - Midair

        else if (!grounded)
        {
            state = MovementState.air;
            canSprint = false;
            canCrouch = false;

            //REMOVE ME IF YOU WANT JUMP DISTANCE TO BE UNCHANGEABLE MID AIR
            if(Input.GetKeyUp(sprintKey))
            {
                moveSpeed = walkSpeed; //if you stop holding shift midair, your jump distance is shortened. MAYBE not good for parkour?
            }
        }
        
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jump logic
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //crouching logic
        if(canCrouch && Input.GetKeyDown(crouchKey)) //START CROUCH
        {
            walkingCollider.isTrigger = true;
            crouchCollider.enabled = true;
            isCrouching = true;

            camTransform.position = crouchedCam.position;



            if(state == MovementState.crouching && canUncrouch)
            {
                walkingCollider.isTrigger = false;
                crouchCollider.enabled = false;
                isCrouching = false;

                camTransform.position = walkingCam.position;
            }
            
        }


        /*
        if (canUncrouch && Input.GetKeyUp(crouchKey)) //NEEDS FIXING< PUSHES YOU THRU GROUND IF WALKING BEAN IS OBSTRUCTED
        {
            walkingCollider.isTrigger = false;
            crouchCollider.enabled = false;
            isCrouching = false;

            camTransform.position = walkingCam.position;

            //transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);

        }

        */

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            //applies downward force to cancel out the gravity dragging you down, only when moving
            if(rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 10f, ForceMode.Force);

        }

        //on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force); //increase (10f) float value when sprinting?

        //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //turn gravity off when on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {

        //limiting speed on slopes
        if(OnSlope() && !exitingSlope)
        {
            if(rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
        
        //limit speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)                         //if youre going faster than you should be
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;  //calculate what your top speed should be
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);  //and apply it
            }
        }

       
    }

    private void CalculateSpeeds()
    {
        walkSpeed = speedStat.totalValue;
        sprintSpeed = walkSpeed * 2f;
        crouchSpeed = walkSpeed / 2f;
    }

    private void Jump()
    {
        exitingSlope = true;

        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); //keeps upward velocity from being calculated, ie jumping while going up or downhill

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        exitingSlope = false;

        readyToJump = true;
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        canUncrouch = false;
    }
    private void OnTriggerExit(Collider other)
    {
        canUncrouch = true;
        //isCrouching = false;
    }

    
}
