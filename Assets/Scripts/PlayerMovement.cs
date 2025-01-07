using UnityEngine;
using UnityEngine.SceneManagement;
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

    // functionality with GooBehavior.cs for player being stuck in goo
    [HideInInspector] public bool stuckInGoo;

    // functionality with BubbleBehavior.cs for player being stuck in bubble
    [HideInInspector] public bool stuckInBubble;

    // funtionality with GameManager.cs for dead() function
    private GameObject gameManagerObj;
    private GameManager gMscript;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // for floating behavior
        playerOrigYPos = rb.position.y;

        // allow for access to death() function
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
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

                // check if player is currently in goo, and needs to be slowed down
                // check if player is stuck in goo
                if (stuckInGoo)
                {
                    // apply slower movement when in goo
                    targetVelocity = 0;
                }

                // apply velocity to the player
                horizVelocity = Mathf.Clamp(targetVelocity, -maxSpeed, maxSpeed);
            }
        }

        else
        {
            holdTime = 0f;
            horizVelocity *= drag;
        }

        //-----=-----

        //PLAYER FLOATING UP
        //apply upward movement if player is below original Y position
        if (rb.position.y < playerOrigYPos - 0.1)
        {
            // set a velocity to float the player upwards gradually
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerFloatSpeed);
        }
        // PLAYER FLOATING DOWN
        // apply downward movement if player is above original Y position
        else if (rb.position.y > playerOrigYPos + 0.1)
        {
            if (!stuckInGoo || !stuckInBubble)
            {
                // set a velocity to float the player upwards gradually
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -playerFloatSpeed);
            }
        }
        else
        {
            // stop movement when player reaches original Y position
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }

        //-----=-----

        // check if player exceeds top of screen, if true -> end game
        if (transform.position.y >= 14f)
        {
            gMscript.death();
        }
    }

    void FixedUpdate()
    {
        // move player
        transform.position = new Vector2(transform.position.x + horizVelocity * Time.deltaTime, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goo"))
        {
            stuckInGoo = true;
        }
        else if (other.gameObject.CompareTag("Bubble"))
        {
            stuckInBubble = true;
        }
    }
}
