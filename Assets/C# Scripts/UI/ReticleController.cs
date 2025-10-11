using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Image reticleIcon;
    public PlayerController playerController;

    [Header("Sprites")]
    public Sprite walking;
    public Sprite sprinting;
    public Sprite crouching;
    public Sprite none;

    public void Update()
    {
        if (playerController.state == PlayerController.MovementState.walking)
        {
            reticleIcon.sprite = walking;
            reticleIcon.color = Color.black;
        }
        if (playerController.state == PlayerController.MovementState.sprinting)
        {
            reticleIcon.sprite = sprinting;
            reticleIcon.color = Color.black;
        }
        if (playerController.state == PlayerController.MovementState.crouching)
        {
            reticleIcon.sprite = crouching;
            reticleIcon.color = Color.black;
        }
    }
}
