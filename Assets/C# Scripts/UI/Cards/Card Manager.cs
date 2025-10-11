using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public CardData cardData; //this determines what appears on the cards

    public Image icon;
    public Image rarityIcon;
    public TextMeshProUGUI titleSlot;

    private Button button;
    private CardSelectionManager selectionManager;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ChoseCard);
        selectionManager = gameObject.transform.parent.GetComponent<CardSelectionManager>();
    }
    public void UpdateCard()
    {
        if (cardData != null)
        {
            icon.sprite = cardData.icon;
            titleSlot.text = cardData.title;
            titleSlot.outlineColor = cardData.rarity;
            rarityIcon.color = cardData.rarity;
        }
    }

    public void ChoseCard()
    {
        ApplyUpgrade();

        //lock the mouse back to the center of the screen
        GameObject playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerCam camScript = playerCam.GetComponent<PlayerCam>();
        camScript.CursorFollowsPlayer();

        //send all unavailable cards back into the pool
        selectionManager.RefreshCardPool();

        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void ApplyUpgrade()
    {
       if(cardData.statUpgradeType == CardData.UpgradeType.None)
        {
            return;
        }
        else
        {
            if (cardData.statUpgradeType == CardData.UpgradeType.MoveSpeed)
            {
                PlayerStats playerStats = PlayerStats.instance;
                playerStats.moveSpeed.AddPercentValue(cardData.upgradeValue);
            }
            if (cardData.statUpgradeType == CardData.UpgradeType.MaxHealth)
            {
                PlayerStats playerStats = PlayerStats.instance;
                playerStats.maxHealth.AddPercentValue(cardData.upgradeValue);
            }
            if (cardData.statUpgradeType == CardData.UpgradeType.CritChance)
            {
                PlayerStats playerStats = PlayerStats.instance;
                playerStats.critChance.AddPercentValue(cardData.upgradeValue);
            }
            if (cardData.statUpgradeType == CardData.UpgradeType.CritDamage)
            {
                PlayerStats playerStats = PlayerStats.instance;
                playerStats.critDamage.AddPercentValue(cardData.upgradeValue);
            }
        }
    }


}
