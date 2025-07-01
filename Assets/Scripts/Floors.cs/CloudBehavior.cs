using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    // cloud spawning
    private GameObject cloudSpawner;

    // cloud movement
    [SerializeField] float cloudHorizVelocity;

    // cloud 'popping'
    private Ray touchRaycast;
    private RaycastHit hitCloud;
    private GameObject player;
    [SerializeField] AudioSource cloudDestroySound;

    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;

    void Start()
    {
        // allow for access to vertObjSpeed/isDead variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();

        cloudSpawner = transform.parent.gameObject;
        transform.rotation = Quaternion.Euler(0, 0, 90);

        player = GameObject.FindGameObjectWithTag("Player");
        cloudDestroySound = GameObject.FindWithTag("Cloud Destroy Sound").GetComponent<AudioSource>();
    }

    void Update()
    {
        // automatically destroy cloud when it goes off screen
        if (transform.position.y >= 20 || transform.position.y <= -20)
        {
            Destroy(gameObject);
        }

        if (gMscript.isDead == false)
        {
            // allow player to manually 'pop' cloud (Mobile)
            if (Input.touchCount > 0)
            {
                Touch touch;
                // check each touch
                for (int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began)
                    {
                        // check if the touch is on a cloud
                        touchRaycast = Camera.main.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(touchRaycast, out hitCloud))
                        {
                            if (hitCloud.transform == transform) // check if the object hit is a cloud
                            {
                                cloudDestroySound.Play();
                                Destroy(gameObject); // 'pop' cloud

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
        // cloud movement (horizontal, vertical)
        // move rightwards if spawned from a spawner on left side of screen
        if (cloudSpawner.GetComponent<CloudSpawnerBehavior>().isLeftSpawner == true)
        {
            transform.position = new Vector2(transform.position.x + cloudHorizVelocity, transform.position.y + gMscript.vertObjSpeed / 2 * Time.deltaTime);
        }
        // move leftwards if spawned from a spawner on right side of screen
        else
        {
            transform.position = new Vector2(transform.position.x - cloudHorizVelocity, transform.position.y + gMscript.vertObjSpeed / 2 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    // allows player to manually 'pop' cloud (PC)
    void OnMouseDown()
    {
        if (gMscript.isDead == false)
        {
            cloudDestroySound.Play();
            Destroy(this.gameObject); // 'pop' cloud
        }
    }
}
