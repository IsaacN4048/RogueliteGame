using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Stat", menuName = "CustomStat")]
public class Stat : ScriptableObject //THIS SCRIPT IS AMAZING AND WORKS PERFECTLY
{
    public string statName;
    public float baseValue;
    public float startingValue;
    public float flatIncrease = 0;
    public float totalValue;


    public List<int> baseModifiers;
    public List<float> percModifiers;
    public List<float> flatIncreases;

    //DisplayVariables
    public float percentBaseIsIncreasedBy;


    public void Start()
    {
        CalculateValue();
    }

    public void Update()
    {
        //CalculateValue();

        //Debug.Log("UPDATE LOOP calc");
    }
    public float CalculateValue() //call when modifiers are changed
    {
        //get base first  
        int baseMods = baseModifiers.Sum(); //should add all the modifier ints together
        baseValue = (startingValue + baseMods); //and then add the modifiers to the starting value

        //then add percent to mods to get total
        float percMods = percModifiers.Sum();
        percentBaseIsIncreasedBy = percModifiers.Sum();

        //and check if there are any flat increases to be added
        flatIncrease = flatIncreases.Sum();


        //totalValue = (baseValue * (1 + percMods)) + flatIncrease; //if base = 10 and percModifiers 0.5, sets current as (10 * (1.5) )* 1 = 15;

        float almostTotalValue = (baseValue * (1 + percMods)) + flatIncrease; //if base = 10 and percModifiers 0.5, sets current as (10 * (1.5) )* 1 = 15;

        almostTotalValue = Mathf.Round(almostTotalValue * 10.0f) * 0.1f;

        totalValue = almostTotalValue;


        //Debug.Log("CalculateValue");

        return totalValue;
    }


    public void AddBaseValue(int value) //simply added to the 
    {
        baseModifiers.Add(value);
        CalculateValue();
    }

    public void AddPercentValue(float value) //make this value 0.20 if %20 is desired
    {
        percModifiers.Add(value);
        CalculateValue();
    }


    public void RemoveAllPercentMods() //clears all percentages
    {
        percModifiers.Clear();
        CalculateValue();
    }

    public void RemoveAllBaseMods() //clears all numbers added to default value
    {
        baseModifiers.Clear();
        CalculateValue();
    }

    public void RemoveAllMods() //sets current value to startingValue
    {
        baseModifiers.Clear();
        percModifiers.Clear();
        flatIncreases.Clear();
        CalculateValue();
    }

    public void AddTotalPercentMod(float percent) //sets total percent, which is multiplied after base and percents and doesnt change those values
    {
        float newFlatIncrease = (totalValue * percent);
        flatIncreases.Add(newFlatIncrease);


        CalculateValue();
    }



}

