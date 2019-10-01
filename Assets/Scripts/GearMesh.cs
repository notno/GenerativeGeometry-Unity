using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMesh : MonoBehaviour
{
    public Gear3D gearData;
    [ReadOnly] public Vector3 center = new Vector3();
    public float radius = 10f;
    public int numTeeth = 16;
    public float depth = 2f;
    public int rotationFactor = 1;

    private static bool first = true;

    [ReadOnly] public float outerRadius;
    [ReadOnly] public float toothWidth;

    // Start is called before the first frame update
    void Start()
    {
        if (first) {
            print("CALLED ONCE");
            gearData = new Gear3D(center, radius, numTeeth, depth, rotationFactor);
            outerRadius = gearData.OuterRadius;
            toothWidth = gearData.ToothWidth;
            first = false;
        }
    }

    public void SetupGear(ref Gear3D prev)
    {
        Debug.Assert(prev != null);
        Vector3 p = this.transform.position;
        print(string.Format("prev: {0}", prev.OuterRadius.ToString()));
        gearData = GearFromPrevious(p, ref prev);
        //gearData.Generate();

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        // make changes to the Mesh by creating arrays which contain the new values
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };



        center = gearData.Center;
        radius = gearData.Radius;
        numTeeth = gearData.GetNumTeeth();
        depth = gearData.Depth;
        rotationFactor = gearData.RotationFactor;
        outerRadius = gearData.OuterRadius;
        toothWidth = gearData.ToothWidth;
    }

    public Gear3D GearFromPrevious(Vector3 center, ref Gear3D previous)
    {
        Debug.Log(string.Format("previous: {0}", previous.OuterRadius.ToString()));
        float distanceFromPrevious = GenerativeGeometry.Math.Distance(center, previous.Center);
        Debug.Assert(distanceFromPrevious >= previous.Radius);
        float outerRadius = distanceFromPrevious - previous.Radius;
        float toothWidth = previous.ToothWidth; // Copy tooth width from last Gear
        float radius = outerRadius - toothWidth;
        float diameter = 2.0f * Mathf.PI * radius;
        int numSpokes = (int)Mathf.Floor(diameter / toothWidth);
        radius = numSpokes * toothWidth / (2.0f * Mathf.PI);
        outerRadius = radius + toothWidth;
        int numTeeth = numSpokes / 2;
        int rotationFactor = -previous.RotationFactor;
        return new Gear3D(center, radius, outerRadius, numTeeth, toothWidth, rotationFactor, previous.Depth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
