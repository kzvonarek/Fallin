using UnityEngine;

public class SpawnFloor : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            Instantiate(floorPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
