using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeStat : MonoBehaviour
{
    public StatDisplay statDisplayer;
    public Stat displayerStat;

    private void Awake()
    {
        statDisplayer = gameObject.transform.parent.GetComponent<StatDisplay>();
        displayerStat = statDisplayer.currentStat;
    }

    public void AddBaseValue()
    {
       displayerStat.AddBaseValue(1); 
        
    }
}
