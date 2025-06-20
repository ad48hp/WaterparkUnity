using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFloat : MonoBehaviour
{
    public float floatForce = 10f;
    public float dragInWater = 2f;

    private Rigidbody rb;
    private bool isInWater = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
            rb.linearDamping = dragInWater;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            rb.linearDamping = 0f;
        }
    }

    void FixedUpdate()
    {
        if (isInWater)
        {
            Debug.Log("InWater");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }
}
