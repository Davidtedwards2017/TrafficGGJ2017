using UnityEngine;
using System.Collections;

public class ArcMeshView : MonoBehaviour
{

    public int numberOfSegments = 5;
    public float radius = 100;
    public float width = 20;
    public float arcAngle = 100;
    public float arcAngleMax = 180;
    public float gapAngle = 10;

    private ArcMeshController arc;

    public void Initialize()
    {
        arc = new ArcMeshController(GetComponent<MeshFilter>(), numberOfSegments, radius, width, arcAngle, arcAngleMax, gapAngle);
    }
	
	// Update is called once per frame
	void Update () {
        if (arc.radius != radius)       arc.radius = radius;
	    if (arc.width != width)         arc.width = width;
	    if (arc.arcAngle != arcAngle)   arc.arcAngle = arcAngle;
	    if (arc.gapAngle != gapAngle)   arc.gapAngle = gapAngle;
	}


}
