using Unity.VisualScripting;
using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    // back and forth mouse var(s)
    private Vector2 lastMousePosition;
    private float startTime;
    private int directionChanges;
    private bool isDragging;
    private float checkDuration = 1f;

    // player behavior
    public bool playerStuck = false;
    private GameObject player;
    [SerializeField] float gooPosDeduct;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // checking if player swiped back and forth to escape Goo floor
        if (playerStuck)
        {
            player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - gooPosDeduct);

            // check for mouse clicked down
            if (Input.GetMouseButton(0))
            {
                if (!isDragging)
                {
                    isDragging = true;
                    lastMousePosition = Input.mousePosition;
                    startTime = Time.time;
                    directionChanges = 0;
                }

                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 delta = currentMousePosition - lastMousePosition;

                // detect direction change
                if (Mathf.Sign(delta.x) != Mathf.Sign(lastMousePosition.x - currentMousePosition.x))
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
                    directionChanges = 0;
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
        if (other.gameObject.CompareTag("Player"))
        {
            playerStuck = true;
        }
    }
}
