using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    // portal exit and entrance
    [SerializeField] bool isEntrance;
    private GameObject portalExit;

    // player floating back up
    private bool inPortal = false;
    [SerializeField] float playerFloatSpeed = 10f;

    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;

    void Start()
    {
        // allow for access to playerOrigYPos and playerRb from GameManager.cs
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();

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
    }

    void Update()
    {
        // Only apply upward movement if player is below the original Y position
        if (inPortal)
        {
            if (gMscript.playerRb.position.y < gMscript.playerOrigYPos)
            {
                // Set a velocity to float the player upwards gradually
                gMscript.playerRb.linearVelocity = new Vector2(gMscript.playerRb.linearVelocity.x, playerFloatSpeed);
            }
            else
            {
                // Once the player is at or above the original position, stop the movement
                gMscript.playerRb.linearVelocity = Vector2.zero; // Stop any additional movement
                gMscript.playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
                inPortal = false; // Stop further updates
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && other.gameObject.CompareTag("Player")) // portal Entrance
        {
            gMscript.playerRb.constraints = RigidbodyConstraints2D.None;
            gMscript.playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
