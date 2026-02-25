using System;
using UnityEngine;
using UnityEngine.UI;

public class DetectTarget : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    [Header("Healthbars")]
    [SerializeField] private GameObject healthbarUI;
    [SerializeField] private Image healthFill;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float targetLingerTime = 0.15f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField]private Enemy currentEnemy;
    private float lastSeenTime;

    private void Update()
    {
        DetectEnemies();
        UpdateHealthbarUI();

    }

    private void DetectEnemies()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.SphereCast(ray, 0.25f, out RaycastHit hit, maxDistance, enemyLayer)) //if enemy is targeted in the current frame
        {
            Enemy enemyScript = hit.collider.GetComponent<Enemy>();

            if(enemyScript != null)
            {
                currentEnemy = enemyScript;
                lastSeenTime = Time.time;
            }
        }
        
        if(currentEnemy != null && Time.time > lastSeenTime + targetLingerTime) //removes enemyScript if no target for lingerTime
        {
            currentEnemy = null;
        }
    }

    private void UpdateHealthbarUI()
    {
        bool hasTarget = currentEnemy != null;

        healthbarUI.SetActive(hasTarget); //when there is no enemyScript, remove healthbar

        if(hasTarget) 
        {
            healthFill.fillAmount = currentEnemy.GetHealthPercent();
        }
    }
}
