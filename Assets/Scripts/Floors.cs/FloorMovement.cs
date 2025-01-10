using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class FloorMovement : MonoBehaviour
{
    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;

    // moving floor attirbutes
    [SerializeField] bool isMovingFloor;
    [SerializeField] float frequency; // speed back and forth
    [SerializeField] float amplitude; // distance back and forth
    [SerializeField] int variance; // add randomness to movement

    void Start()
    {
        // allow for access to vertObjSpeed and currSlowedTime variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();

        // random values for variance
        int[] randomValue = new int[] { 1, -1 };
        variance = randomValue[Random.Range(0, 2)];
    }

    void FixedUpdate()
    {
        // floor moves up at a set speed (speed increases over time)
        if (!isMovingFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + gMscript.vertObjSpeed * Time.deltaTime);
        }
        else if (isMovingFloor) // move back and forth if is a moving floor
        {
            transform.position = new Vector2(variance * (amplitude * Mathf.Sin(Time.time * frequency)), transform.position.y + gMscript.vertObjSpeed * Time.deltaTime);
        }
    }
}
