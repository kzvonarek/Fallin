using UnityEngine;

public class BubbleLauncherBehavior : MonoBehaviour
{
    private float bubbleTimer;
    [SerializeField] float bubbleTimerReset;
    [SerializeField] GameObject bubblePreset;
    public bool isLeftLauncher;

    void Start()
    {
        bubbleTimer = 0f;

        // rotate launcher correctly
        transform.rotation.Set(0f, 0f, 270f, 0f);

        // move launcher to left most of screen
        if (isLeftLauncher)
        {
            transform.position = new Vector2(-4.244f, transform.position.y);

            // rotate launcher correctly
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        // move launcher to right msot of screen
        else
        {
            transform.position = new Vector2(4.244f, transform.position.y);

            // rotate launcher correctly
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    void Update()
    {
        // spawn bubble on timer
        bubbleTimer += Time.deltaTime;
        if (bubbleTimer >= bubbleTimerReset)
        {
            if (isLeftLauncher) // spawn on left side of screen
            {
                GameObject newBubble = Instantiate(bubblePreset, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
                newBubble.transform.parent = this.transform;
            }
            else // spawn on right side of screen
            {
                GameObject newBubble = Instantiate(bubblePreset, new Vector2(transform.position.x - 1f, transform.position.y), Quaternion.identity);
                newBubble.transform.parent = this.transform;
            }
            bubbleTimer = 0f;
        }
    }
}
