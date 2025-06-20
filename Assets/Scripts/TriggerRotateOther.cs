using UnityEngine;

public class TriggerRotateOther : MonoBehaviour
{
    [Tooltip("Objekt, jehož rotace se má zmìnit")]
    public Transform targetObject;

    [Tooltip("Cílová rotace (Euler úhly ve stupních)")]
    public Vector3 targetEulerAngles = new Vector3(0, 180, 0);

    [Tooltip("Rychlost rotace ve stupních za sekundu")]
    public float rotationSpeed = 90f;

    [Tooltip("Tag hráèe")]
    public string playerTag = "Player";

    public bool rotate = false;
    private Quaternion targetRotation;

    void Start()
    {
        if (targetObject != null)
        {
            targetRotation = Quaternion.Euler(targetEulerAngles);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Dotk!");
            rotate = true;
        }
    }

    void Update()
    {
        if (rotate && targetObject != null)
        {
            targetObject.rotation = Quaternion.RotateTowards(
                targetObject.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(targetObject.rotation, targetRotation) < 0.1f)
            {
                targetObject.rotation = targetRotation;
                rotate = false; // nebo true, pokud má být trvalý efekt
            }
        }
    }
}
