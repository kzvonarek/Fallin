using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    private GameObject player;
    private bool thisGooFloor = false;

    // player taps/clicks vars
    [SerializeField] float timeFrame;
    [SerializeField] int neededTaps;
    private float timer = 0.0f;
    private int tapCount = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // behavior if player is stuck in goo
        if (player.GetComponent<PlayerBehavior>().stuckInGoo && thisGooFloor)
        {
            player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - 0.5f);

            // check for mouse click or screen tap
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                tapCount++;

                // reset timer when first tap is detected
                if (tapCount == 1)
                {
                    timer = timeFrame;
                }
            }

            // if timer is running, decrease it
            if (tapCount > 0)
            {
                timer -= Time.deltaTime;

                // remove player from goo if three taps/clicks within time frame
                if (tapCount == neededTaps)
                {
                    player.GetComponent<PlayerBehavior>().stuckInGoo = false;
                    thisGooFloor = false;
                }

                // if timer expires before three taps/clicks
                if (timer <= 0)
                {
                    tapCount = 0;
                    timer = 0.0f;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisGooFloor = true;
        }
    }
}