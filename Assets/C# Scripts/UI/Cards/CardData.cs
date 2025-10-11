using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public Sprite icon;
    public string title;
    public Color rarity;

    public UpgradeType statUpgradeType;
    public float upgradeValue;

    public enum UpgradeType
    {
        None,
        MoveSpeed,
        MaxHealth,
        CritChance,
        CritDamage
    }
    
}
