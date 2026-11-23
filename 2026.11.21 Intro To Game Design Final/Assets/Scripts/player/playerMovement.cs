
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerMovement : NetworkBehaviour
{
    public List<GameObject> raycastHitGameObjects;
    private playerManager playerManager;
    private float horizontalInput;
    private float verticalInput;
    private bool jumpRequested;
    private Vector3 previousPosition;
    
    void Start()
    {
        playerManager = this.GetComponent<playerManager>();
        playerManager.currentSpeed = 7f;

        if (raycastHitGameObjects == null)
        {
            raycastHitGameObjects = new List<GameObject>();
        }

        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down);

        foreach (RaycastHit h in hits)
        {
            raycastHitGameObjects.Add(h.collider.gameObject);

            if (h.distance < 1)
            {
                playerManager.canJump = true;
            }
        }

        previousPosition = playerManager.rb.position;
    }

    void Update()
{
    if (!IsSpawned || !IsOwner) return;
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    if (Input.GetKeyDown(KeyCode.Space) && playerManager.canJump)
    {
        jumpRequested = true;
        playerManager.canJump = false;
    }

    if (playerManager.menu != null && playerManager.playerCamera != null)
    {
        Vector3 directionToMenuVec = playerManager.menu.transform.position - playerManager.playerCamera.position;
        directionToMenuVec.Normalize();

        float dot = Vector3.Dot(directionToMenuVec, playerManager.playerCamera.forward);

        if (dot > 0.95f)
        {
            Raycast();
        }
    }
}
    void FixedUpdate()
    {
        if (playerManager.playerCamera != null)
        {
            Vector3 forward = playerManager.playerCamera.forward;
            Vector3 right = playerManager.playerCamera.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * verticalInput + right * horizontalInput;
            direction = Vector3.ClampMagnitude(direction, 1f);

            playerManager.rb.velocity = new Vector3(
                direction.x * playerManager.currentSpeed,
                playerManager.rb.velocity.y,
                direction.z * playerManager.currentSpeed
            );
        }

        if (jumpRequested)
        {
            playerManager.rb.AddForce(Vector3.up * playerManager.jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }

        Vector3 displacement = playerManager.rb.position - previousPosition;
        displacement.y = 0;

        float realSpeed = displacement.magnitude / Time.fixedDeltaTime;

        previousPosition = playerManager.rb.position;
    }
    
    void LateUpdate()
    {
        if (!IsSpawned || !IsOwner) return;
        if (playerManager.playerCamera == null) return;

        float cameraYaw = playerManager.playerCamera.eulerAngles.y;

        transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
    }
    public void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                playerManager.canJump = true;
                break;
            }
        }
    }

    public void Raycast()
    {
        if (playerManager.playerCamera != null)
        {
            Debug.DrawRay(
                playerManager.playerCamera.position,
                playerManager.playerCamera.forward * 10,
                Color.blue,
                10f
            );
        }
    }
}
