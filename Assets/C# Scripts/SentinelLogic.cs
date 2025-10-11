using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelLogic : MonoBehaviour
{
    private Animator animator;
    public bool Awake;
    public bool PlayerPresent;
    public float aggroRadius;
    private GameObject player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(Awake && Vector3.Distance(player.transform.position, gameObject.transform.position) >= aggroRadius)
        {
                Sleep();
            Debug.Log("Totem detects No player within radius");
        }
        
    }

    public void Waken()  //triggers when player enters trigger
    {
        PlayerPresent = true;
        if (!Awake) //only wakes when is asleep
        {
            animator.SetTrigger("Awake");
            Awake = true;
        }
    }
    public void Sleep() //triggers when player exits aggroRange
    {
        PlayerPresent = false;
        Awake = false;
        animator.SetTrigger("Sleep");
    }
}
