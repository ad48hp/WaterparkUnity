using UnityEngine;
using System.Collections;

public class RotateBetweenEulerPoints : MonoBehaviour
{
    public Vector3[] eulerPoints;       // Pole cílových rotací v eulerech (stupnì)
    public float rotationSpeed = 2f;    // Rychlost rotace (stupnì za sekundu)
    public float pauseDuration = 1f;    // Pauza mezi body (v sekundách)

    private int currentPointIndex = 0;

    void Start()
    {
        if (eulerPoints == null || eulerPoints.Length == 0)
        {
            Debug.LogWarning("Není nastaven žádný bod pro rotaci.");
            enabled = false;
            return;
        }

        transform.rotation = Quaternion.Euler(eulerPoints[0]);
        StartCoroutine(RotateRoutine());
    }

    IEnumerator RotateRoutine()
    {
        while (true)
        {
            int nextPointIndex = (currentPointIndex + 1) % eulerPoints.Length;
            Quaternion targetRotation = Quaternion.Euler(eulerPoints[nextPointIndex]);

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            transform.rotation = targetRotation;

            yield return new WaitForSeconds(pauseDuration);

            currentPointIndex = nextPointIndex;
        }
    }
}
