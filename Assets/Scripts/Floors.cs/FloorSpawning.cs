using UnityEngine;

public class FloorSpawning : MonoBehaviour
{

    [SerializeField] GameObject[] floorPrefab;
    [SerializeField] int numOfFloors;
    private float floorTimer = 0f;
    private float floorTimerReset;
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
    // access to GameManager.cs for currSlowedTime variable
    private GameObject gameManagerObj;
    private GameManager gMscript;

    void Start()
    {
        // allow for access to vertObjSpeed and currSlowedTime variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    void Update()
    {
        floorTimer += Time.deltaTime;
        powerupTimer += Time.deltaTime;
        currencyTimer += Time.deltaTime;

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

        // spawn floor
        if (floorTimer >= floorTimerReset)
        {
            SpawnFloor(numOfFloors);
            floorTimer = 0f;
        }

        // spawn powerup
        if (powerupTimer >= powerupTimerReset)
        {
            SpawnPowerup();
            powerupTimer = 0f;
        }

        // spawn currency
        if (currencyTimer >= currencyTimerReset)
        {
            SpawnCurrency();
            currencyTimer = 0f;
        }
    }

    void SpawnFloor(int numOfFloors)
    {
        int prefabRandFloor = Random.Range(0, numOfFloors);
        Instantiate(floorPrefab[prefabRandFloor], transform.position, Quaternion.identity);
    }

    void SpawnPowerup()
    {
        int prefabRandPowerup = Random.Range(0, numOfPowerups);
        float horizSpawnVariance = Random.Range(-3.79f, 3.79f);
        Instantiate(powerupsPrefabSet[prefabRandPowerup], new Vector2(transform.position.x + horizSpawnVariance, transform.position.y + 3), Quaternion.identity);
    }

    void SpawnCurrency()
    {
        float horizSpawnVariance = Random.Range(-3.79f, 3.79f);
        Instantiate(currencyObj, new Vector2(transform.position.x + horizSpawnVariance, transform.position.y + 3), Quaternion.identity);
    }
}
