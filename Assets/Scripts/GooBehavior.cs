using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    // back and forth mouse var(s)
    private Vector2 lastMousePosition;
    private float startTime;
    private int directionChanges;
    private bool isDragging;
    [SerializeField] float checkDuration;

    // player behavior
    public bool playerStuck = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerStuck)
        {
            player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y);

            // check for mouse clicked down
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                startTime = Time.time;
                directionChanges = 0;
            }

            // check if mouse is held down
            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 delta = currentMousePosition - lastMousePosition;

                // detect direction change
                if (delta.x > 0 && lastMousePosition.x - currentMousePosition.x > 0 ||
                    delta.x < 0 && lastMousePosition.x - currentMousePosition.x < 0)
                {
                    directionChanges++;
                }

                lastMousePosition = currentMousePosition;

                // check if checkDuration seconds have passed
                if (Time.time - startTime >= checkDuration)
                {
                    if (directionChanges >= 4)
                    {
                        playerStuck = false;
                    }

                    // reset tracking
                    isDragging = false;
                }
            }

            // check for mouse up to reset state
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStuck = true;
        }
    }
}
