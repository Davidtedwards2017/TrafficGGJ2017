using UnityEngine;
using System.Collections;

public static class Polygon {
	
	// Creates convex polygon from given points
	public static Mesh Create (Vector3[] points)
	{
//		Vector2 rays = 
//		for (int i = 0; i < points.Length; i++)
//		{
//			
//		}
		Mesh mesh = new Mesh();
		Vector3[] verts = points;
		int[] tris = new int[(points.Length - 2) * 3 * 2];
		int index = 0;
		for (int i = 0; i < tris.Length / 6; i++)
		{
			tris[i * 6] = index;
			tris[i * 6 + 1] = index + 1;
			tris[i * 6 + 2] = verts.Length-1;
			tris[i * 6 + 3] = verts.Length-1;
			tris[i * 6 + 4] = index + 1;
			tris[i * 6 + 5] = index;
			index++;
		}
		mesh.vertices = verts;
		mesh.triangles = tris;
		return mesh;
	}

}
