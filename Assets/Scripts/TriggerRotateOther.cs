using UnityEngine;

public class TriggerRotateOther : MonoBehaviour
{
    [Tooltip("Objekt, jeho� rotace se m� zm�nit")]
    public Transform targetObject;

    [Tooltip("C�lov� rotace (Euler �hly ve stupn�ch)")]
    public Vector3 targetEulerAngles = new Vector3(0, 180, 0);

    [Tooltip("Rychlost rotace ve stupn�ch za sekundu")]
    public float rotationSpeed = 90f;

    [Tooltip("Tag hr��e")]
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
                rotate = false; // nebo true, pokud m� b�t trval� efekt
            }
        }
    }
}
