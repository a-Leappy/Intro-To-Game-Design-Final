using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public float jumpForce = 10f;
    public float playerSpeed = 7f;
    public float currentSpeed;
    public Rigidbody rb;
    public bool canJump;
    public int playerHealth = 100;
    public Transform playerCamera;    
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // Implement death logic here (e.g., respawn, game over screen)
    }
}
