using UnityEngine;

public class GooBehavior : MonoBehaviour
{
    private GameObject player;
    private bool thisGooFloor = false;

    // player drag/swipe vars
    [SerializeField] float dragDistance = 50f; // minimum distance for a swipe/drag to be registered
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    // access to currMini variable
    private GameObject miniPowerup;
    private PowerupManager pMscript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // behavior if player is stuck in goo
        if (player.GetComponent<PlayerBehavior>().stuckInGoo && thisGooFloor)
        {
            // allow for access to currMini variable
            miniPowerup = GameObject.FindWithTag("Mini Powerup");
            if (miniPowerup == null)
            {
                // do nothing
            }
            else
            {
                pMscript = miniPowerup.GetComponent<PowerupManager>();
            }

            if (pMscript != null && pMscript.currMini)
            {
                player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - 0.3f);
            }
            else if (pMscript == null || !pMscript.currMini)
            {
                player.transform.position = new Vector2(player.transform.position.x, this.transform.position.y - 0.5f);
            }

            // check for mouse drag
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;
                // make sure that start pos is > than end pos, and that the distance 'traveled' is > dragDistance
                if (startTouchPosition.y > endTouchPosition.y && Mathf.Abs(startTouchPosition.y - endTouchPosition.y) > dragDistance)
                {
                    player.GetComponent<PlayerBehavior>().stuckInGoo = false;
                    thisGooFloor = false;
                }
            }

            // check for screen swipe
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    if (startTouchPosition.y > endTouchPosition.y && Mathf.Abs(startTouchPosition.y - endTouchPosition.y) > dragDistance)
                    {
                        player.GetComponent<PlayerBehavior>().stuckInGoo = false;
                        thisGooFloor = false;
                    }
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