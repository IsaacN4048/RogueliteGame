using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance; //SINGLETON

    [SerializeField]
    public List<Stat> listOfStats;

    public Stat moveSpeed;
    public Stat maxHealth;
    public Stat healthRegen;
    public Stat defense;
    public Stat dodgeChance;
    public Stat physicalDamage;
    public Stat elementalDamage;
    public Stat critChance;
    public Stat critDamage;
    public Stat cooldownSpeed;
    public Stat fortune;

    //NON VISIBLE STATSs


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SetStartingValues();
        AddStatsToList();
    }

    public void Start()
    {
        RemoveALLMods();
    }
    public void RemoveALLMods()
    {
        for (int i = 0; i < listOfStats.Count; i++)
        {
            listOfStats[i].RemoveAllMods();
        }
    }

    public void SetStartingValues()
    {
        moveSpeed.startingValue = 4f;
        moveSpeed.CalculateValue();

        maxHealth.startingValue = 100f;
        maxHealth.CalculateValue();

        healthRegen.startingValue = 0f;
        healthRegen.CalculateValue();

        defense.startingValue = 010f; //percentage
        defense.CalculateValue();

        dodgeChance.startingValue = 0f; //percentage
        dodgeChance.CalculateValue();

        physicalDamage.startingValue = 100f; //percentage
        physicalDamage.CalculateValue();

        elementalDamage.startingValue = 0f; //percentage
        elementalDamage.CalculateValue();

        critChance.startingValue = 5f;  //percentage
        critChance.CalculateValue();

        critDamage.startingValue = 150f;  //percentage
        critDamage.CalculateValue();

        cooldownSpeed.startingValue = 0f;  //percentage
        cooldownSpeed.CalculateValue();

        fortune.startingValue = 0f;
        fortune.CalculateValue();

        //NON VISIBLE STATS





    }

    public void AddStatsToList()
    {
        listOfStats.Add(moveSpeed);
        listOfStats.Add(maxHealth);
        listOfStats.Add(healthRegen);
        listOfStats.Add(defense);
        listOfStats.Add(dodgeChance);
        listOfStats.Add(physicalDamage);
        listOfStats.Add(elementalDamage);
        listOfStats.Add(critChance);
        listOfStats.Add(critDamage);
        listOfStats.Add(cooldownSpeed);
        listOfStats.Add(fortune);
    }


    public void TotalPercentMod100Percent()
    {
        moveSpeed.AddTotalPercentMod(1);

    }

}
