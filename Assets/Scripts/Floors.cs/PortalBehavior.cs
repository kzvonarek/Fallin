using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    // portal exit and entrance
    [SerializeField] bool isEntrance;
    private GameObject player;
    private Transform portalExit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isEntrance)
        {
            portalExit = transform.parent.Find("Portal Exit");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && other.gameObject.CompareTag("Player")) // find portal exit's corresponding exit
        {
            string targetExitName = "";
            switch (this.name)
            {
                case "Portal Entrance L":
                    targetExitName = "Portal Exit R";
                    break;
                case "Portal Entrance B":
                    targetExitName = "Portal Exit B";
                    break;
                case "Portal Entrance T":
                    targetExitName = "Portal Exit B";
                    break;
                case "Portal Entrance R":
                    targetExitName = "Portal Exit L";
                    break;
                default:
                    Debug.LogError("Invalid");
                    return;
            }

            Transform targetExit = portalExit.transform.Find(targetExitName);
            other.transform.position = targetExit.position;
            // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            // player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // destroy both portal parts on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
