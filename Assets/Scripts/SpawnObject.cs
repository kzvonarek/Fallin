using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] List<GameObject> objectPrefabs;
    [SerializeField] int numOfObjects;
    private int prefabRandNum;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            prefabRandNum = Random.Range(0, numOfObjects);
            Instantiate(objectPrefabs[prefabRandNum], transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
