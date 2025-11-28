using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;      
    public Vector3 offset = new Vector3(0f, 1f, 0f); // Height above the player

    void LateUpdate()
    {
        if (player == null) return;

        // Follow the player's position + offset
        transform.position = player.position + offset;

        // Make sure this object NEVER rotates with the player
        transform.rotation = Quaternion.identity;
    }
}


