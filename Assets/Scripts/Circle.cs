using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Circle
{
    protected Circle(Vector3 center, float radius, int edges)
    {
        this.center = center;
        this.Radius = radius;
        this.numSpokes = edges;
    }

    protected const float DEFAULT_RADIUS = 100.0f;
    protected const int DEFAULT_nUMSPOKES = 16;

    public Vector3 Center { get => center; set => center = value; }
    private Vector3 center;
    public float Radius { get => radius; set => radius = value; }
    private float radius = DEFAULT_RADIUS;


    protected int numSpokes = DEFAULT_nUMSPOKES;

    protected List<int> triangleVertIndices;
    protected List<Vector3> vertices;
    protected List<Vector3> normals;


    public void Generate()
    {
        MakeTriangles();
    }

    public void MakeTriangles()
    {
        vertices.Add(center);
        normals.Add(new Vector3(1, 0, 0));

        // Iterate through all spokes (NumTeeth*2)
        for (int i = 1; i <= numSpokes; i++)
        {
            MakeVertices(i);
            MakeNormals();
            MakeTriangleVertexIndices(i);
        };
    }

    protected virtual void MakeTriangleVertexIndices(int i)
    {
        if (i < numSpokes)
        {
            // Make a face triangle 
            AddTri(i + 1, i, 0);
        }
        else if (i == numSpokes)
        {
            // Last triangle face, clockwise
            AddTri(1, i, 0);
        }
    }

    protected virtual void AddTri(int a, int b, int c)
    {
        triangleVertIndices.Add(a);
        triangleVertIndices.Add(b);
        triangleVertIndices.Add(c);
    }

    protected virtual void MakeNormals()
    {
        normals.Add(new Vector3(1, 0, 0));
    }

    protected virtual void MakeVertices(int i)
    {
        Vector3 c = center;
        float theta = GetThetaAtIthSpoke(i - 1);
        float cT = Mathf.Cos(theta);
        float sT = Mathf.Sin(theta);
        // Create vertices for front of circle
        vertices.Add(new Vector3(0 + c.x, Radius * cT + c.y, Radius * sT + c.z));
    }

    protected float GetEdgeWidthUnit()  {
		return 2.0f * Mathf.PI / numSpokes;
	}

    protected float GetThetaAtIthSpoke(int i) {
		return i* GetEdgeWidthUnit();
	}
}
