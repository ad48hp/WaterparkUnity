using UnityEngine;

public class MeshCollisionDetector : MonoBehaviour
{
    public GameObject otherObject; // Objekt, se kter�m detekujeme kolizi (nastav v Inspectoru)

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
                Debug.LogError("Other object nem� MeshFilter!");
            }
        }
    }

    void Update()
    {
        if (thisMeshFilter == null || otherMeshFilter == null)
            return;

        if (IsMeshIntersecting(thisMeshFilter, transform, otherMeshFilter, otherObject.transform))
        {
            Debug.Log("Meshy se p�ekr�vaj�!");
        }
    }

    // Kontrola kolize dvou mesh� (AABB test)
    bool IsMeshIntersecting(MeshFilter mfA, Transform tfA, MeshFilter mfB, Transform tfB)
    {
        Bounds boundsA = GetWorldBounds(mfA.mesh.bounds, tfA);
        Bounds boundsB = GetWorldBounds(mfB.mesh.bounds, tfB);

        return boundsA.Intersects(boundsB);
    }

    // P�evod lok�ln�ch bounds na sv�tov� sou�adnice
    Bounds GetWorldBounds(Bounds localBounds, Transform t)
    {
        Vector3 center = t.TransformPoint(localBounds.center);

        // Vypo��tat rozsah podle m���tka (scale)
        Vector3 extents = Vector3.Scale(localBounds.extents, t.lossyScale);

        Bounds worldBounds = new Bounds(center, extents * 2);
        return worldBounds;
    }
}
