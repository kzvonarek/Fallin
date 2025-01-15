using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    private GameObject player;
    private bool uncollected;

    // visiual timer slider for powerups
    private GameObject powerupTimerObj;
    private Slider powerupTimerSlider;
    private bool powerupActive;

    // mini powerup
    [HideInInspector] public bool currMini;
    [SerializeField] float miniTimer;
    private GameObject miniIconUI;

    // shield powerup
    private bool currShielded;
    [SerializeField] float shieldTimer;
    private GameObject shieldIconUI;

    // jump powerup (replaced by Cloud)
    [SerializeField] float jumpForce;

    // slowdown powerup
    [SerializeField] float slowdownTimer;
    private bool slowTime;
    private GameObject slowdownIconUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        powerupTimerObj = GameObject.FindGameObjectWithTag("Powerup UI Timer");
        powerupTimerSlider = powerupTimerObj.GetComponent<Slider>();

        uncollected = true;
        powerupActive = false;

        // mini powerup
        currMini = false;
        miniIconUI = GameObject.FindGameObjectWithTag("Mini Me UI Icon");

        // shield powerup
        currShielded = false;
        shieldIconUI = GameObject.FindGameObjectWithTag("Shield UI Icon");

        // slowdown powerup
        slowTime = false;
        slowdownIconUI = GameObject.FindGameObjectWithTag("Slowdown UI Icon");
    }

    void Update()
    {
        if (powerupActive == true)
        {
            powerupTimerSlider.value -= Time.deltaTime;
        }
    }

    private IEnumerator WaitAndRevert(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        powerupTimerObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().enabled = false;
        powerupActive = false;

        // mini powerup
        if (currMini)
        {
            player.gameObject.transform.localScale = new Vector3(3.343594f, 3.343594f, 3.343594f);
            currMini = false;
            miniIconUI.GetComponent<Image>().enabled = false;
        }

        // shield powerup
        if (currShielded)
        {
            player.transform.Find("Shield Effect").gameObject.SetActive(false);
            player.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            currShielded = false;
            shieldIconUI.GetComponent<Image>().enabled = false;
        }

        // slowdown powerup
        if (slowTime)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            slowTime = false;
            slowdownIconUI.GetComponent<Image>().enabled = false;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uncollected = false;
            powerupActive = true;
            powerupTimerObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().enabled = true;

            // mini powerup (make player small)
            if (this.gameObject.CompareTag("Mini Powerup"))
            {
                other.gameObject.transform.localScale = new Vector3(1.671797f, 1.671797f, 1.671797f); // player
                currMini = true;

                miniIconUI.GetComponent<Image>().enabled = true;
                powerupTimerSlider.maxValue = miniTimer;
                powerupTimerSlider.value = miniTimer;

                if (transform.Find("Mini Powerup Sprite") != null)
                {
                    Transform miniSpriteTransform = transform.Find("Mini Powerup Sprite");
                    if (miniSpriteTransform != null)
                    {
                        Destroy(miniSpriteTransform.gameObject);
                    }
                }

                StartCoroutine(WaitAndRevert(miniTimer));
            }

            // shield powerup (allow temporary immunity to all objects)
            if (this.gameObject.CompareTag("Shield Powerup"))
            {
                other.transform.Find("Shield Effect").gameObject.SetActive(true); // player
                other.gameObject.GetComponent<PolygonCollider2D>().enabled = false; // player
                currShielded = true;

                shieldIconUI.GetComponent<Image>().enabled = true;
                powerupTimerSlider.maxValue = shieldTimer;
                powerupTimerSlider.value = shieldTimer;


                if (transform.Find("Shield Powerup Sprite") != null)
                {
                    Transform shieldSpriteTransform = transform.Find("Shield Powerup Sprite");
                    if (shieldSpriteTransform != null)
                    {
                        Destroy(shieldSpriteTransform.gameObject);
                    }
                }

                StartCoroutine(WaitAndRevert(shieldTimer));
            }

            // slowdown powerup (temporary slowing of player/floor movement)
            if (this.gameObject.CompareTag("Slowdown Powerup"))
            {
                slowTime = true;
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                slowdownIconUI.GetComponent<Image>().enabled = true;
                powerupTimerSlider.value = slowdownTimer;
                powerupTimerSlider.maxValue = slowdownTimer;


                if (transform.Find("Slowdown Powerup Sprite") != null)
                {
                    Transform slowdownSpriteTransform = transform.Find("Slowdown Powerup Sprite");
                    if (slowdownSpriteTransform != null)
                    {
                        Destroy(slowdownSpriteTransform.gameObject);
                    }
                }

                StartCoroutine(WaitAndRevert(slowdownTimer));
            }

            // bomb powerup (destroy all floors)
            if (this.gameObject.CompareTag("Bomb Powerup"))
            {
                powerupActive = false;
                powerupTimerObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().enabled = false;

                GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
                foreach (GameObject floor in floors)
                {
                    if (floor.transform.parent != null)
                    {
                        Destroy(floor.transform.parent.gameObject);
                    }
                    Destroy(floor);
                }

                Destroy(gameObject);
            }

            // jump powerup (push player upwards)
            // replaced by Cloud (still here temporarily)
            if (this.gameObject.CompareTag("Jump Powerup"))
            {
                powerupActive = false;
                powerupTimerObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().enabled = false;

                other.transform.position = new Vector2(other.transform.position.x,
                other.transform.position.y + jumpForce); // player

                Destroy(gameObject);
            }
        }

        // destroy powerup if uncollected
        if (other.gameObject.CompareTag("Destroy Zone") && uncollected)
        {
            Destroy(this.gameObject);
        }
    }
}
