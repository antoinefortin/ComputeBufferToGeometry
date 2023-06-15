using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSphere : MonoBehaviour
{
    public ComputeShader computeShader;
    ComputeBuffer resultBuffer;
    Vector3[] resultArray = new Vector3[256 * 256];

    void Awake()
    {
        // Create the result buffer and bind it to the compute shader
        resultBuffer = new ComputeBuffer(256 * 256, sizeof(float) * 3);
        computeShader.SetBuffer(0, "Result", resultBuffer);

        // Execute the compute shader
        computeShader.Dispatch(0, 256 / 8, 256 / 8, 1);

        // Copy results from the GPU to the CPU
        resultBuffer.GetData(resultArray);

        // Create a new mesh
        Mesh mesh = new Mesh();

        // Set the vertices
        mesh.vertices = resultArray;

        // Generate the triangles
        int[] triangles = new int[(256 - 1) * (256 - 1) * 6];
        for (int ti = 0, vi = 0, y = 0; y < 256 - 1; y++, vi++)
        {
            for (int x = 0; x < 256 - 1; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + 256;
                triangles[ti + 5] = vi + 256 + 1;
            }
        }
        mesh.triangles = triangles;

        // Calculate normals
        mesh.RecalculateNormals();

        // Assign the mesh to a MeshFilter/MeshRenderer
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // Remember to clean up resources!
        resultBuffer.Dispose();
    }
}
