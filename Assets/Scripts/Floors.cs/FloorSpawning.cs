using UnityEngine;

public class FloorSpawning : MonoBehaviour
{

    [SerializeField] GameObject[] floorPrefabOrange;
    [SerializeField] int numOfFloorsOrange; // num of different floors in specific prefab set
    [SerializeField] GameObject[] floorPrefabRed;
    [SerializeField] int numOfFloorsRed;
    private float floorTimer = 0f;
    [SerializeField] float floorTimerReset = 0f;
    // ----=----
    private GameObject[][] floorPrefabSet;
    [SerializeField] int numOfFloorSets; // num of different sets of floor prefabs
    private int prefabRandSet; // used for randomizing floor prefab set chosen
    private float setTimer = 0f;
    private int previousSet; // prevent same set from being used twice in a row
    // ----=----
    [SerializeField] GameObject[] windPrefabSet;
    private float windTimer = 0f;
    // ----=----
    private GameObject[] backgrounds; // list of backgrounds
    private int numOfBG; // number of backgrounds in list
    private GameObject skyBG; // to turn on and off depending on theme
    private GameObject caveBG;

    void Start()
    {
        floorPrefabSet = new GameObject[][] { floorPrefabOrange, floorPrefabRed };

        prefabRandSet = 0; // always start with orange set
        previousSet = 0;

        skyBG = GameObject.FindGameObjectWithTag("Sky BG");
        caveBG = GameObject.FindGameObjectWithTag("Cave BG");
        backgrounds = new GameObject[] { skyBG, caveBG };
        numOfBG = 2;

        // deactivate any background other than initial background
        for (int i = 1; i < numOfBG; i++)
        {
            backgrounds[i].SetActive(false);
        }
    }

    void Update()
    {
        setTimer += Time.deltaTime;
        floorTimer += Time.deltaTime;
        windTimer += Time.deltaTime;

        if (setTimer >= 30f)
        {
            while (prefabRandSet == previousSet)
            {
                prefabRandSet = Random.Range(0, numOfFloorSets);
            }

            backgrounds[previousSet].SetActive(false); // deactivate prev. bg
            backgrounds[prefabRandSet].SetActive(true); // activate curr. bg

            previousSet = prefabRandSet;
            setTimer = 0;
        }

        if (floorTimer >= floorTimerReset)
        {
            if (prefabRandSet == 0)
            {
                SpawnFloor(prefabRandSet, numOfFloorsOrange);
            }
            else if (prefabRandSet == 1)
            {
                SpawnFloor(prefabRandSet, numOfFloorsRed);
            }
            floorTimer = 0f;
        }

        if (windTimer >= 8.45f)
        {
            SpawnWind();
            windTimer = 0f;
        }
    }

    void SpawnFloor(int prefabRandSet, int numOfFloors)
    {
        int prefabRandFloor = Random.Range(0, numOfFloors);
        Instantiate(floorPrefabSet[prefabRandSet][prefabRandFloor], transform.position, Quaternion.identity);
    }

    void SpawnWind()
    {
        int prefabRandWind = Random.Range(0, 2);
        Instantiate(windPrefabSet[prefabRandWind], transform.position, Quaternion.identity);
    }
}
