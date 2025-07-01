using Unity.Collections;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    private GameObject player;
    private GameObject bubbleLauncher;
    private bool thisBubble = false;
    [SerializeField] float bubbleHorizVelocity;

    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;

    // bubble popping
    private Ray touchRaycast;
    private RaycastHit hitBubble;
    [SerializeField] AudioSource bubblePopSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bubbleLauncher = transform.parent.gameObject;

        // allow for access to isDead variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
        bubblePopSound = GameObject.FindWithTag("Bubble Pop Sound").GetComponent<AudioSource>();
    }

    void Update()
    {
        // allow player to manually pop bubble (Mobile)
        if (gMscript.isDead == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch;
                // check each touch
                for (int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began)
                    {
                        // check if the touch is on a bubble
                        touchRaycast = Camera.main.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(touchRaycast, out hitBubble))
                        {
                            if (hitBubble.transform == transform) // check if the object hit is a bubble
                            {
                                Destroy(gameObject); // 'pop' bubble
                                bubblePopSound.Play();

                                if (player.GetComponent<PlayerBehavior>().stuckInBubble == true)
                                {
                                    player.GetComponent<PlayerBehavior>().stuckInBubble = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        // bubble movement (horizontal, vertical w/ launcher)
        // move rightwards if spawned from a launcher on left side of screen
        if (bubbleLauncher.GetComponent<BubbleLauncherBehavior>().isLeftLauncher == true)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + bubbleHorizVelocity, bubbleLauncher.transform.position.y);
        }
        // move leftwards if spawned from a launcher on right side of screen
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - bubbleHorizVelocity, bubbleLauncher.transform.position.y);
        }

        // ----====----

        // move player with bubble if stuck
        if (player.GetComponent<PlayerBehavior>().stuckInBubble && thisBubble)
        {
            player.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.60f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisBubble = true;
        }

        // automatically pop bubble if it hits left/right-most Pop Zone
        if (other.gameObject.CompareTag("Pop Zone"))
        {
            bubblePopSound.Play();
            Destroy(this.gameObject);
        }
    }

    // allows player to manually pop bubble (PC)
    void OnMouseDown()
    {
        if (gMscript.isDead == false)
        {
            bubblePopSound.Play();
            Destroy(gameObject); // 'pop' bubble

            if (player.GetComponent<PlayerBehavior>().stuckInBubble == true)
            {
                player.GetComponent<PlayerBehavior>().stuckInBubble = false;
            }
        }
    }
}