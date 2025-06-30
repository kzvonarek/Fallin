using UnityEngine;

public class BGMSoundManager : MonoBehaviour
{
    // don't destroy on load
    private static BGMSoundManager instance;

    void Awake()
    {
        // don't destroy on load to new scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
