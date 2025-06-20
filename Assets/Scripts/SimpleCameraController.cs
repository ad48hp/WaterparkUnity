using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    public float moveSpeed = 5.0f;
    public bool lockCursor = true;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector3 rot = transform.eulerAngles;
        rotationX = rot.y;
        rotationY = rot.x;
    }

    void Update()
    {
        // Pohyb myši
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);

        // Pohyb klávesami
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(dir) * moveSpeed * Time.deltaTime;
        transform.position += move;
    }
}
