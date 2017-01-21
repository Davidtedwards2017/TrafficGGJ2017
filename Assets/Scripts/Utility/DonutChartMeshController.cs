using UnityEngine;

public class DonutChartMeshController : MonoBehaviour
{
    DonutChartMesh mDonutChart;
    float[] mData;
    float delay = 0.1f;
    public Material mainMaterial;
    Material[] materials;
    public int segments;
    public Color[] colors;
    public float radiusInner;
    public float radiusOuter;
    public float[] radiusScale;
    //private int randomBuffer;

    void Start()
    {
        //randomBuffer = Random.seed;
        //Random.seed = 10;

        materials = new Material[segments];
        for (int i = 0; i < segments; i++)
        {
            materials[i] = new Material(mainMaterial);
            //materials[i].color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)255);
            //materials[i].SetColor("_EmissionColor", colors[i]);
            materials[i].SetColor("_TintColor", colors[i]);
            //Debug.LogError(materials[i].color);
        }

        mDonutChart = gameObject.AddComponent<DonutChartMesh>() as DonutChartMesh;
        if (mDonutChart != null)
        {
            mDonutChart.Init(mData, 100, 90, radiusOuter, radiusInner, radiusScale, materials, delay);
            mData = GenerateRandomValues(segments);
            mDonutChart.Draw(mData);
        }
        //Random.seed = randomBuffer;
    }
    //void Update ()
    //{
    //    for (int i = 0; i < segments; i++)
    //    {
    //        materials[i].SetColor("_TintColor", colors[i]);
    //    }
    //    mDonutChart.Init(mData, 100, 90, radiusOuter, radiusInner, radiusScale, materials, delay);
    //    mDonutChart.Draw(mData);
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown("a"))
    //    {
    //        randomBuffer = Random.seed;
    //        Random.seed = 10;
    //        mData = GenerateRandomValues(segments);
    //        mDonutChart.Draw(mData);
    //        Random.seed = randomBuffer;
    //    }
    //}

    float[] GenerateRandomValues(int length)
    {
        float[] targets = new float[length];

        for (int i = 0; i < length; i++)
        {
            //targets[i] = Random.Range(0f, 100f);
            targets[i] = 1f;
        }
        return targets;
    }
}