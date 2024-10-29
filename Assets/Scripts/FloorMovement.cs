using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    private GameManager gMscript;
    void Start()
    {
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        // floor moves up at a set speed (speed increases over time)
        transform.position = new Vector2(transform.position.x, transform.position.y + gMscript.vertFloorSpeed * Time.deltaTime);
    }
}
