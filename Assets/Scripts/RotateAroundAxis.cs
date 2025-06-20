using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public Vector3 rotationDirection = new Vector3(0f, 0f, 0f); // stupnì za sekundu kolem X, Y, Z

    void Update()
    {
        // Spoèítáme rotaci okolo každé osy zvláš podle rychlosti a èasu
        float rotX = rotationDirection.x * Time.deltaTime;
        float rotY = rotationDirection.y * Time.deltaTime;
        float rotZ = rotationDirection.z * Time.deltaTime;

        // Vytvoøíme Quaternion pro každou osu
        Quaternion rotXQ = Quaternion.AngleAxis(rotX, Vector3.right);
        Quaternion rotYQ = Quaternion.AngleAxis(rotY, Vector3.up);
        Quaternion rotZQ = Quaternion.AngleAxis(rotZ, Vector3.forward);

        // Aplikujeme rotace (v poøadí X -> Y -> Z)
        transform.rotation = rotZQ * rotYQ * rotXQ * transform.rotation;
    }
}
