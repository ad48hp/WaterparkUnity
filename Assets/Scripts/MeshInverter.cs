using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshInverter : MonoBehaviour
{
    //public bool switchInverted = false;

    //private bool lastState = false;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null) return;

        // Duplikuj mesh (abychom nemìnili originál)
        Mesh original = mf.sharedMesh;
        Mesh mesh = Instantiate(original);
        mesh.name = original.name + "_InvertedCopy";

        // Invertuj winding order
        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int temp = triangles[i];
            triangles[i] = triangles[i + 1];
            triangles[i + 1] = temp;
        }
        mesh.triangles = triangles;

        // Invertuj normály
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        mf.sharedMesh = mesh;
    }
}
