using UnityEngine;

public class FloorCollisions : MonoBehaviour
{
    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;

    void Start()
    {
        // allow for access to death() function
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    // check for floor collisions with other objects
    void OnTriggerEnter2D(Collider2D other)
    {
        // end game on collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            gMscript.death(); // call death function from GameManager.cs
        }

        // destroy floor on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
