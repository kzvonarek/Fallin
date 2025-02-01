using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    // make player arrow follow player.x position
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
}
