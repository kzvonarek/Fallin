using UnityEngine;

public class CrackedFloor : MonoBehaviour
{
    [SerializeField] AudioSource crackedSound;

    void Start()
    {
        crackedSound = GameObject.FindWithTag("Cracked Sound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when player collides w/ cracked floor, destroy it
        // TODO: have particle effect on destroy
        if (other.gameObject.CompareTag("Player"))
        {
            crackedSound.Play();
            Destroy(this.gameObject);
        }
    }
}