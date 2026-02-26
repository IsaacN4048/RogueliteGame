using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    [SerializeField]private Vector3 lastPlayerPosition;

    //public GameObject floatingText;
    //public GameObject floatingTextPos;
    //public string floatingTextNumber;

    [Header("EnemyStats")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    [Header("Behavior")]
    public bool IsAlert;
    public bool CanAttack;

    [Header("Attacks")]
    public bool FiresProjectiles;
    public GameObject projectile;
    public Transform firePoint;
    public float fireInterval;
    public float projectileSpeed;

    private Coroutine attackRoutine;

    
    //public float damageAmount;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerTransform = player.transform;
        projectileSpeed = projectile.GetComponent<RangedProjectile>().shotVelocity; //gets the shot velocity from the bullet, maybe change


        currentHealth = maxHealth;
    }

   
    public void Update()
    {   
        
        if(player != null) 
        {
            MoveToPlayer();
        }
        if(CanAttack) //call IsAlert = true in enemy scripts, when you want it to start attacking. To stop, StopAllCouroutines
        {
            StartCoroutine(AttackLoop());
            CanAttack = false;
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
        //while(IsAlert) 
               
        yield return new WaitForSeconds(fireInterval);
        FireProjectile();
        CanAttack = true;
    }

    private void FireProjectile()
    {
        Vector3 playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 1.1f, playerTransform.position.z);
        //Vector3 playerPos = playerTransform.position;
        //Vector3 playerVelocity = (playerPos - lastPlayerPosition) / Time.deltaTime;
        Vector3 playerVelocity = player.GetComponent<FirstPersonController>().Velocity;
        lastPlayerPosition = playerPos;
        Vector3 distanceToPlayer = playerPos - firePoint.position;
        float distance = distanceToPlayer.magnitude;
        float timeToHit = distance / projectileSpeed;
        timeToHit = Mathf.Min(timeToHit, 1.5f);
        Vector3 predictedPosition = playerPos + playerVelocity * timeToHit;

        Vector3 direction = (predictedPosition - firePoint.position).normalized;

        GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction));


        proj.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;
        //Rigidbody rb = proj.GetComponent<Rigidbody>();
        //rb.linearVelocity = direction * projectileSpeed;

        //RangedProjectile projScript = proj.GetComponent<RangedProjectile>();

        Debug.DrawRay(firePoint.position, direction * 30f, Color.red, 2f);
    }


   



    private void GetPlayerPositionPrediction()
    {
        
       
        
        
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
