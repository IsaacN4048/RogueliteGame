using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager instance;

    public CardData[] AllCards; //PUT EVERY CARD DATA IN THE GAME HERE

    public List<CardData> AvailableCards; //EVERY CARD THAT IS CURRENTLY UNLOCKED

    public List<CardData> UnavailableCards; //POOL OF CARDS THAT HAVENT BEEN SELECTED

    public List<GameObject> CardGameObjects; //ACTUAL UI CARDS THAT ARE SHOWN ONSCREEN

    public GameObject LastSelected { get; set; }
    public int LastSelectedIndex { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        //If we move right
        if(InputManager.instance.NavigationInput.x > 0)
        {
            //Select next card
            HandleNextCardSelection(1);
        }

        //If we move left
        if (InputManager.instance.NavigationInput.x < 0)
        {
            //Select previous card
            HandleNextCardSelection(-1);
        }

    }

    private void OnEnable() //CALLED WHEN THE CARD MENU IS OPENED
    {
        ChoseCardData();
        StartCoroutine(SetSelectedAfterOneFrame());
    }

    private IEnumerator SetSelectedAfterOneFrame()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(CardGameObjects[0]);
    }

    private void HandleNextCardSelection(int addition)
    {
        if(EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
        {
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, CardGameObjects.Count -1);
            EventSystem.current.SetSelectedGameObject(CardGameObjects[newIndex]);
        }
    }

    public void ChoseCardData() //loops through all visible cards, applying a random data script to each
    {
        for(int i = 0; i < CardGameObjects.Count; i++)
        {
            CardManager cardManager = CardGameObjects[i].gameObject.GetComponent<CardManager>(); //get the manager script on a card

            //pick a random cardData from AvailableCards
            int randomIndex = Random.Range(0, AvailableCards.Count);
            CardData chosenCard = AvailableCards[randomIndex];

            //assign data to the cardManager
            cardManager.cardData = chosenCard;
            cardManager.UpdateCard();

            //move cardData to Unavailable, so you wont get repeats
            UnavailableCards.Add(chosenCard);
            AvailableCards.RemoveAt(randomIndex);

            continue;
        }
    }

    public void RefreshCardPool()
    {
        AvailableCards.AddRange(UnavailableCards);
        UnavailableCards.Clear();
    }

}
