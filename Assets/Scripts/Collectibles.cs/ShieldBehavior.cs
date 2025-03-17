using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float yOffset;

    void Update()
    {
        // shield constantly follows player
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
    }
}
