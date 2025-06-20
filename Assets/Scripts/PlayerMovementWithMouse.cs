using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementWithMouse : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraPivot; // Odkaz na kameru (uvnit� hr��e)

    [Header("Obstacle Avoidance")]
    public float rayDistance = 1f;
    public LayerMask obstacleLayers;

    private Rigidbody rb;
    private Vector3 inputDirection;
    private float rotationY; // horizont�ln� rotace (Y)
    private float rotationX; // vertik�ln� rotace (X)

    public float jumpForce = 5f;
    public float groundCheckDistance = 0.1f;

    public bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.freezeRotation = true; // pokud chce� zak�zat rotaci rigidbody, odkomentuj

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // My� � horizont�ln� a vertik�ln� vstup
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f); // omezen� nahoru/dol�

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        // Pohyb - na�ten� vstupu
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDirection = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        if (inputDirection.magnitude > 0.1f)
        {
            Vector3 worldMoveDir = transform.TransformDirection(inputDirection);

            // Raycast dop�edu na kontrolu p�ek�ky
            if (!Physics.Raycast(transform.position, worldMoveDir, rayDistance))
            {
                // P�idat s�lu pokud nen� p�ek�ka a rychlost nen� p��li� velk�
                if (rb.linearVelocity.magnitude < moveSpeed)
                {
                    rb.AddForce(worldMoveDir * moveSpeed, ForceMode.Acceleration);
                }
            }
            else
            {
                // Zpomalit hr��e, kdy� je p�ek�ka bl�zko
                rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.5f, rb.linearVelocity.y, rb.linearVelocity.z * 0.5f);
                Debug.Log("Did!");
            }
        }
        else
        {
            // M�rn� zpomalit, kdy� hr�� nechod�
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y, rb.linearVelocity.z * 0.9f);
        }

        // Raycast dolů pro kontrolu, jestli je hráč na zemi
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

    }
}
