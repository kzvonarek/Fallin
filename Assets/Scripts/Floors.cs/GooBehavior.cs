using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    private Vector2 lastInputPosition;
    private int directionChanges;
    private bool isDragging;
    private float swipeStartTime;
    private float swipeDuration = 1f;

    private GameObject player;
    private bool thisGooFloor = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.GetComponent<PlayerMovement>().playerStuck && thisGooFloor)
        {
            player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - 0.5f);

            // check for both touch and mouse input
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                Vector2 currentInputPosition;

                if (Input.touchCount > 0) // use touch input
                {
                    Touch touch = Input.GetTouch(0);
                    currentInputPosition = touch.position;

                    if (!isDragging || touch.phase == TouchPhase.Began)
                    {
                        startSwipe(currentInputPosition);
                    }
                    else if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
                    {
                        processSwipe(currentInputPosition);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        resetSwipe();
                    }
                }
                else // use mouse input
                {
                    currentInputPosition = Input.mousePosition;

                    if (!isDragging)
                    {
                        startSwipe(currentInputPosition);
                    }
                    else
                    {
                        processSwipe(currentInputPosition);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        resetSwipe();
                    }
                }
            }
        }
    }

    private void startSwipe(Vector2 startPosition)
    {
        isDragging = true;
        lastInputPosition = startPosition;
        swipeStartTime = Time.time;
        directionChanges = 0;
    }

    private void processSwipe(Vector2 currentPosition)
    {
        Vector2 delta = currentPosition - lastInputPosition;

        // check for direction change
        if (delta.x * (lastInputPosition.x - currentPosition.x) < 0)
        {
            directionChanges++;
            lastInputPosition = currentPosition;
        }

        // check if swipe duration has elapsed
        if (Time.time - swipeStartTime >= swipeDuration)
        {
            if (directionChanges >= 4) // back and forth 2 times
            {
                player.GetComponent<PlayerMovement>().playerStuck = false;
                thisGooFloor = false;
            }

            // reset tracking
            resetSwipe();
        }
    }

    private void resetSwipe()
    {
        isDragging = false;
        directionChanges = 0;
        swipeStartTime = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisGooFloor = true;
        }
    }
}