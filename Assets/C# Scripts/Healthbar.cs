using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public Image healthJuice;
    public Image healthJuiceBuffer;
    public TextMeshProUGUI healthNumbers;
    public Slider slider;
    //public Slider bufferSlider;

    private void Start()
    {
        //healthJuice.fillAmount = 1f;
        playerHealth = PlayerHealth.instance;
        slider = GetComponent<Slider>();
        //bufferSlider.maxValue = playerHealth.maxHealth;
        //bufferSlider.value = bufferSlider.maxValue;
    }
    private void Update()
    {
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.currentHealth;


        //healthJuice.fillAmount = Mathf.Lerp(healthJuice.fillAmount, playerHealth.currentHealth / playerHealth.maxHealth, Time.deltaTime * 16f);

        StartCoroutine(Buffer());


        healthNumbers.text = (playerHealth.currentHealth.ToString() + "/" + playerHealth.maxHealth.ToString());
    }

    public IEnumerator Buffer()
    {
        yield return new WaitForSeconds(3f);

        //healthJuiceBuffer.fillAmount = Mathf.Lerp(healthJuiceBuffer.fillAmount, healthJuice.fillAmount, Time.deltaTime * 4f);

        //bufferSlider.value = Mathf.Lerp(bufferSlider.value, slider.value, Time.deltaTime * 4f);

    }


}
