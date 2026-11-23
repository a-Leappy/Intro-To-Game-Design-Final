
using UnityEngine;

public class cameraMouse : MonoBehaviour
{
    public float sensitivity = 1f;
    public bool canMove = false;

    private float xRotation;
    private float yRotation;
    private cameraFollow follow;

    void Start()
    {
        follow = GetComponent<cameraFollow>();

        Vector3 angles = transform.localEulerAngles;

        xRotation = Mathf.DeltaAngle(0f, angles.x);
        yRotation = angles.y;
    }


void Update()
{
    if (!canMove)
        return;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    float mouseX = Input.GetAxisRaw("Mouse X");
    float mouseY = Input.GetAxisRaw("Mouse Y");

    xRotation -= mouseY * sensitivity;
    yRotation += mouseX * sensitivity;

    xRotation = Mathf.Clamp(xRotation, -89f, 89f);
    yRotation = Mathf.Repeat(yRotation, 360f);
}


    void LateUpdate()
    {
        if (!canMove)
            return;

        transform.localRotation =
            Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
