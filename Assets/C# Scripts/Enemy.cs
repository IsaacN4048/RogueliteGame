using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private Transform playerTransform;

    [Header("EnemyStats")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

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
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }


    public void TakeDamage(float amount)
    {
        currentHealth = currentHealth - amount;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
















    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
    */
}
