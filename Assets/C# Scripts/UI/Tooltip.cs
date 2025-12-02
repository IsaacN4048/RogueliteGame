using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRect;
    private void Awake()
    {
        backgroundRect = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<TextMeshProUGUI>();

        ShowTooltip("Helloooooooooooooooooooooooooooooooooooooooooooooo World, this is some randommmmmmmmmmmmmmmmmmmmmmmmmmmmmm text!");
    }


    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize , 
            tooltipText.preferredHeight + textPaddingSize );
        backgroundRect.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
