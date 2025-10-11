
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public Stat currentStat;
    public TextMeshProUGUI nameSlot;
    public TextMeshProUGUI valueSlot;

    public void Update()
    {
        nameSlot.text = currentStat.statName;
        valueSlot.text = currentStat.totalValue.ToString();
    }

}


