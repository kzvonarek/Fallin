using UnityEngine;

public class WindBehavior : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] float windForce;
    [SerializeField] bool isLeftWind;
    private Vector2 pushDirection;

    void OnTriggerEnter2D(Collider2D other)
    {
        // push player in direction (L/R)
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
}
