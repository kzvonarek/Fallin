using UnityEngine;

public class RotatingBehavior : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 70f;
    [SerializeField] float rotationVariance = 1f;

    void Update()
    {
        // rotate floor around z-axis
        transform.Rotate(0, 0, rotationVariance * rotationSpeed * Time.deltaTime);
    }
}
