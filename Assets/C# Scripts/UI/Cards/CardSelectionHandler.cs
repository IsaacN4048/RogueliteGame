using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.1f;

    private Vector3 startPos;
    private Vector3 startScale;

    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    private IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;

        float elapsedTime = 0f;

        while(elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            if(startingAnimation)
            {
                endPosition = startPos + new Vector3(0f, verticalMoveAmount, 0f);
                endScale = startScale * scaleAmount;
            }
            else
            {
                endPosition = startPos;
                endScale = startScale;
            }

            //calculate the lerped amounts
            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, (elapsedTime / moveTime));
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            //apply the changes to position and scale
            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //select the card
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //deselect card
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));

        //set this card as "Last Selected"
        CardSelectionManager.instance.LastSelected = gameObject;

        //Find index of this card
        for (int i = 0; i < CardSelectionManager.instance.CardGameObjects.Count; i++)
        {
            if (CardSelectionManager.instance.CardGameObjects[i] == gameObject)
            {
                CardSelectionManager.instance.LastSelectedIndex = i;
                return;
            }
        }

    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine (MoveCard(false));
    }
}
