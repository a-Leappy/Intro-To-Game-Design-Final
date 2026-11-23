using UnityEngine;

public class cameraMouse : MonoBehaviour
{
    public float sensitivity = 1f;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.localEulerAngles;

        xRotation = Mathf.DeltaAngle(0f, angles.x);
        yRotation = angles.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        xRotation -= mouseY * sensitivity;
        yRotation += mouseX * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        yRotation = Mathf.Repeat(yRotation, 360f);
    }

    void LateUpdate()
    {
        transform.localRotation =
            Quaternion.Euler(xRotation, yRotation, 0f);
    }
}