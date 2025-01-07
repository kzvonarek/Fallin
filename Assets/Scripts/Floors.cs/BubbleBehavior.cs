using Unity.Collections;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    private GameObject player;
    private GameObject bubbleLauncher;
    private bool thisBubble = false;
    [SerializeField] float bubbleHorizVelocity;

    // bubble popping
    private Ray touchRaycast;
    private RaycastHit hitBubble;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bubbleLauncher = transform.parent.gameObject;
    }

    void Update()
    {
        // automatically pop bubble if it goes off screen
        if (gameObject.transform.position.y >= 17)
        {
            Destroy(gameObject);
        }

        // ----====----

        // allow player to manually pop bubble (Mobile)
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
            Destroy(this.gameObject);
        }
    }

    // allows player to manually pop bubble (PC)
    void OnMouseDown()
    {
        Destroy(gameObject); // 'pop' bubble

        if (player.GetComponent<PlayerBehavior>().stuckInBubble == true)
        {
            player.GetComponent<PlayerBehavior>().stuckInBubble = false;
        }
    }
}