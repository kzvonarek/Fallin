using UnityEngine;

public class BubbleLauncherBehavior : MonoBehaviour
{
    [SerializeField] GameObject bubblePreset;
    private float bubbleTimer;
    [SerializeField] float bubbleTimerReset;

    void Start()
    {
        bubbleTimer = 0f;
        // bubbleTimerReset = 2f;
    }

    void Update()
    {
        bubbleTimer += Time.deltaTime;
        if (bubbleTimer >= bubbleTimerReset)
        {
            Instantiate(bubblePreset, transform.position, Quaternion.identity);
            bubbleTimer = 0f;
        }
    }
}
