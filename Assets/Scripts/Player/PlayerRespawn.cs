using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform checkpoint;

    void Start()
    {
        Respawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
    }

    public void Respawn()
    {
        if (checkpoint != null)
        {
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;

            // reset velocity pokud m� Rigidbody
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb) rb.linearVelocity = Vector3.zero;
        }
    }
}
