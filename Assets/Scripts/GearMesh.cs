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
    [ReadOnly] public float outerRadius;
    [ReadOnly] public float toothWidth;

    private static bool first = true;
    public Material gearMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (first) {
            print("CALLED ONCE");
            gearData = new Gear3D(center, radius, numTeeth, depth, rotationFactor);
            gearData.Generate();
            AddMesh();

            outerRadius = gearData.OuterRadius;
            toothWidth = gearData.ToothWidth;
            first = false;
        }
    }

    public void SetupGear(ref Gear3D prev)
    {
        Debug.Assert(prev != null);
        gearData = GearFromPrevious(this.transform.position, ref prev);
        gearData.Generate();
        AddMesh();

        center = gearData.Center;
        radius = gearData.Radius;
        numTeeth = gearData.GetNumTeeth();
        depth = gearData.Depth;
        rotationFactor = gearData.RotationFactor;
        outerRadius = gearData.OuterRadius;
        toothWidth = gearData.ToothWidth;
    }
    private void AddMesh()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        mesh.Clear();
        mesh.vertices = gearData.Vertices.ToArray();
        mesh.uv = gearData.Uv.ToArray();
        mesh.triangles = gearData.TriangleVertIndices.ToArray();

        //mesh.Optimize();
        //mesh.OptimizeIndexBuffers();
        ////mesh.OptimizeReorderVertexBuffer();
        ////mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //mesh.RecalculateTangents();
        meshRenderer.sharedMaterial = gearMaterial;
    }

    public Gear3D GearFromPrevious(Vector3 center, ref Gear3D previous)
    {
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
        gameObject.transform.Rotate(rotationFactor,0,0);
    }
}
