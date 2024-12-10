using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    [SerializeField] bool isLever;
    private GameObject lockedFloor;
    void Start()
    {
        // find correct locked floor within parent object
        if (isLever)
        {
            // assume locked floor is other child under parent w/ correct tag
            Transform parentTransform = transform.parent;
            foreach (Transform child in parentTransform)
            {
                if (child != transform && child.CompareTag("Locked Floor"))
                {
                    lockedFloor = child.gameObject;
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.isLever && other.CompareTag("Player"))
        {
            Destroy(lockedFloor);
        }
    }
}
