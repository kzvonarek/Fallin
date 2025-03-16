using UnityEngine;

public class WindBehavior : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] float windForce;
    [SerializeField] bool isLeftWind;
    private Vector2 pushDirection;

    void OnTriggerEnter2D(Collider2D other)
    {
        // push player in direction (L/R) when enters wind area
        if (other.gameObject.CompareTag("Player"))
        {
            playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (isLeftWind)
            {
                pushDirection = Vector2.left * windForce;
            }
            else
            {
                pushDirection = Vector2.right * windForce;
            }
            playerRb.AddForce(pushDirection, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // continuously apply wind force while player is in wind area
        if (other.gameObject.CompareTag("Player") && playerRb != null)
        {
            playerRb.AddForce(pushDirection, ForceMode2D.Force);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // reset playerRb and stop wind force when player exits wind area
        if (other.gameObject.CompareTag("Player"))
        {
            playerRb.linearVelocity = new Vector2(0, playerRb.linearVelocity.y); // stop horizontal movement
            playerRb = null;
        }
    }
}
