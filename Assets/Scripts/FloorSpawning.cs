using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawning : MonoBehaviour
{

    [SerializeField] GameObject[] floorPrefabGreen;
    [SerializeField] int numOfFloorsGreen; // num of different floors in specific prefab set
    [SerializeField] GameObject[] floorPrefabRed;
    [SerializeField] int numOfFloorsRed;
    private float floorTimer = 0f;
    // ----=----
    private GameObject[][] floorPrefabSet;
    [SerializeField] int numOfFloorSets; // num of different sets of floor prefabs
    private int prefabRandSet; // used for randomizing floor prefab set chosen
    private float setTimer = 0f;
    private int previousSet; // prevent same set from being used twice in a row

    void Start()
    {
        floorPrefabSet = new GameObject[][] { floorPrefabGreen, floorPrefabRed };

        prefabRandSet = 0; // always start with green set
        previousSet = 0;
    }

    void Update()
    {
        setTimer += Time.deltaTime;
        floorTimer += Time.deltaTime;

        if (setTimer >= 30f)
        {
            while (prefabRandSet == previousSet)
            {
                prefabRandSet = Random.Range(0, numOfFloorSets);
            }
            previousSet = prefabRandSet;
            setTimer = 0;
        }

        if (floorTimer >= 2f)
        {
            if (prefabRandSet == 0)
            {
                SpawnFloor(prefabRandSet, numOfFloorsGreen);
            }
            else if (prefabRandSet == 1)
            {
                SpawnFloor(prefabRandSet, numOfFloorsRed);
            }
            floorTimer = 0f;
        }
    }

    void SpawnFloor(int prefabRandSet, int numOfFloors)
    {
        int prefabRandFloor = Random.Range(0, numOfFloors);
        Instantiate(floorPrefabSet[prefabRandSet][prefabRandFloor], transform.position, Quaternion.identity);
    }
}
