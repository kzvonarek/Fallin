using UnityEngine;

public class CloudSpawnerBehavior : MonoBehaviour
{
    private float cloudTimer;
    [SerializeField] float cloudTimerReset;
    [SerializeField] GameObject cloudPreset;
    public bool isLeftSpawner;

    private int cloudSpawnNumber;

    void Start()
    {
        cloudTimer = 0f;
    }

    void Update()
    {
        // spawn cloud on timer
        cloudTimer += Time.deltaTime;
        if (cloudTimer >= cloudTimerReset)
        {
            cloudSpawnNumber = Random.Range(0, 3);

            if (isLeftSpawner && cloudSpawnNumber == 1) // spawn on left side of screen
            {
                GameObject newCloud = Instantiate(cloudPreset, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
                newCloud.transform.parent = this.transform;
            }
            else if (!isLeftSpawner && cloudSpawnNumber == 2) // spawn on right side of screen
            {
                GameObject newCloud = Instantiate(cloudPreset, new Vector2(transform.position.x - 1f, transform.position.y), Quaternion.identity);
                newCloud.transform.parent = this.transform;
            }
            cloudTimer = 0f;
        }
    }
}
