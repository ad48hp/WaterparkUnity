using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMoverFlexible : MonoBehaviour
{
    [Tooltip("Pou��t body jako relativn� (lok�ln�) v��i startovn� pozici")]
    public bool useRelativePositions = true;

    [Tooltip("Seznam bod� (lok�ln� nebo glob�ln� podle nastaven�)")]
    public List<Vector3> points = new List<Vector3>();

    public float speed = 2f;
    public float waitTime = 1f;
    public float stopDistance = 0.05f;

    private int currentIndex = 0;
    private bool isWaiting = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (points.Count < 2 || isWaiting) return;

        Vector3 target = useRelativePositions
            ? initialPosition + points[currentIndex]
            : points[currentIndex];

        Vector3 moveDir = (target - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < stopDistance)
        {
            transform.position = target;
            StartCoroutine(WaitBeforeNextPoint());
        }
    }

    IEnumerator WaitBeforeNextPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentIndex = (currentIndex + 1) % points.Count;
        isWaiting = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 basePos = Application.isPlaying ? initialPosition : transform.position;

        for (int i = 0; i < points.Count; i++)
        {
            Vector3 worldPoint = useRelativePositions
                ? basePos + points[i]
                : points[i];

            Gizmos.DrawCube(worldPoint, Vector3.one * 0.2f);

            if (i < points.Count - 1)
            {
                Vector3 nextWorld = useRelativePositions
                    ? basePos + points[i + 1]
                    : points[i + 1];
                Gizmos.DrawLine(worldPoint, nextWorld);
            }
        }
    }
}
