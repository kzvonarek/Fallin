using UnityEngine;

public class CrackedFloor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // when player collides w/ cracked floor, destroy it
        // TODO: have particle effect on destroy
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}