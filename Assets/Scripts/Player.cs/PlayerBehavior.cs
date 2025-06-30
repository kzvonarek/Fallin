using UnityEngine;
using System.Collections;
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    // player movement
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
    [SerializeField] float playerFloatSpeedUp;
    [SerializeField] float playerFloatSpeedDown; // used for goo/cloud/bubble to float downwards

    // functionality with GooBehavior.cs for player being stuck in goo
    [HideInInspector] public bool stuckInGoo;

    // functionality with BubbleBehavior.cs for player being stuck in bubble
    [HideInInspector] public bool stuckInBubble;

    // functionality with Leaf Floor for player having leaf effect
    private bool playerLeafed;
    [SerializeField] float leafMovementMultiplier;
    [SerializeField] GameObject leafEffect;
    private float leafTimeFrame = 1f;
    [SerializeField] int neededLeafTaps;
    private float leafTimer;
    private int leafTapCount = 0;
    private float leafDecayTimer;
    [SerializeField] float leafDecayTimeFrame;

    // player arrow behavior
    private GameObject playerArrow;

    // funtionality with GameManager.cs for dead() function and currSlowedTime variable
    private GameObject gameManagerObj;
    private GameManager gMscript;

    // cloud floor behavior
    [SerializeField] float cloudJumpForce;
    private bool playerClouded;

    // wind area behavior
    private bool inWindArea;

    // access to currSlow variable
    private GameObject slowPowerupObj;
    private PowerupManager pMscript;

    // shield effect
    public GameObject shieldEffect;

    [SerializeField] AudioSource gooSound;
    [SerializeField] AudioSource bubbleSound;
    [SerializeField] AudioSource leafSound;
    [SerializeField] AudioSource windSound;
    [SerializeField] AudioSource cloudSound;

    void Start()
    {
        shieldEffect.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        // for floating behavior
        playerOrigYPos = rb.position.y;

        // allow for access to death() function
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();

        // allow access to playerArrow
        playerArrow = GameObject.FindGameObjectWithTag("Player Arrow");

        leafDecayTimer = 0f;

        inWindArea = false;
    }

    void Update()
    {
        // PLAYER DRAGGING (back and forth movement condition)
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
            Touch touch = Input.GetTouch(0);

            // ensure touch is valid
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                isDragging = true;
                inputPosition = touch.position;
            }
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

        if (!playerClouded)
        {
            //PLAYER FLOATING UP
            //apply upward movement if player is below original Y position
            if (rb.position.y < playerOrigYPos - 0.1)
            {
                // set a velocity to float the player upwards gradually
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerFloatSpeedUp);
            }
            // PLAYER FLOATING DOWN
            // apply downward movement if player is above original Y position
            else if (rb.position.y > playerOrigYPos + 0.1)
            {
                if (!stuckInGoo || !stuckInBubble)
                {
                    // set a velocity to float the player upwards gradually
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -playerFloatSpeedDown);
                }
            }
            else
            {
                // stop movement when player reaches original Y position
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
        }

        //-----=-----

        // PLAYER SCREEN POSITION CHECKS

        // check if player exceeds top of screen, if true -> end game
        if (transform.position.y >= 16f)
        {
            gMscript.death();
        }

        // check if player is out of screen view, if true -> show player arrow
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
        if (playerLeafed && gMscript.isDead == false)
        {
            leafEffect.SetActive(true);
            leafDecayTimer += Time.deltaTime;

            // check for mouse click
            if (Input.GetMouseButtonDown(0))
            {
                leafTapCount++;
                // reset timer when the first tap is detected
                if (leafTapCount == 1)
                {
                    leafTimer = leafTimeFrame;
                }
            }

            // check for screen tap
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    leafTapCount++;
                    // reset timer when the first tap is detected
                    if (leafTapCount == 1)
                    {
                        leafTimer = leafTimeFrame;
                    }
                }
            }

            // if timer is running, decrease it
            if (leafTapCount > 0)
            {
                leafTimer -= Time.deltaTime;

                // remove 'leaves' from player if three taps/clicks within time frame
                if (leafTapCount == neededLeafTaps)
                {
                    playerLeafed = false;
                }

                // if timer expires before three taps/clicks
                if (leafTimer <= 0)
                {
                    leafTapCount = 0;
                    leafTimer = 0.0f;
                }
            }

            // destroy leaf effect/slow down after decay time frame
            if (leafDecayTimer >= leafDecayTimeFrame)
            {
                playerLeafed = false;
            }
        }

        // check if player is not leafed anymore, disabling leaf effect
        if (playerLeafed == false)
        {
            leafTapCount = 0;
            leafTimer = 0.0f;
            leafDecayTimer = 0f;
            leafEffect.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        // check if player is currently in goo, and needs to be slowed down completely
        if (stuckInGoo)
        {
            // apply no player movement when in goo
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        else if (!inWindArea)
        {
            // move player L/R
            transform.position = new Vector2(transform.position.x + horizVelocity * Time.deltaTime, transform.position.y);
        }

        // allow for access to currSlow variable
        slowPowerupObj = GameObject.FindWithTag("Mini Powerup");
        if (slowPowerupObj == null)
        {
            // do nothing
        }
        else
        {
            pMscript = slowPowerupObj.GetComponent<PowerupManager>();
        }

        if (playerClouded && (pMscript == null || !pMscript.currMini))
        {
            rb.AddForce(Vector2.up * cloudJumpForce, ForceMode2D.Impulse);
            StartCoroutine(ResetPlayerClouded());
        }
        else if (playerClouded && pMscript != null && pMscript.currSlow)
        {
            rb.AddForce(Vector2.up * (cloudJumpForce / 4), ForceMode2D.Impulse); // reduced force when slowed
            StartCoroutine(ResetPlayerClouded());
        }
        else if (playerClouded)
        {
            rb.AddForce(Vector2.up * cloudJumpForce, ForceMode2D.Impulse);
            StartCoroutine(ResetPlayerClouded());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goo"))
        {
            gooSound.Play();
            stuckInGoo = true;
        }
        else if (other.gameObject.CompareTag("Bubble"))
        {
            bubbleSound.Play();
            stuckInBubble = true;
        }
        else if (other.gameObject.CompareTag("Leaf"))
        {
            leafSound.Play();
            playerLeafed = true;
        }
        else if (other.gameObject.CompareTag("Wind"))
        {
            windSound.Play();
            playerLeafed = false;
            inWindArea = true;
        }
        else if (other.gameObject.CompareTag("Cloud")) // Cloud Floor Behavior
        {
            cloudSound.Play();
            playerClouded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            inWindArea = false;
            horizVelocity = 0f; // reset horizontal velocity when exiting wind area
        }
    }

    private IEnumerator ResetPlayerClouded()
    {
        yield return new WaitForSeconds(0.2f); // wait for cloudJumpForce to apply
        playerClouded = false;
    }
}
