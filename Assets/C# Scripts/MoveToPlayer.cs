using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private Transform playerTransform;

    public bool isMoving;
    public float smoothTime = 0.1f;
    public float collectDistance = 0.5f;
    public Vector3 targetOffset = new Vector3(0, 1.5f, 0);
    private Vector3 velocity = Vector3.zero;
    
    public Rigidbody body;
    public Collider collllider;

    public void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    public void Update()
    {
        if(isMoving)
        {
            Destroy(body); body = null;
            Destroy(collllider); collllider = null;

            Vector3 targetPos = playerTransform.position + targetOffset;

            float distance = Vector3.Distance(transform.position, targetPos);

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

            if(distance < collectDistance)
            {
                Collect();
            }
        }
        else
        {
            return;
        }
    }

    public void Attract()
    {
        isMoving = true;
    }

    public void Collect()
    {
        Debug.Log("+XP");
        Destroy(gameObject);
    }
}
