using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorCollisions : MonoBehaviour
{
    // check for floor collisions with other objects
    void OnTriggerEnter2D(Collider2D other)
    {
        // end game on collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            // reset scene (temporary [look at notes])
            SceneManager.LoadScene("Main Scene");
            // for testing (remove later)
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
