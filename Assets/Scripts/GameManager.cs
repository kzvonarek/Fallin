using UnityEngine;

public class GameManager : MonoBehaviour
{
    // floor speed
    public float vertObjSpeed;
    [SerializeField] float objSpeedInc;
    [SerializeField] float maxObjSpeed;

    void Update()
    {
        // floor speed increasing until it is greater than max floor speed
        if (vertObjSpeed < maxObjSpeed)
        {
            vertObjSpeed += objSpeedInc * Time.deltaTime;
        }
    }
}
