using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    /*
    public void menuButtonClick(AudioClip buttonSound)
    {
        audioSource.PlayOneShot(buttonSound);
    }
    */
}
