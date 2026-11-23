
using UnityEngine;
using Unity.Netcode;

public class playerCameraSetup : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        Camera cam = Camera.main;
        

        if (cam != null)
        {
            cameraFollow follow = cam.GetComponent<cameraFollow>();

            if (follow != null)
            {
                follow.player = transform;
            }
            cameraMouse mouse = Camera.main.GetComponent<cameraMouse>();
            mouse.canMove = true;
        }
    }
}
