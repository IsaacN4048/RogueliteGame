using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance; //SINGLETON

    public float maxHealth;
    public float currentHealth;
    public float minHealth = 0f;

    public Stat healthStat;

    //SCRIPTS
    public Healthbar healthbarScript;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

        private void Start()
    {
        maxHealth = healthStat.totalValue;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        maxHealth = healthStat.totalValue;

        if (currentHealth <= minHealth) //when you have 0 HP
        {
            //gameObject.SetActive(false);
            Debug.Log("Player is dead");
        }
        if (currentHealth > maxHealth) //when you overheal
        {
            currentHealth = maxHealth;
            Debug.Log("Player is overhealed");
        }
    }
    public void TakeDamage(float damageIncoming)
    {
        currentHealth -= damageIncoming;
    }
    public void Heal(float healing)
    {
        currentHealth += healing;
    }
}
