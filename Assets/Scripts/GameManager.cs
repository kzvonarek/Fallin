using UnityEngine;

public class GameManager : MonoBehaviour
{
    // floor speed for FloorMovement.cs
    public float vertObjSpeed;
    [SerializeField] float objSpeedInc;
    [SerializeField] float maxObjSpeed;

    // player original Y position for PortalBehavior.cs
    [SerializeField] GameObject player;
    public Rigidbody2D playerRb;
    public float playerOrigYPos;

    void Start()
    {
        // get player original Y position [PortalBehavior.cs]
        playerOrigYPos = playerRb.position.y;
    }

    void Update()
    {
        // floor speed increasing until it is greater than max floor speed [FloorMovement.cs]
        if (vertObjSpeed < maxObjSpeed)
        {
            vertObjSpeed += objSpeedInc * Time.deltaTime;
        }
    }
}
