using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Circle
{
    public Circle(Vector3 center, float radius, int edges)
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

    public List<Vector3> Vertices { get => vertices; protected set => vertices = value; }
    private List<Vector3> vertices = new List<Vector3>();
    public List<Vector2> Uv { get => uv; protected set => uv = value; }
    private List<Vector2> uv = new List<Vector2>();
    public List<int> TriangleVertIndices { get => triangleVertIndices; protected set => triangleVertIndices = value; }
    private List<int> triangleVertIndices = new List<int>();


    public void GenerateVertsAndTris()
    {
        MakeTriangles();
    }

    public void MakeTriangles()
    {
        Vertices.Add(new Vector3(0,0,0));
        Uv.Add(new Vector2(0,0));

        // Iterate through all spokes (NumTeeth*2)
        for (int i = 1; i <= numSpokes; i++)
        {
            MakeVerticesAndUV(i);
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
        TriangleVertIndices.Add(a);
        TriangleVertIndices.Add(b);
        TriangleVertIndices.Add(c);
    }

    protected virtual void MakeVerticesAndUV(int i)
    {
        Vector3 c = center;
        float theta = GetThetaAtIthSpoke(i - 1);
        float cT = Mathf.Cos(theta);
        float sT = Mathf.Sin(theta);
        // Create vertices for front of circle
        Vertices.Add(new Vector3(0 + c.x, Radius * cT + c.y, Radius * sT + c.z));
        Uv.Add(new Vector2(c.x, Radius * sT + c.z));
    }

    protected float GetEdgeWidthUnit()  {
		return 2.0f * Mathf.PI / numSpokes;
	}

    protected float GetThetaAtIthSpoke(int i) {
		return i* GetEdgeWidthUnit();
	}
}
