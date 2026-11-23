
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class projectile : NetworkBehaviour
{
    public float projectileSpeed;
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
        if (!IsServer) return;

        Suicide();
    }
}
