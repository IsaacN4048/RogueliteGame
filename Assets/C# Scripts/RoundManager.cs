using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    public List<GameObject> PlantSpawners;
    public GameObject currentPlantSpawner;


    public bool inRound = false;

    public void StartRound()
    {
        foreach(GameObject spawner in PlantSpawners) 
        {
            ItemSpawner spawnScript = spawner.GetComponent<ItemSpawner>();
            spawnScript.SpawnItem();
        }
        //obviously trigger all spawners
    }

    public void EndRound()
    {
        foreach (GameObject spawner in PlantSpawners)
        {
            ItemSpawner spawnScript = spawner.GetComponent<ItemSpawner>();
            spawnScript.RemoveSpawnedItem();
        }
        //set stats back to base stats
    }

}
