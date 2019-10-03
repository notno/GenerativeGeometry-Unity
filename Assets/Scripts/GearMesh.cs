using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMesh : MonoBehaviour
{

    public Vector3 center = new Vector3();
    public float radius = 2f;
    public float toothWidth = .1f;
    public float depth;
    public int rotationFactor = 1;

    public Circle gearData;
    public Material gearMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //gearData = new Circle(center, radius, 16);
        //gearData = new Gear2D(center, radius, toothWidth, rotationFactor);
        gearData = new Gear3D(center, radius, toothWidth, depth, rotationFactor);

        SetupGear();
    }

    public void SetupGear()
    {
        gearData.GenerateVertsAndTris();
        AddMesh();
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

        mesh.RecalculateNormals();
        meshRenderer.sharedMaterial = gearMaterial;

        mesh.Optimize();
        //mesh.OptimizeIndexBuffers();
        ////mesh.OptimizeReorderVertexBuffer();
        ////mesh.RecalculateBounds();
        //mesh.RecalculateTangents();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(rotationFactor,0,0);
    }
}
