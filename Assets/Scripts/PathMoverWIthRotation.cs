using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMoverWithRotation : MonoBehaviour
{
    [Tooltip("Použít body jako relativní (lokální) vùèi startovní pozici")]
    public bool useRelativePositions = true;

    public List<Vector3> points = new List<Vector3>();         // seznam pozic
    public List<Vector3> rotations = new List<Vector3>();      // odpovídající euler rotace (v stupních)

    public float moveSpeed = 2f;
    public float rotationSpeed = 180f; // stupnì za sekundu
    public float waitTime = 1f;
    public float stopDistance = 0.05f;

    private int currentIndex = 0;
    private bool isWaiting = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        if (points.Count > 0 && currentIndex < rotations.Count)
        {
            ApplyRotation(rotations[currentIndex]);
        }
    }

    void Update()
    {
        if (points.Count < 2 || isWaiting)
            return;

        Vector3 targetPos = useRelativePositions
            ? initialPosition + points[currentIndex]
            : points[currentIndex];

        Vector3 moveDir = (targetPos - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) < stopDistance)
        {
            transform.position = targetPos;
            StartCoroutine(WaitAndRotate());
        }
    }

    IEnumerator WaitAndRotate()
    {
        isWaiting = true;

        // Vyèkej než se dorotuje
        if (currentIndex < rotations.Count)
        {
            Quaternion targetRot = Quaternion.Euler(rotations[currentIndex]);
            while (Quaternion.Angle(transform.rotation, targetRot) > 0.5f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            transform.rotation = targetRot;
        }

        yield return new WaitForSeconds(waitTime);

        currentIndex = (currentIndex + 1) % points.Count;
        isWaiting = false;
    }

    void ApplyRotation(Vector3 euler)
    {
        transform.rotation = Quaternion.Euler(euler);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 basePos = Application.isPlaying ? initialPosition : transform.position;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 worldPoint = useRelativePositions ? basePos + points[i] : points[i];
            Gizmos.DrawCube(worldPoint, Vector3.one * 0.2f);

            if (i < points.Count - 1)
            {
                Vector3 next = useRelativePositions ? basePos + points[i + 1] : points[i + 1];
                Gizmos.DrawLine(worldPoint, next);
            }
        }
    }
}
