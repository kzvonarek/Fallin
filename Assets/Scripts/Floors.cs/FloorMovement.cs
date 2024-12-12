using System;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    // access to GameManager.cs
    private GameObject gameManagerObj;
    private GameManager gMscript;
    [SerializeField] bool isMovingFloor;
    [SerializeField] float moveMultiplier;

    void Start()
    {
        // allow for access to vertObjSpeed variable
        gameManagerObj = GameObject.FindWithTag("Game Manager");
        gMscript = gameManagerObj.GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        // floor moves up at a set speed (speed increases over time)
        if (!isMovingFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + gMscript.vertObjSpeed * Time.deltaTime);
        }
        else if (isMovingFloor)
        {
            // move back and forth if is a moving floor
            transform.position = new Vector2(Mathf.Sin(Time.time * moveMultiplier), transform.position.y + gMscript.vertObjSpeed * Time.deltaTime);
        }
    }
}
