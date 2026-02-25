using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelLogic : MonoBehaviour //BECAUSE OF HOW HEALTHBAR LOGIC WORKS, YOU CAN SEE IT WHILE ASLEEP... FIX
{
    private Animator animator;
    public bool Awake;
    public bool PlayerPresent;
    public float aggroRadius;
    private GameObject player;

    public Enemy enemy;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        enemy = gameObject.GetComponentInChildren<Enemy>();
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
        enemy.SetAlert(true);/////////////////////////////
        if (!Awake) //only wakes when is asleep
        {
            animator.SetTrigger("Awake");
            Awake = true;
        }
    }
    public void Sleep() //triggers when player exits aggroRange
    {
        PlayerPresent = false;
        enemy.SetAlert(false);///////////////////////////////////////////////////////
        Awake = false;
        animator.SetTrigger("Sleep");
    }
}
