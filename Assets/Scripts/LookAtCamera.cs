using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    [Tooltip("Dodateèný rotaèní offset ve stupních")]
    public Vector3 rotationOffset = Vector3.zero;

    void Update()
    {
        if (Camera.main == null)
            return;

        Vector3 direction = Camera.main.transform.position - transform.position;

        if (direction == Vector3.zero)
            return;

        // Vytvoøení základní rotace smìrem ke kameøe
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Pøidání rotaèního offsetu
        Vector3 targetEuler = targetRotation.eulerAngles + rotationOffset;

        // Zachování rotace podle povolených os
        Vector3 currentEuler = transform.rotation.eulerAngles;

        float x = rotateX ? targetEuler.x : currentEuler.x;
        float y = rotateY ? targetEuler.y : currentEuler.y;
        float z = rotateZ ? targetEuler.z : currentEuler.z;

        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
