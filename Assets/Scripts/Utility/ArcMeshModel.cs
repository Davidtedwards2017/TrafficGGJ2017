using UnityEngine;
using System.Collections;

public class ArcMeshModel
{
    public const int numPieces = 120;
    public readonly int numberOfSegments;
    public Mesh mesh { get; private set; }

    public ArcMeshModel(int _numberOfSegments)
    {
        //HACK division by zero error
        if (_numberOfSegments == 0) _numberOfSegments = 1;
        numberOfSegments = _numberOfSegments;
        Generate();
    }

    void Generate()
    {
        Vector3[] verts = new Vector3[numPieces * 2];
        int[] tris = new int[numPieces * 6 - 6*numberOfSegments];
        Vector3[] norms = new Vector3[numPieces * 2];
        Vector2[] uvs = new Vector2[numPieces * 2];

        for (int i = 0; i < numPieces; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int index = i * 2 + j;
                verts[index] = Vector3.zero;
                norms[index] = new Vector3(0f, 1f, 0f);
                uvs[index] = Vector2.zero;
            }
        }
        int subSegments = numPieces/numberOfSegments;
        int triCounter = 0;
        int vertCounter = 0;
        for (int i = 0; i < numberOfSegments; i++)
        {
            for (int j = 1; j < subSegments; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    tris[triCounter] = vertCounter + k;
                    tris[triCounter + 3] = vertCounter + 3 - k;
                    triCounter++;
                }
                vertCounter += 2;
                triCounter += 3;
            }
            vertCounter += 2;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.normals = norms;
        mesh.uv = uvs;

        mesh.triangles = tris;
        //mesh.subMeshCount = numberOfSegments;

        //int[][] subTris = new int[numberOfSegments][];

        //int countedSlices = 0;

        //// Set sub meshes
        //for (int i = 0; i < numberOfSegments; i++)
        //{
        //    subTris[i] = new int[tris.Length / numberOfSegments];

        //    // Add tris to subTris
        //    for (int j = 0; j < subTris[i].Length; j++)
        //    {
        //        subTris[i][j] = tris[countedSlices];
        //        countedSlices++;

        //        mesh.SetTriangles(subTris[i], i);
        //    }

        //}

        SetMesh(mesh);
    }

    public void SetMesh(Mesh newMesh)
    {
        mesh = newMesh;
    }


}
