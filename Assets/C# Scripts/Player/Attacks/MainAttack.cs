using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainAttack : MonoBehaviour
{
    public float shotVelocity;
    public float lifetime;
    public float finalDamage;

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
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(finalDamage);
        }
    }
}
