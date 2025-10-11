using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthJuice;
    public Image healthJuiceBuffer;
    public TextMeshProUGUI healthNumbers;

    private void Start()
    {
        healthJuice.fillAmount = 1f;
    }
    private void Update()
    {
        //healthJuice.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;

        //healthJuiceBuffer.fillAmount = Mathf.Lerp(healthJuice.fillAmount, healthJuice.fillAmount, Time.deltaTime * 1f);


        healthJuice.fillAmount = Mathf.Lerp(healthJuice.fillAmount, playerHealth.currentHealth / playerHealth.maxHealth, Time.deltaTime * 16f);

        StartCoroutine(Buffer());


        healthNumbers.text = (playerHealth.currentHealth.ToString() + "/" + playerHealth.maxHealth.ToString());
    }

    public IEnumerator Buffer()
    {
        yield return new WaitForSeconds(3f);

        healthJuiceBuffer.fillAmount = Mathf.Lerp(healthJuiceBuffer.fillAmount, healthJuice.fillAmount, Time.deltaTime * 4f);

    }
}
