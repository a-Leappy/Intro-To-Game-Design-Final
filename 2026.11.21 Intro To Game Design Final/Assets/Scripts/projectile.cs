
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class projectile : NetworkBehaviour
{
    public float projectileSpeed;
    public bool isQProjectile = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        Invoke(nameof(Suicide), 2f);
    }

    public void Suicide()
    {
        if (!IsServer || !IsSpawned) return;

        CancelInvoke();
        NetworkObject.Despawn(true);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!IsServer || !IsSpawned) return;

        if (isQProjectile)
        {
            abilitiesHandler target =
                other.collider.GetComponentInParent<abilitiesHandler>();

            if (target != null && target.IsSpawned)
            {
                target.QHitClientRpc();
            }
        } else
        {
            playerManager target = other.collider.GetComponentInParent<playerManager>();

            if (target != null && target.IsSpawned)
            {
                target.ModifyHealth(-20);
            }
        }

        Suicide();
    }
}
