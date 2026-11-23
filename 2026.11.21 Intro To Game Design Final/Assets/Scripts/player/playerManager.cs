
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerManager : NetworkBehaviour
{
    public float jumpForce = 10f;
    public float playerSpeed = 7f;
    public float currentSpeed;
    public Rigidbody rb;
    public bool canJump;
    public int playerHealth = 100;
    public Transform playerCamera;
    public GameObject menu;

    void Start()
    {
        currentSpeed = playerSpeed;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        Camera cam = Camera.main;

        if (cam != null)
        {
            playerCamera = cam.transform;

            cameraFollow follow = cam.GetComponent<cameraFollow>();
            cameraMouse mouse = cam.GetComponent<cameraMouse>();

            if (follow != null)
                follow.player = transform;

            if (mouse != null)
                mouse.canMove = true;
        }
    }

    public void SetPlayerSpeed(float speed, int time)
    {
        currentSpeed = speed;
        Invoke("ResetPlayerSpeed", time);
    }

    void ResetPlayerSpeed()
    {
        Debug.Log("Resetting player speed to default");
        currentSpeed = playerSpeed;
    }

    public void ModifyHealth(int amount)
    {
        playerHealth += amount;

        if (playerHealth > 100)
        {
            playerHealth = 100;
        }

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died");
    }
}
