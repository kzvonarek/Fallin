using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    // portal exit and entrance
    [SerializeField] bool isEntrance;
    private Transform portalExit;
    private GameObject player;

    void Start()
    {
        if (isEntrance)
        {
            portalExit = transform.parent.Find("Portal Exit");
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntrance && (other.gameObject.CompareTag("Player") || other.CompareTag("Shield Effect"))) // find portal exit's corresponding exit
        {
            string targetExitName;
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
            player.transform.position = targetExit.position;
        }

        // destroy both portal parts on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
