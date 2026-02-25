using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private Transform playerTransform;

    //public GameObject floatingText;
    //public GameObject floatingTextPos;
    //public string floatingTextNumber;

    [Header("EnemyStats")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    [Header("Behavior")]
    public bool IsAlert;

    [Header("Attacks")]
    public bool FiresProjectiles;
    public GameObject projectile;
    public Transform firePoint;
    public float fireInterval;
    public float fireForce;

    private Coroutine attackRoutine;

    
    //public float damageAmount;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerTransform = player.transform;


        currentHealth = maxHealth;
    }

   
    public void Update()
    {   
        if(player != null) 
        {
            MoveToPlayer();
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth = currentHealth - amount;
        //floatingTextNumber = amount.ToString();

        /*if (currentHealth > 0 && floatingText != null )
        {
            ShowDamageNumbers();
        }
        */
       
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;//ChatGPT gave me this
    }


    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }




    //ATTACK LOGIC

     public void SetAlert(bool alert) ////////////CRASHES GAME, supposed to only trigger when entering attack radius
    {
        if(alert && !IsAlert)
        {
            IsAlert = true;
            attackRoutine = StartCoroutine(AttackLoop());
        }
        else if(!alert && IsAlert)
        {
            IsAlert = false;

            if(attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
            }
        }
    }
     

    public IEnumerator AttackLoop()
    {
        while(IsAlert) 

        FireProjectile();
               
        yield return new WaitForSeconds(fireInterval);
    }

    private void FireProjectile()
    {
        
        GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.identity);

        RangedProjectile projScript = proj.GetComponent<RangedProjectile>();

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;
        rb.linearVelocity = direction * fireForce;

        Debug.DrawRay(firePoint.position, direction * 10f, Color.red, 2f);
    }
















































    /*
    private void ShowDamageNumbers()
    {
        var obj = Instantiate(floatingText, floatingTextPos.transform.position, Quaternion.identity, transform);
        obj.GetComponent<TextMesh>().text = floatingTextNumber;
    }


    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
    */
}
