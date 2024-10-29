using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] bool isEntrance;
    private GameObject portalExit;

    void Start()
    {
        portalExit = GameObject.FindWithTag("Portal Exit");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && other.gameObject.CompareTag("Player")) // Portal Entrance
        {
            other.transform.position = new Vector2(portalExit.transform.position.x, transform.position.y);
        }

        // destroy both portal parts on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
