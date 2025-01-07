using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerBehavior : MonoBehaviour
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

    // functionality with Leaf Floor for player having leaf effect
    [HideInInspector] public bool playerLeafed;
    [SerializeField] float leafMovementMultiplier;
    [SerializeField] GameObject leafEffect;
    [SerializeField] float leafTimeFrame;
    [SerializeField] int neededLeafTaps;
    private float timer = 0.0f;
    private int tapCount = 0;

    // Player Arrow behavior
    private GameObject playerArrow;

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

        // allow access to playerArrow
        playerArrow = GameObject.FindGameObjectWithTag("Player Arrow");
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
                if (playerLeafed)
                {
                    targetVelocity *= leafMovementMultiplier;
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
        if (transform.position.y >= 16f)
        {
            gMscript.death();
        }

        // check if player is out of screen vision, if true -> show player arrow
        if (transform.position.y >= 10f)
        {
            playerArrow.SetActive(true);
        }
        else // if false -> don't show player arrow
        {
            playerArrow.SetActive(false);
        }

        //-----=-----
        // LEAF FLOOR BEHAVIOR

        // behavior if player has 'leaves' stuck on them
        if (playerLeafed)
        {
            // check for mouse click or screen tap
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                tapCount++;

                // reset timer when first tap is detected
                if (tapCount == 1)
                {
                    timer = leafTimeFrame;
                }
            }

            // if timer is running, decrease it
            if (tapCount > 0)
            {
                timer -= Time.deltaTime;

                // remove 'leaves' from player if three taps/clicks within time frame
                if (tapCount == neededLeafTaps)
                {
                    playerLeafed = false;
                }

                // if timer expires before three taps/clicks
                if (timer <= 0)
                {
                    tapCount = 0;
                    timer = 0.0f;
                }
            }
        }

        // check if player is not leafed anymore, disabling leaf effect
        if (playerLeafed == false)
        {
            leafEffect.SetActive(false);
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
        else if (other.gameObject.CompareTag("Leaf"))
        {
            playerLeafed = true;
            leafEffect.SetActive(true);
        }
    }
}
