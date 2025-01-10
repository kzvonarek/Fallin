using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    private GameObject player;
    private bool uncollected;

    // mini powerup
    private bool currMini;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uncollected = true;

        // mini powerup
        currMini = false;
    }

    private IEnumerator WaitAndRevert(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currMini)
        {
            player.gameObject.transform.localScale = new Vector3(3.343594f, 3.343594f, 3.343594f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uncollected = false;
            if (this.gameObject.CompareTag("Mini Powerup"))
            {
                other.gameObject.transform.localScale = new Vector3(1.671797f, 1.671797f, 1.671797f);
                currMini = true;
                StartCoroutine(WaitAndRevert(6.0f));
            }
        }

        if (other.gameObject.CompareTag("Destroy Zone") && uncollected)
        {
            Destroy(this.gameObject);
        }
    }
}
