using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RangedProjectile : MonoBehaviour //as this is ranged, make your secondary a melee
{
    public float shotVelocity;
    public float lifetime;
    public float finalDamage;

    [Header("Cooldown")]
    public float cooldown;

    [Header("Lifetime")]
    public int hitCounter = 0;
    public int maxHits = 1; //PlayerStats.instance.PiercingStat.totalValue;

    [Header("Damage Numbers")]
    [SerializeField] private GameObject floatingText;

    private void Start()
    {
    Rigidbody rb = GetComponent<Rigidbody>();

        if(rb != null )
        {
            rb.AddForce(gameObject.transform.forward * shotVelocity, ForceMode.Impulse);
        }
        Destroy(gameObject, lifetime);
    }
    

    public void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag) //checks the tag of the collision
        {
            case "Enemy":

                hitCounter++;
                if (hitCounter >= maxHits) //this is piercing essentially
                {
                    Destroy(gameObject);
                }

                Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
                enemyScript.TakeDamage(finalDamage);

                if (GameManager.instance.damageNumbers) //lets you toggle off for performance
                {
                    ShowDamageNumbers(finalDamage.ToString(), collision.GetContact(0).point);
                }

                break;

        }
    }

    private void ShowDamageNumbers(string damageText, Vector3 textPos)
    {
        if(floatingText != null) //allows you to turn off damage numbers with a simple bool
        {
            GameObject prefab = Instantiate(floatingText, textPos, Quaternion.identity);
            prefab.GetComponent<TextMesh>().text = damageText;
        }
       
    }



}
