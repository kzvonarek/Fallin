using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    // scrolling background
    [SerializeField] Renderer bgRenderer;
    [SerializeField] float speed;

    // don't destroy on load
    private static BackgroundScroller instance;

    void Update()
    {
        // horizontal scroll
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
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
    }
}
