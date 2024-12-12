using UnityEngine;

public class BubbleLauncherBehavior : MonoBehaviour
{
    [SerializeField] GameObject bubblePreset;
    private float bubbleTimer;
    [SerializeField] float bubbleTimerReset;

    void Start()
    {
        bubbleTimer = 0f;
    }

    void Update()
    {
        bubbleTimer += Time.deltaTime;
        if (bubbleTimer >= bubbleTimerReset)
        {
            Instantiate(bubblePreset, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
            bubbleTimer = 0f;
        }
    }
}
