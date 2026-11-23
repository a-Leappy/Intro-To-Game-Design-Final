using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0.29f, 0f);

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
    }
}