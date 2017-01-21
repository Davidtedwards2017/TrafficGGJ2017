using System;
using UnityEngine;
using System.Collections;

public class ArcMeshController
{

    private float _radius;

    public float radius
    {
        get { return _radius; }
        set
        {
            _radius = value;
            SetRadiusOrWidth();
        }
    }

    private float _width;

    public float width
    {
        get { return _width; }
        set
        {
            _width = value;
            SetRadiusOrWidth();
        }
    }

    private float _arcAngle;

    public float arcAngle
    {
        get { return _arcAngle; }
        set
        {
            _arcAngle = value;
            SetAngles();
        }
    }

    private float _arcAngleMax;

    public float arcAngleMax
    {
        get { return _arcAngleMax; }
        set
        {
            _arcAngleMax = value;
            SetAngles();
        }
    }

    private float _gapAngle;

    public float gapAngle
    {
        get { return _gapAngle; }
        set
        {
            _gapAngle = value;
            SetAngles();
        }
    }

    private readonly ArcMeshModel arc;

    public ArcMeshController(MeshFilter meshFilter, int numberOfSegments = 5, float newRadius = 20f, float newWidth = 2f, float newArcAngle = 100f, float newArcAngleMax = 180f, float newGapAngle = 10f)
    {
        _radius = newRadius;
        _width = newWidth;
        _arcAngle = newArcAngle;
        _gapAngle = newGapAngle;
        _arcAngleMax = newArcAngleMax;

        arc = new ArcMeshModel(numberOfSegments);
        SetAngles();

        meshFilter.mesh = arc.mesh;
    }

    //public void ArcMeshControllerTest(int numberOfSegments = 5, float newRadius = 100f, float newWidth = 20f, float newArcAngle = 180f, float newGapAngle = 10f)
    //{
    //    _radius = newRadius;
    //    _width = newWidth;
    //    _arcAngle = newArcAngle;
    //    _gapAngle = newGapAngle;

    //    arc = new ArcMeshModel(numberOfSegments);
    //    SetAngles();
    //    meshFilter.mesh = arc.mesh;
    //}

    private void SetRadiusOrWidth()
    {
        Vector3[] verts = new Vector3[arc.mesh.vertexCount];
        Vector2[] uvs = new Vector2[arc.mesh.vertexCount];

        for (int i = 0; i < 0.5f * arc.mesh.vertexCount; i++)
        {
            verts[2 * i] = arc.mesh.vertices[2 * i].normalized * (_radius + 0.5f * _width);
            verts[2 * i + 1] = arc.mesh.vertices[2 * i + 1].normalized * (_radius - 0.5f * _width);

            uvs[2 * i + 0] = new Vector2(verts[i * 2 + 0].x, verts[i * 2 + 0].z);
            uvs[2 * i + 1] = new Vector2(verts[i * 2 + 1].x, verts[i * 2 + 1].z);
        }

        arc.mesh.vertices = verts;
        arc.mesh.uv = uvs;
    }

    private void SetAngles()
    {
        Vector3[] verts = new Vector3[arc.mesh.vertexCount];
        Vector2[] uvs = new Vector2[arc.mesh.vertexCount];

        float subAngle = (_arcAngleMax - _gapAngle * (arc.numberOfSegments - 1)) * Mathf.Deg2Rad;

        int counter = 0;
        float angle = 0;
        for (int i = 0; i < arc.numberOfSegments; i++)
        {
            for (int j = 0; j < ArcMeshModel.numPieces/ arc.numberOfSegments; j++)
            {
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                float z = Mathf.Cos(angle);
                float w = Mathf.Sin(angle);

                verts[counter + 1] = new Vector3(z, 0f, w) * (_radius - 0.5f * _width);
                verts[counter+ 0] = new Vector3(x, 0f, y) * (_radius + 0.5f * _width);

                uvs[counter + 0] = new Vector2(verts[counter + 0].x, verts[counter + 0].z);
                uvs[counter + 1] = new Vector2(verts[counter + 1].x, verts[counter + 1].z);

                counter += 2;

                if (angle < _arcAngle * Mathf.Deg2Rad) angle += subAngle / ArcMeshModel.numPieces;
            }
            if (angle < _arcAngle * Mathf.Deg2Rad) angle += _gapAngle * Mathf.Deg2Rad;
        }

        arc.mesh.vertices = verts;
        arc.mesh.uv = uvs;
    }

}
