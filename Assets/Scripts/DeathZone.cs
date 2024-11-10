using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathZone : MonoBehaviour
{
    // check for player collision with death zone
    void OnCollisionEnter2D(Collision2D collision)
    {
        // end game on collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // reset scene (temporary [look at notes])
            SceneManager.LoadScene("Main Scene");
            // for testing (remove later)
            Debug.Log("Game Over");
        }
    }
}
