using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;

    [SerializeField] UnityEvent onTriggerExit;

    [SerializeField] string tagFilter;

    //public Collider trigger;
    //public Animator animator;
    public bool needsPlayer = false;
    public bool healsPlayer = false;
    private GameObject player;

    private void Start()
    {
        if (needsPlayer)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if(!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
        Debug.Log("Player enters");

        if(needsPlayer)
        {
            if (healsPlayer)
            {
                PlayerHealth playerHealth = player.gameObject.GetComponent<PlayerHealth>();
                playerHealth.Heal(10f);
            }
              

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
        Debug.Log("Player exits");
    }
}
