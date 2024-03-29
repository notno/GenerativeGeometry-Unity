﻿using UnityEngine;
using GenerativeGeometry;

public class Gear3D : Gear
{
    public float Depth { get => depth; set => depth = value; }
    float depth;

    // Copy constructor
    public Gear3D(Gear3D p) : this(p.Center, p.Radius, p.OuterRadius, p.GetNumTeeth(), p.ToothWidth, p.depth) { }

    public Gear3D(Vector3 center, float radius, float oR, int nT, float tW, float depth) :
        base(center, radius, oR, nT, tW)
    {
        this.Depth = depth;
        Debug.Assert(radius > 0);
        Debug.Assert(OuterRadius >= radius);
        Debug.Assert(ToothWidth > 0);
    }


    private Gear3D(Vector3 center, float radius, int numTeeth, float depth) :
        base(center, radius, numTeeth)
    {
        this.Depth = depth;
        Debug.Assert(radius > 0);
        Debug.Assert(OuterRadius >= radius);
        Debug.Assert(ToothWidth > 0);
    }

    public Gear3D(Vector3 center, float radius, float toothWidth, float depth) :
        base(center, radius, toothWidth)
    {
        this.Depth = depth;
    }

    protected override void MakeVerticesAndUV(int i)
    {
        float theta = GetThetaAtIthSpoke(i - 1);
        float cT = Mathf.Cos(theta);
        float sT = Mathf.Sin(theta);

        // Create vertices and uv for front of gear
        Vertices.Add(new Vector3(0, Radius * cT, Radius * sT));
        Uv.Add(new Vector2(0, Radius * sT));
        Vertices.Add(new Vector3(0, OuterRadius * cT, OuterRadius * sT));
        Uv.Add(new Vector2(0, OuterRadius * sT));

        // Create vertices and uv for back of gear
        Vertices.Add(new Vector3(Depth, OuterRadius * cT, OuterRadius * sT));
        Uv.Add(new Vector2(Depth, OuterRadius * sT));
        Vertices.Add(new Vector3(Depth, Radius * cT, Radius * sT));
        Uv.Add(new Vector2(Depth, Radius * sT));
    }

    protected override void MakeTriangleVertexIndices(int i)
    {
        // Storing indices of Vertices for easy use when making triangles
        // Using "even" for even numbered spokes, "odd" for odd spokes
        int even4 = 4 * i,
            even3 = even4 - 1,
            even2 = even4 - 2,
            even1 = even4 - 3,
            even8 = 4 * (i + 1),
            even7 = even8 - 1,
            even6 = even8 - 2,
            even5 = even8 - 3,
            odd1 = even1,
            odd2 = even4,
            odd3 = even5,
            odd4 = even8;

        if (i < numSpokes)
        {
            if ((i & 1) == 1)
            { // Gear tooth
              // Make gear face triangle for tooth
              // Neighbor's outer vertex, Outer vertex
              // Triangles all sharing vertex 0, the center point
                AddTri(even6, even2, 0);
                // Make 2 tris for outer face of tooth
                AddTri(even6, even7, even3);
                AddTri(even3, even2, even6);
                // Make 2 tris for cw side of tooth
                AddTri(even1, even2, even4);
                AddTri(even2, even3, even4);
                // Make 2 tris for ccw side of tooth
                AddTri(even8, even7, even6);
                AddTri(even5, even8, even6);
            }
            else // Gap between gear teeth
            {
                // Make gear face triangle
                AddTri(0, odd3, odd1);
                // Make 2 tris for outer face of gap
                AddTri(odd3, odd4, odd1);
                AddTri(odd1, odd4, odd2);
            }
        }
        else if (i == numSpokes)
        {
            // Last triangle face, clockwise, a gap
            AddTri(0, 1, odd1);
            // Make 2 tris for outer face of gap
            AddTri(1, 4, odd1);
            AddTri(odd1, 4, odd2);
        }
    }
}