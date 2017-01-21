using UnityEngine;
using System.Collections;

public class DonutChartMesh : MonoBehaviour
{

    float[] mData;

    int mSlices;
    float mRotationAngle;
    float mOuterRadius;
    float mInnerRadius;
    float[] mRadiusScale;
    Material[] mMaterials;

    Vector3[] mVertices;
    Vector3[] mNormals;
    Vector3 mNormal = new Vector3(0f, 0f, -1f);
    Vector2[] mUvs;
    int[] mTriangles;
    MeshRenderer mMeshRenderer;
    //float delay = 0.1f;

    public void Init(float[] data, int slices, float rotationAngle, float outerRadius, float innerRadius, float[] radiusScale, Material[] materials, float speed)
    {
        mData = data;
        mSlices = slices;
        mRotationAngle = rotationAngle;
        mOuterRadius = outerRadius;
        mInnerRadius = innerRadius;
        mRadiusScale = radiusScale;
        //delay = speed;

        // Get Mesh Renderer
        mMeshRenderer = gameObject.GetComponent("MeshRenderer") as MeshRenderer;
        if (mMeshRenderer == null)
        {
            gameObject.AddComponent<MeshRenderer>();
            mMeshRenderer = gameObject.GetComponent("MeshRenderer") as MeshRenderer;
        }

        mMeshRenderer.materials = materials;

        mMaterials = materials;


        //Init(data);
    }

    public void Init(float[] data)
    {
        mSlices = 100;
        mRotationAngle = 90f;
        mOuterRadius = 0.3f;
        mInnerRadius = 0.15f;

        mData = data;
    }

    public void Draw(float[] data)
    {
        mData = data;
        StopAllCoroutines();
        StartCoroutine(Draw());
    }

    public IEnumerator Draw()
    {

        //Check data validity for pie chart...
        while (mData == null)
        {
            print("PieChart: Data null");
            yield return null;
        }

        for (int i = 0; i < mData.Length; i++)
        {
            if (mData[i] < 0)
            {
                print("PieChart: Data < 0");
                yield return null;
            }
        }

        // Calculate sum of data values
        float sumOfData = 0;
        foreach (float value in mData)
        {
            sumOfData += value;
        }
        if (sumOfData <= 0)
        {
            print("PieChart: Data sum <= 0");
            yield return null;
        }
        // Determine how many triangles in slice
        int[] slice = new int[mData.Length];
        int numOfTris = 0;
        int numOfSlices = 0;
        int countedSlices = 0;

        // Caluclate slice size
        for (int i = 0; i < mData.Length; i++)
        {
            numOfTris = (int)((mData[i] / sumOfData) * mSlices);
            slice[numOfSlices++] = numOfTris;
            countedSlices += numOfTris;
        }
        // Check that all slices are counted.. if not -> add/sub to/from biggest slice..
        int idxOfLargestSlice = 0;
        int largestSliceCount = 0;
        for (int i = 0; i < mData.Length; i++)
        {
            if (largestSliceCount < slice[i])
            {
                idxOfLargestSlice = i;
                largestSliceCount = slice[i];
            }
        }

        // Check validity for pie chart
        if (countedSlices == 0)
        {
            print("PieChart: Slices == 0");
            yield return null;
        }

        // Adjust largest dataset to get proper slice
        slice[idxOfLargestSlice] += mSlices - countedSlices;

        // Check validity for pie chart data
        if (slice[idxOfLargestSlice] <= 0)
        {
            print("PieChart: Largest pie <= 0");
            yield return null;
        }

        // Init vertices and triangles arrays
        mVertices = new Vector3[mSlices * 6];
        mNormals = new Vector3[mSlices * 6];
        mUvs = new Vector2[mSlices * 6];
        mTriangles = new int[mSlices * 6];

        //gameObject.AddComponent("MeshFilter");
        //gameObject.AddComponent("MeshRenderer");

        Mesh mesh = ((MeshFilter)GetComponent("MeshFilter")).mesh;

        mesh.Clear();

        mesh.name = "Pie Chart Mesh";

        // Roration offset (to get star point to "12 o'clock")
        float rotOffset = mRotationAngle / 360f * 2f * Mathf.PI;

        // Calc the points in circle
        float angle;
        float[] x = new float[mSlices];
        float[] y = new float[mSlices];
        float[] z = new float[mSlices];
        float[] w = new float[mSlices];

        for (int i = 0; i < mSlices; i++)
        {
            angle = i * 2f * Mathf.PI / mSlices;
            x[i] = (Mathf.Cos(angle + rotOffset) * mOuterRadius);
            y[i] = (Mathf.Sin(angle + rotOffset) * mOuterRadius);
            z[i] = (Mathf.Cos(angle + rotOffset) * mInnerRadius);
            w[i] = (Mathf.Sin(angle + rotOffset) * mInnerRadius);
        }

        // Generate mesh with slices (vertices and triangles)
        for (int i = 0; i < mSlices; i++)
        {
            mVertices[i * 6 + 0] = new Vector3(z[i], w[i], 0f);
            mVertices[i * 6 + 1] = new Vector3(x[i], y[i], 0f);
            // This will ensure that last vertex = first vertex..
            mVertices[i * 6 + 2] = new Vector3(x[(i + 1) % mSlices], y[(i + 1) % mSlices], 0f);

            mVertices[i * 6 + 3] = new Vector3(x[(i + 1) % mSlices], y[(i + 1) % mSlices], 0f);
            mVertices[i * 6 + 4] = new Vector3(z[(i + 1) % mSlices], w[(i + 1) % mSlices], 0f);
            // This will ensure that last vertex = first vertex..
            mVertices[i * 6 + 5] = new Vector3(z[i], w[i], 0f);

            mNormals[i * 6 + 0] = mNormal;
            mNormals[i * 6 + 1] = mNormal;
            mNormals[i * 6 + 2] = mNormal;
            mNormals[i * 6 + 3] = mNormal;
            mNormals[i * 6 + 4] = mNormal;
            mNormals[i * 6 + 5] = mNormal;

            mUvs[i * 6 + 0] = new Vector2(0f, 0f);
            mUvs[i * 6 + 1] = new Vector2(x[i], y[i]);
            // This will ensure that last uv = first uv..
            mUvs[i * 6 + 2] = new Vector2(x[(i + 1) % mSlices], y[(i + 1) % mSlices]);

            mUvs[i * 6 + 3] = new Vector2(x[(i + 1) % mSlices], y[(i + 1) % mSlices]);
            mUvs[i * 6 + 4] = new Vector2(z[(i + 1) % mSlices], w[(i + 1) % mSlices]);
            // This will ensure that last uv = first uv..
            mUvs[i * 6 + 5] = new Vector2(z[i], w[i]);

            mTriangles[i * 6 + 0] = i * 6 + 0;
            mTriangles[i * 6 + 1] = i * 6 + 1;
            mTriangles[i * 6 + 2] = i * 6 + 2;
            mTriangles[i * 6 + 3] = i * 6 + 3;
            mTriangles[i * 6 + 4] = i * 6 + 4;
            mTriangles[i * 6 + 5] = i * 6 + 5;
        }


        // Assign verts, norms, uvs and tris to mesh and calc normals
        mesh.vertices = mVertices;
        mesh.normals = mNormals;
        mesh.uv = mUvs;
        //mesh.triangles = triangles;

        mesh.subMeshCount = mData.Length;

        int[][] subTris = new int[mData.Length][];

        countedSlices = 0;

        // Set sub meshes
        for (int i = 0; i < mData.Length; i++)
        {
            // Every triangle has three veritces..
            subTris[i] = new int[slice[i] * 6];

            // Add tris to subTris
            for (int j = 0; j < slice[i]; j++)
            {
                subTris[i][j * 6 + 0] = mTriangles[countedSlices * 6 + 0];
                subTris[i][j * 6 + 1] = mTriangles[countedSlices * 6 + 1];
                subTris[i][j * 6 + 2] = mTriangles[countedSlices * 6 + 2];
                subTris[i][j * 6 + 3] = mTriangles[countedSlices * 6 + 3];
                subTris[i][j * 6 + 4] = mTriangles[countedSlices * 6 + 4];
                subTris[i][j * 6 + 5] = mTriangles[countedSlices * 6 + 5];

                // scale verts
                for (int k = 0; k < 6; k++)
                {
                    mVertices[subTris[i][j * 6 + k]] *= mRadiusScale[i];
                }
                //if (j % 5 == 0)
                //    yield return new WaitForSeconds(delay);

                mesh.SetTriangles(subTris[i], i);
                countedSlices++;
            }

        }
        mesh.vertices = mVertices;
    }

    // Properties
    public float[] Data { get { return mData; } set { mData = value; } }

    public int Slices { get { return mSlices; } set { mSlices = value; } }

    public float RotationAngle { get { return mRotationAngle; } set { mRotationAngle = value; } }

    public float Radius { get { return mOuterRadius; } set { mOuterRadius = value; } }

    public Material[] Materials { get { return mMaterials; } set { mMaterials = value; } }

}