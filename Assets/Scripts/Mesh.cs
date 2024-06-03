using UnityEngine;
using System.Collections;

public class Mesh : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            UnityEngine.Mesh mesh = meshFilter.sharedMesh;
            if (mesh != null)
            {
                if (mesh.isReadable)
                {
                    Debug.Log("Mesh is readable.");
                }
                else
                {
                    Debug.LogWarning("Mesh is not readable. Make sure 'Read/Write Enabled' is checked in the import settings.");
                }
            }
            else
            {
                Debug.LogError("MeshFilter has no sharedMesh assigned.");
            }
        }
        else
        {
            Debug.LogError("No MeshFilter component found on this GameObject.");
        }
    }
}
