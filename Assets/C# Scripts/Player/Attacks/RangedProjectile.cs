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
       
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            hitCounter++;
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(finalDamage);
        }
        if (hitCounter >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}
