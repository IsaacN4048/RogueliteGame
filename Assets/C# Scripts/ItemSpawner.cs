using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSpawner : MonoBehaviour
{
    public GameObject ItemToSpawn;

    public InputAction spawnItem;
    public InputSystem_Actions playerControls;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }
    private void Update()
    {
        if (spawnItem.triggered)
        {
            SpawnItem();
            Debug.Log("WORKED!!");
        }
    }

    private void OnEnable()
    {
        spawnItem = playerControls.Player.Spawn;
        spawnItem.Enable();
    }

    private void OnDisable()
    {
        spawnItem.Disable();
    }


    public void SpawnItem()
    {
        Instantiate(ItemToSpawn, transform);
    }
}
