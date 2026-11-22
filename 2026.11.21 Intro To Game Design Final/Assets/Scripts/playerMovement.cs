
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public Rigidbody rb;
    public bool canJump;
    public List<GameObject> raycastHitGameObjects;
    public GameObject menu;
    public Transform playerCamera;

    private float horizontalInput;
    private float verticalInput;
    private float currentSpeed;
    private bool jumpRequested;
    private Vector3 previousPosition;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        canJump = true;
        currentSpeed = 7f;

        if (raycastHitGameObjects == null)
        {
            raycastHitGameObjects = new List<GameObject>();
        }

        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down);

        foreach (RaycastHit h in hits)
        {
            raycastHitGameObjects.Add(h.collider.gameObject);
            Debug.Log(h.distance);

            if (h.distance < 1)
            {
                canJump = true;
            }
        }

        previousPosition = rb.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = 4f;
        }
        else
        {
            currentSpeed = 7f;
        }

        // Read WASD input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Get camera direction
        if (playerCamera != null)
        {
            Vector3 forward = playerCamera.forward;
            Vector3 right = playerCamera.right;

            // Keep movement horizontal
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            // Calculate movement relative to camera
            Vector3 direction =
                forward * verticalInput +
                right * horizontalInput;

            // Prevent diagonal movement from being faster
            direction = Vector3.ClampMagnitude(direction, 1f);

            // Apply movement
            rb.velocity = new Vector3(
                direction.x * currentSpeed,
                rb.velocity.y,
                direction.z * currentSpeed
            );
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumpRequested = true;
            canJump = false;
        }

        // Menu detection
        if (menu != null && playerCamera != null)
        {
            Vector3 directionToMenuVec =
                menu.transform.position - playerCamera.position;

            directionToMenuVec.Normalize();

            float dot = Vector3.Dot(
                directionToMenuVec,
                playerCamera.forward
            );

            if (dot > 0.95f)
            {
                Raycast();
                Debug.Log(dot);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply jump
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }

        Vector3 displacement = rb.position - previousPosition;
        displacement.y = 0;

        float realSpeed = displacement.magnitude / Time.fixedDeltaTime;

        Debug.Log("Real Horizontal Speed: " + realSpeed);

        previousPosition = rb.position;
    }
    public void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                canJump = true;
                break;
            }
        }
    }

    public void Raycast()
    {
        if (playerCamera != null)
        {
            Debug.DrawRay(
                playerCamera.position,
                playerCamera.forward * 10,
                Color.blue,
                10f
            );
        }
    }
}
