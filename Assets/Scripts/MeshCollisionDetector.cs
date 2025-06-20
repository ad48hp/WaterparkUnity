using UnityEngine;

public class MeshCollisionDetector : MonoBehaviour
{
    public GameObject otherObject; // Objekt, se kterým detekujeme kolizi (nastav v Inspectoru)

    private MeshFilter thisMeshFilter;
    private MeshFilter otherMeshFilter;

    void Start()
    {
        thisMeshFilter = GetComponent<MeshFilter>();
        if (otherObject != null)
        {
            otherMeshFilter = otherObject.GetComponent<MeshFilter>();
            if (otherMeshFilter == null)
            {
                Debug.LogError("Other object nemá MeshFilter!");
            }
        }
    }

    void Update()
    {
        if (thisMeshFilter == null || otherMeshFilter == null)
            return;

        if (IsMeshIntersecting(thisMeshFilter, transform, otherMeshFilter, otherObject.transform))
        {
            Debug.Log("Meshy se pøekrývají!");
        }
    }

    // Kontrola kolize dvou meshù (AABB test)
    bool IsMeshIntersecting(MeshFilter mfA, Transform tfA, MeshFilter mfB, Transform tfB)
    {
        Bounds boundsA = GetWorldBounds(mfA.mesh.bounds, tfA);
        Bounds boundsB = GetWorldBounds(mfB.mesh.bounds, tfB);

        return boundsA.Intersects(boundsB);
    }

    // Pøevod lokálních bounds na svìtové souøadnice
    Bounds GetWorldBounds(Bounds localBounds, Transform t)
    {
        Vector3 center = t.TransformPoint(localBounds.center);

        // Vypoèítat rozsah podle mìøítka (scale)
        Vector3 extents = Vector3.Scale(localBounds.extents, t.lossyScale);

        Bounds worldBounds = new Bounds(center, extents * 2);
        return worldBounds;
    }
}
