using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        CursorFollowsPlayer();
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //stops mouse from rotating 360 degrees

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void CursorFollowsPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked; //makes cursor locked into very CNETER of screen
        Cursor.visible = false; //makes default cursor invisible;
    }
    public void FreeCursor()
    {
        Cursor.lockState = CursorLockMode.None; //allows you to use cursor for UI
        Cursor.visible = true; //allows you to see your cursor again;
    }
}
