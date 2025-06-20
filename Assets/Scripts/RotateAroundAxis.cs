using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public Vector3 rotationDirection = new Vector3(0f, 0f, 0f); // stupn� za sekundu kolem X, Y, Z

    void Update()
    {
        // Spo��t�me rotaci okolo ka�d� osy zvl᚝ podle rychlosti a �asu
        float rotX = rotationDirection.x * Time.deltaTime;
        float rotY = rotationDirection.y * Time.deltaTime;
        float rotZ = rotationDirection.z * Time.deltaTime;

        // Vytvo��me Quaternion pro ka�dou osu
        Quaternion rotXQ = Quaternion.AngleAxis(rotX, Vector3.right);
        Quaternion rotYQ = Quaternion.AngleAxis(rotY, Vector3.up);
        Quaternion rotZQ = Quaternion.AngleAxis(rotZ, Vector3.forward);

        // Aplikujeme rotace (v po�ad� X -> Y -> Z)
        transform.rotation = rotZQ * rotYQ * rotXQ * transform.rotation;
    }
}
