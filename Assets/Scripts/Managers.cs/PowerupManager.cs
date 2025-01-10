using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    private GameObject player;
    private bool uncollected;

    // mini powerup
    private bool currMini;
    [SerializeField] float miniTimer;

    // shield powerup
    private bool currShielded;
    [SerializeField] float shieldTimer;

    // bomb powerup
    private bool destroyFloors;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uncollected = true;

        // mini powerup
        currMini = false;

        // shield powerup
        currShielded = false;

        // bomb powerup
        destroyFloors = false;
    }

    void Update()
    {

    }

    private IEnumerator WaitAndRevert(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // mini powerup
        if (currMini)
        {
            player.gameObject.transform.localScale = new Vector3(3.343594f, 3.343594f, 3.343594f);
            currMini = false;
            Destroy(gameObject);
        }

        // shield powerup
        if (currShielded)
        {
            player.transform.Find("Shield Effect").gameObject.SetActive(false);
            player.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            currShielded = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uncollected = false;

            // mini powerup
            if (this.gameObject.CompareTag("Mini Powerup"))
            {
                other.gameObject.transform.localScale = new Vector3(1.671797f, 1.671797f, 1.671797f); // player
                currMini = true;
                Destroy(transform.Find("Mini Powerup Sprite").gameObject);

                StartCoroutine(WaitAndRevert(miniTimer));
            }

            // shield powerup
            if (this.gameObject.CompareTag("Shield Powerup"))
            {
                other.transform.Find("Shield Effect").gameObject.SetActive(true); // player
                other.gameObject.GetComponent<PolygonCollider2D>().enabled = false; // player
                currShielded = true;
                Destroy(transform.Find("Shield Powerup Sprite").gameObject);

                StartCoroutine(WaitAndRevert(shieldTimer));
            }

            // bomb powerup
            if (this.gameObject.CompareTag("Bomb Powerup"))
            {
                Destroy(gameObject);
                destroyFloors = true;

                GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
                foreach (GameObject floor in floors)
                {
                    if (floor.transform.parent != null)
                    {
                        Destroy(floor.transform.parent.gameObject);
                    }
                    Destroy(floor);
                }
                destroyFloors = false;
            }
        }

        // destroy powerup if uncollected
        if (other.gameObject.CompareTag("Destroy Zone") && uncollected)
        {
            Destroy(this.gameObject);
        }
    }
}
