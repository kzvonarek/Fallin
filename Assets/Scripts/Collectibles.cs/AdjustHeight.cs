using UnityEngine;

public class AdjustHeight : MonoBehaviour
{
    private bool teleported = false;
    // adjust height of collectible (currency or powerup) to prevent it getting stuck in floors
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Collectibles") && teleported == false)
            {
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 3f);
                teleported = true;
            }
        }
    }
}
