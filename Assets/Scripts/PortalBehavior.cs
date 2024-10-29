using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    // portal exit and entrance
    [SerializeField] bool isEntrance;
    private GameObject portalExit;

    // player floating back up
    private GameObject player;
    private Rigidbody2D playerRb;
    private bool inPortal = false;
    private float origYPos;
    [SerializeField] float playerFloatSpeed = 10f;

    void Start()
    {
        // Find the correct portal exit within the same parent object
        if (isEntrance)
        {
            // Assume the exit is the other child under the same parent
            Transform parentTransform = transform.parent;
            foreach (Transform child in parentTransform)
            {
                if (child != transform && child.CompareTag("Portal Exit"))
                {
                    portalExit = child.gameObject;
                    break;
                }
            }
        }

        // player
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        origYPos = playerRb.position.y;
    }

    void Update()
    {
        if (inPortal && playerRb.position.y < origYPos)
        {
            // smoothly move player up to the original Y position
            float newY = Mathf.MoveTowards(playerRb.position.y, origYPos, playerFloatSpeed * Time.deltaTime);
            playerRb.position = new Vector2(playerRb.position.x, newY);

            // once player has reached original position, stop moving
            if (Mathf.Approximately(playerRb.position.y, origYPos))
            {
                inPortal = false;
                playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && other.gameObject.CompareTag("Player")) // Portal Entrance
        {
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            other.transform.position = new Vector2(portalExit.transform.position.x, portalExit.transform.position.y);
            inPortal = true;
        }

        // destroy both portal parts on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
