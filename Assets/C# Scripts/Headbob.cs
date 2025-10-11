using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public float baseFrequency;
    public float baseSmooth;

    [Range(0.001f, 0.09f)]
    public float Amount = 0.03f;

    [Range(0f, 30f)]
    public float Frequency = 12.0f;

    [Range(10f, 100f)]
    public float Smooth = 18.0f;

    Vector3 StartPos;

    public PlayerController playerController;
    public GameObject player;

    private void Start()
    {
        StartPos = transform.localPosition;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        CHeckForHeadbobTrigger();
        StopHeadbob();

        if (playerController.state == PlayerController.MovementState.walking)
        {
            Frequency = baseFrequency;
            Smooth = baseSmooth;
        }
        if (playerController.state == PlayerController.MovementState.sprinting)
        {
            Frequency = baseFrequency * 2f;
            Smooth = baseSmooth + 12f;
        }
        if (playerController.state == PlayerController.MovementState.crouching)
        {
            Frequency = baseFrequency / 2f;
            Smooth = baseSmooth - 8f;
        }
        if (playerController.state == PlayerController.MovementState.air)
        {
            Recenter();
            Frequency = 0f;
        }

    }

    private void CHeckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0 )
        {
            StartHeadbob();
        }
    }

    private Vector3 StartHeadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }

    private void StopHeadbob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, Time.deltaTime *6);
    }

    private void Recenter()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, Time.deltaTime * 20);
    }
}
