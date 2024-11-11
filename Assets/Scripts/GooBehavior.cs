using Unity.VisualScripting;
using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    // swipe interactivity
    private Vector2 lastTouchPosition;
    private int directionChanges;
    private bool isDragging;
    private float swipeStartTime;
    private float swipeDuration = 1f;

    // player interactivity
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
            player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - 0.75f);

            if (Input.GetMouseButton(0) || Input.touchCount > 0) // check for mouse or touch input
            {
                Vector2 currentInputPosition;

                if (Input.touchCount > 0) // use touch input
                {
                    Touch touch = Input.GetTouch(0);
                    currentInputPosition = touch.position;

                    if (touch.phase == TouchPhase.Began && !isDragging)
                    {
                        // initialize dragging for touch
                        StartSwipe(currentInputPosition);
                    }
                    else if (touch.phase == TouchPhase.Moved && isDragging)
                    {
                        ProcessSwipe(currentInputPosition);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        ResetSwipe();
                    }
                }
                else // use mouse input
                {
                    currentInputPosition = Input.mousePosition;

                    if (!isDragging)
                    {
                        StartSwipe(currentInputPosition);
                    }
                    else
                    {
                        ProcessSwipe(currentInputPosition);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        ResetSwipe();
                    }
                }
            }
        }
    }

    private void StartSwipe(Vector2 startPosition)
    {
        isDragging = true;
        lastTouchPosition = startPosition;
        swipeStartTime = Time.time;
        directionChanges = 0;
    }

    private void ProcessSwipe(Vector2 currentPosition)
    {
        Vector2 delta = currentPosition - lastTouchPosition;

        // check for direction change
        if (delta.x * (lastTouchPosition.x - currentPosition.x) < 0)
        {
            directionChanges++;
            lastTouchPosition = currentPosition;
        }

        // check if swipe duration elapsed
        if (Time.time - swipeStartTime >= swipeDuration)
        {
            if (directionChanges >= 4) // swipe back and forth 2 times
            {
                player.GetComponent<PlayerMovement>().playerStuck = false;
                thisGooFloor = false;
            }

            // reset tracking
            ResetSwipe();
        }
    }

    private void ResetSwipe()
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