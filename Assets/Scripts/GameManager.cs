using UnityEngine;

public class GameManager : MonoBehaviour
{
    // floor speed
    public float vertFloorSpeed;
    [SerializeField] float floorSpeedInc;
    [SerializeField] float maxFloorSpeed;
    void Start()
    {

    }

    void Update()
    {
        // floor speed increasing until it is greater than max floor speed
        if (vertFloorSpeed < maxFloorSpeed)
        {
            vertFloorSpeed += floorSpeedInc * Time.deltaTime;
        }
    }
}
