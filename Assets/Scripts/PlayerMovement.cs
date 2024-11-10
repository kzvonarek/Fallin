using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float speed; // speed to follow mouse/finger
    [SerializeField] float maxSpeed; // max speed to follow mouse/finger
    [SerializeField] float drag; // friction on mouse/finger release
    private float horizVelocity;
    private bool isDragging;
    private float holdTime = 0f;
    [SerializeField] float holdThreshold;

    // functionality with PortalBehavior.cs for floating upwards
    private Rigidbody2D rb;
    private float playerOrigYPos;
    [SerializeField] float playerFloatSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // for floating behavior
        playerOrigYPos = rb.position.y;
    }

    void Update()
    {
        // PLAYER DRAGGING (back and forth)
        isDragging = false;
        Vector2 inputPosition = Vector2.zero;

        // check if left mouse button is being held
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            inputPosition = Input.mousePosition;
        }

        // check if there is at least one finger holding screen
        else if (Input.touchCount > 0)
        {
            isDragging = true;
            inputPosition = Input.GetTouch(0).position;
        }

        if (isDragging)
        {
            holdTime += Time.deltaTime;

            // make sure its a hold and not click/tap
            if (holdTime >= holdThreshold)
            {
                // get mouse/finger position
                Vector2 targetPosition = mainCamera.ScreenToWorldPoint(inputPosition);

                // calculate velocity to follow the mouse/finger position
                float targetVelocity = (targetPosition.x - transform.position.x) * speed;

                // apply velocity to the player
                horizVelocity = Mathf.Clamp(targetVelocity, -maxSpeed, maxSpeed);
            }
        }

        else
        {
            holdTime = 0f;
            horizVelocity *= drag;
        }

        // PLAYER FLOATING UP
        // apply upward movement if player is below original Y position
        if (rb.position.y < playerOrigYPos)
        {
            // set a velocity to float the player upwards gradually
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerFloatSpeed);
        }
        else
        {
            // stop movement when player reaches original Y position
            rb.linearVelocity = Vector2.zero;
        }

        // PLAYER FLOATING DOWN
        // apply downward movement if player is above original Y position
        if (rb.position.y > playerOrigYPos)
        {
            // set a velocity to float the player upwards gradually
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -playerFloatSpeed);
        }
        else
        {
            // stop movement when player reaches original Y position
            rb.linearVelocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // move player
        transform.position = new Vector2(transform.position.x + horizVelocity * Time.deltaTime, transform.position.y);
    }
}
