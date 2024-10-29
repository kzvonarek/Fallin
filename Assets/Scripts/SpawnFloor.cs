using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    [SerializeField] List<GameObject> floorPrefabs;
    [SerializeField] int numOfFloors;
    private int prefabRandNum;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            prefabRandNum = Random.Range(0, numOfFloors);
            Instantiate(floorPrefabs[prefabRandNum], transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
