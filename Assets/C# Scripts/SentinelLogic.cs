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
        if(enemy.currentHealth <= 0) //play animation when it dies, and spawn reward in
        {
            Destroy(gameObject);
        }
        
    }

    public void Waken()  //triggers when player enters trigger
    {
        PlayerPresent = true;

        if (!Awake) //only animates the awake when is asleep
        {
            animator.SetTrigger("Awake");
            Awake = true;
        }

        if (enemy.IsAlert == false) //starts the Attack loop only once, when the player enters the trigger
        {
            enemy.IsAlert = true;
            enemy.CanAttack = true;
        } 

        //spawn in enemies until the sentinel is destroyed
        
    }
    public void Sleep() //triggers when player exits aggroRange
    {
        PlayerPresent = false;
        enemy.StopAllCoroutines();
        enemy.IsAlert = false;
        Awake = false;
        animator.SetTrigger("Sleep");
    }
}
