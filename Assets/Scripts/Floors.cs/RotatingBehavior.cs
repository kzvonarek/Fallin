using UnityEngine;

public class RotatingBehavior : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float rotationVariance;

    void Update()
    {
        // rotate floor around z-axis
        transform.Rotate(0, 0, rotationVariance * rotationSpeed * Time.deltaTime);
    }
}
