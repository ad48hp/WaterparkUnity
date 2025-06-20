using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    [Tooltip("Dodate�n� rota�n� offset ve stupn�ch")]
    public Vector3 rotationOffset = Vector3.zero;

    void Update()
    {
        if (Camera.main == null)
            return;

        Vector3 direction = Camera.main.transform.position - transform.position;

        if (direction == Vector3.zero)
            return;

        // Vytvo�en� z�kladn� rotace sm�rem ke kame�e
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // P�id�n� rota�n�ho offsetu
        Vector3 targetEuler = targetRotation.eulerAngles + rotationOffset;

        // Zachov�n� rotace podle povolen�ch os
        Vector3 currentEuler = transform.rotation.eulerAngles;

        float x = rotateX ? targetEuler.x : currentEuler.x;
        float y = rotateY ? targetEuler.y : currentEuler.y;
        float z = rotateZ ? targetEuler.z : currentEuler.z;

        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
