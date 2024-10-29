using UnityEngine;

public class FloorCollisions : MonoBehaviour
{
    // check for floor collisions with other objects
    void OnTriggerEnter2D(Collider2D other)
    {
        // end game on collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }

        // destroy floor on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
