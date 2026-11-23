using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class abilitiesHandler : NetworkBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    private playerManager playerManager;
    private float abilityECooldown = 0f;
    private float abilityQCooldown = 0f;
    private float abilityShiftCooldown = 0f;
    private int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading = false;
    private bool fireRequested = false;
    void Start()
    {   
        currentAmmo = maxAmmo;
        playerManager = this.GetComponent<playerManager>();
    }

    void Update()
    {
        if (!IsSpawned || !IsOwner) return;
        DecrementCooldowns();
        if (!isReloading)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo > 0)
                {
                    fireRequested = true;
                    currentAmmo--;
                }
                else
                {
                    ReloadAmmo();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                // Implement secondary fire logic here
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && abilityShiftCooldown <= 0)
            {
                ShiftAbility();
            }
            if (Input.GetKeyDown(KeyCode.Q) && abilityQCooldown <= 0)
            {
                // Implement ability 2 logic here
                abilityQCooldown = 10f; // Reset cooldown for Q ability
            }
            if (Input.GetKeyDown(KeyCode.E) && abilityECooldown <= 0)
            {
                EAbility();
            }
            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
            {
                ReloadAmmo();
            }
        }
    }
    void LateUpdate()
    {
        if (!IsSpawned || !IsOwner) return;

        if (fireRequested)
        {
            fireRequested = false;
            PrimaryFire();
        }
    }
    void DecrementCooldowns()
    {
        if (abilityECooldown > 0)
        {
            abilityECooldown -= Time.deltaTime;
        }
        if (abilityQCooldown > 0)
        {
            abilityQCooldown -= Time.deltaTime;
        }
        if (abilityShiftCooldown > 0)
        {
            abilityShiftCooldown -= Time.deltaTime;
        }
    }

    void PrimaryFire()
    {
        if (playerManager.playerCamera == null) return;

        Transform cam = playerManager.playerCamera;
        Vector3 direction = cam.forward;
        Vector3 spawnPosition = cam.position + direction * 1f;

        PrimaryFireServerRpc(spawnPosition, direction);
    }

    [ServerRpc]
    void PrimaryFireServerRpc(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject g = Instantiate(
            projectile,
            spawnPosition,
            Quaternion.LookRotation(direction)
        );

        NetworkObject networkObject = g.GetComponent<NetworkObject>();
        Rigidbody rb = g.GetComponent<Rigidbody>();

        if (networkObject == null || rb == null)
        {
            Debug.LogError("Projectile missing NetworkObject or Rigidbody!");
            Destroy(g);
            return;
        }

        // Prevent collision with the player firing
        Collider[] projectileColliders = g.GetComponentsInChildren<Collider>();
        Collider[] playerColliders = GetComponentsInChildren<Collider>();

        foreach (Collider projectileCollider in projectileColliders)
        {
            foreach (Collider playerCollider in playerColliders)
            {
                Physics.IgnoreCollision(projectileCollider, playerCollider);
            }
        }

        direction.Normalize();

        rb.isKinematic = false;
        rb.velocity = direction * projectileSpeed;

        Debug.Log("Projectile velocity: " + rb.velocity);

        networkObject.Spawn();
    }
    void ReloadAmmo()
    {
        isReloading = true;
        playerManager.SetPlayerSpeed(1, 2);
        Invoke("SetAmmo", 2f); // Simulate a reload time of 2 seconds
    }

    void SetAmmo()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reloaded ammo");
    }
    void ShiftAbility()
    {
        playerManager.SetPlayerSpeed(14f, 5); // Example: Increase speed for 5 seconds
        abilityShiftCooldown = 10f; // Reset cooldown for shift ability
    }
    void EAbility()
    {
        playerManager.SetPlayerSpeed(1, 1); // Example: Heal player by 20 health points
        Invoke("EAbilityFunction", 1f); // Delay the effect of E ability by 1 second
    }
    void EAbilityFunction()
    {
        playerManager.ModifyHealth(20); // Example: Heal player by 20 health points
        abilityECooldown = 15f; // Reset cooldown for E ability
    }
}