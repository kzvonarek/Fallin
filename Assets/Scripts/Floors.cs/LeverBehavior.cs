using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    [SerializeField] bool isLever;
    private GameObject lockedFloor;
    [SerializeField] AudioSource leverSound;
    [SerializeField] Sprite leverOnSprite;
    void Start()
    {
        leverSound = GameObject.FindWithTag("Lever Sound").GetComponent<AudioSource>();
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
        if (this.isLever && (other.CompareTag("Player") || other.CompareTag("Shield Effect")))
        {
            leverSound.Play();
            GetComponent<SpriteRenderer>().sprite = leverOnSprite;
            Destroy(lockedFloor);
        }
    }
}
