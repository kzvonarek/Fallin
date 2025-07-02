using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMSoundManager : MonoBehaviour
{
    // don't destroy on load
    private static BGMSoundManager instance;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "Game Play")
        {
            this.GetComponent<AudioSource>().pitch = 1.0f;
        }
    }

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
