using UnityEngine;

public class FloorSpawning : MonoBehaviour
{

    [SerializeField] GameObject[] floorPrefabOrange;
    [SerializeField] int numOfFloorsOrange; // num of different floors in specific prefab set
    [SerializeField] GameObject[] floorPrefabRed;
    [SerializeField] int numOfFloorsRed;
    private float floorTimer = 0f;
    private float floorTimerReset;
    // ----=----
    private GameObject[][] floorPrefabSet;
    [SerializeField] int numOfFloorSets; // num of different sets of floor prefabs
    private int prefabRandSet; // used for randomizing floor prefab set chosen
    private float setTimer = 0f;
    private int previousSet; // prevent same set from being used twice in a row
    // ----=----
    // [SerializeField] GameObject[] windPrefabSet;
    // private float windTimer = 0f;
    // [SerializeField] float windTimerReset;
    // ----=----
    [SerializeField] GameObject[] powerupsPrefabSet;
    [SerializeField] int numOfPowerups;
    private float powerupTimer = 0f;
    [SerializeField] float powerupTimerReset;
    // ----=----
    [SerializeField] GameObject currencyObj;
    private float currencyTimer = 0f;
    [SerializeField] float currencyTimerReset;
    // ----=----

    private GameObject[] backgrounds; // list of backgrounds
    private int numOfBG; // number of backgrounds in list
    private GameObject skyBG; // to turn on and off depending on theme
    private GameObject caveBG;
    // ----=----
    // access to GameManager.cs for currSlowedTime variable
    private GameObject gameManagerObj;
    private GameManager gMscript;

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

        // allow for access to vertObjSpeed and currSlowedTime variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    void Update()
    {
        setTimer += Time.deltaTime;
        floorTimer += Time.deltaTime; // increase by milliseconds
        powerupTimer += Time.deltaTime;
        currencyTimer += Time.deltaTime;
        // windTimer += Time.deltaTime;

        // adjust floorTimerReset based on vertObjSpeed milestones
        if (gMscript.vertObjSpeed < 10f)
        {
            floorTimerReset = 1.5f;
        }
        else if (gMscript.vertObjSpeed >= 10f && gMscript.vertObjSpeed < 12.5f)
        {
            floorTimerReset = 1.3f;
        }
        else if (gMscript.vertObjSpeed >= 12.5f && gMscript.vertObjSpeed < 15f)
        {
            floorTimerReset = 1.1f;
        }
        else if (gMscript.vertObjSpeed >= 15f)
        {
            floorTimerReset = 0.9f;
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

        if (powerupTimer >= powerupTimerReset)
        {
            SpawnPowerup();
            powerupTimer = 0f;
        }

        if (currencyTimer >= currencyTimerReset)
        {
            SpawnCurrency();
            currencyTimer = 0f;
        }

        // if (windTimer >= windTimerReset)
        // {
        //     SpawnWind();
        //     windTimer = 0f;
        // }
    }

    void SpawnFloor(int prefabRandSet, int numOfFloors)
    {
        int prefabRandFloor = Random.Range(0, numOfFloors);
        Instantiate(floorPrefabSet[prefabRandSet][prefabRandFloor], transform.position, Quaternion.identity);
    }

    // void SpawnWind()
    // {
    //     int prefabRandWind = Random.Range(0, 2);
    //     Instantiate(windPrefabSet[prefabRandWind], transform.position, Quaternion.identity);
    // }

    void SpawnPowerup()
    {
        int prefabRandPowerup = Random.Range(0, numOfPowerups);
        float horizSpawnVariance = Random.Range(-3.79f, 3.79f);
        Instantiate(powerupsPrefabSet[prefabRandPowerup], new Vector2(transform.position.x + horizSpawnVariance, transform.position.y), Quaternion.identity);
    }

    void SpawnCurrency()
    {
        float horizSpawnVariance = Random.Range(-3.79f, 3.79f);
        Instantiate(currencyObj, new Vector2(transform.position.x + horizSpawnVariance, transform.position.y), Quaternion.identity);
    }
}
