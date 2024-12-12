using Unity.Collections;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    private GameObject player;
    private GameObject bubbleLauncher;
    private bool thisBubble = false;
    [SerializeField] float bubbleHorizVelocity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bubbleLauncher = GameObject.FindGameObjectWithTag("Bubble Launcher");
    }

    void FixedUpdate()
    {
        // bubble movement (horizontal, vertical w/ launcher)
        // player not stuck w/ bubble, so it continues to move horizontal
        if (player.GetComponent<PlayerMovement>().stuckInBubble == false)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.1f, bubbleLauncher.transform.position.y);
        }
        // player stuck w/ bubble, so it only moves vertically w/ launcher
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, bubbleLauncher.transform.position.y);
        }

        // move player with bubble if stuck
        if (player.GetComponent<PlayerMovement>().stuckInBubble && thisBubble)
        {
            player.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f);
        }

        // ----====----

        // automatically pop bubble if it goes off screen
        if (gameObject.transform.position.x > 7 || gameObject.transform.position.x < -7 ||
        gameObject.transform.position.y >= 17)
        {
            Destroy(gameObject);
        }

        // ----====----
        // allows player to manually pop bubble (Mobile controls)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // check if the touch is on a bubble
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform) // check if the object hit is a bubble
                    {
                        Destroy(gameObject); // 'pop' bubble

                        if (player.GetComponent<PlayerMovement>().stuckInBubble == true)
                        {
                            player.GetComponent<PlayerMovement>().stuckInBubble = false;
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisBubble = true;
        }
    }

    // allows player to manually pop bubble (PC controls)
    void OnMouseDown()
    {
        Destroy(gameObject); // 'pop' bubble

        if (player.GetComponent<PlayerMovement>().stuckInBubble == true)
        {
            player.GetComponent<PlayerMovement>().stuckInBubble = false;
        }
    }
}