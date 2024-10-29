using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    // portal exit and entrance
    [SerializeField] bool isEntrance;
    private GameObject portalExit;

    void Start()
    {
        // find correct portal exit within parent object
        if (isEntrance)
        {
            // assume exit is other child under parent w/ correct tag
            Transform parentTransform = transform.parent;
            foreach (Transform child in parentTransform)
            {
                if (child != transform && child.CompareTag("Portal Exit"))
                {
                    portalExit = child.gameObject;
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && other.gameObject.CompareTag("Player")) // portal Entrance
        {
            other.transform.position = new Vector2(portalExit.transform.position.x, portalExit.transform.position.y);
        }

        // destroy both portal parts on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
