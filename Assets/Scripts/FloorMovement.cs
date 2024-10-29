using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] float vertSpeed;

    void Update()
    {
        // speed increases over time
        // TO-DO: add while loop, so there is an eventual max speed it can reach and stop at
        vertSpeed += 0.001f;
    }

    void FixedUpdate()
    {
        // floor moves up at a set speed (speed increases over time)
        transform.position = new Vector2(transform.position.x, transform.position.y + vertSpeed * Time.deltaTime);
    }
}
