using UnityEngine;
using GenerativeGeometry;

public class Gear2D : Gear
{

    public Gear2D(Vector3 center, float radius, int numTeeth) :
        base(center, radius, numTeeth) { }


    public Gear2D(Vector3 center, float radius, int numTeeth, int rotation ) :
        base(center, radius, numTeeth)
    {
        RotationFactor = rotation;
        Debug.Assert(radius > 0);
        Debug.Assert(OuterRadius >= radius);
    }


    public Gear2D(Vector3 center, float radius, float toothWidth, int rotation) :
        base(center, radius, toothWidth)
    {
        RotationFactor = rotation;
        Debug.Assert(radius > 0);
        Debug.Assert(OuterRadius >= radius);
        Debug.Assert(ToothWidth > 0);
    }


    protected override void MakeVerticesAndUV(int i)
	{
        float theta = GetThetaAtIthSpoke(i - 1);
        float cT = Mathf.Cos(theta);
        float sT = Mathf.Sin(theta);

        // Create vertices for front of gear
        Vertices.Add(new Vector3(0, Radius* cT, Radius * sT));
        Uv.Add(new Vector2(0, Radius * sT));

        Vertices.Add(new Vector3(0, OuterRadius * cT, OuterRadius * sT));
        Uv.Add(new Vector2(0, Radius * sT));

    }

    protected override void MakeTriangleVertexIndices(int i) 
	{
		if (i < numSpokes) {
			if ((i & 1) == 1) { // Gear tooth
				// Make gear face triangle for tooth
				AddTri(2*(i+1)-1, 2* i-1, 0); 
			}
			else // Gap between gear teeth
			{
				// Make gear face triangle
				AddTri(0, 2*(i+1), 2* i);
			}
		}
		else if (i == numSpokes) {
			// Last triangle face, clockwise, a gap
			AddTri(0, 2, 2*i);
		}
	}

}

