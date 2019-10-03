using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GearChain : MonoBehaviour
{
    public GearMesh proto;
    public int NumToGenerate = 10;

    public Vector3 center = new Vector3(0,0,0);
    public float radius = 1f;
    public float toothWidth = .1f;
    public float depth = .3f;

    List<(Vector3 Center, float Radius, float ToothWidth, int Rotation)> gearSystemData;
    public List<GearMesh> gearSystem;

    void Start()
    {
        //InvokeRepeating("SpawnGear", 2.0f, 0.3f);
    }

    public void GenerateGearSystem()
    {
        // Reinitialize data list
        gearSystemData = new List<(Vector3 Center, float Radius, float ToothWidth, int Rotation)>();
        gearSystem = new List<GearMesh>();
        // Clean up scene
        GameObject[] gears= GameObject.FindGameObjectsWithTag("MyGear");
        for (var i = 0; i < gears.Length; i++)
        {
            DestroyImmediate(gears[i]);
        }

        // Copy values from Unity editor inspector
        Vector3 newCenter = center;
        float newRadius = radius;
        float newToothWidth = toothWidth;
        int rotation = 1;

        // Add first gear datum
        gearSystemData.Add((newCenter, newRadius, newToothWidth, rotation));

        int n = NumToGenerate;
        while (n-1 > 0)
        {
            Debug.Assert(newRadius > 0);

            float distance = UnityEngine.Random.Range(1.2f * (newRadius + newToothWidth), 3f * (newRadius + newToothWidth));
            float newOuterRadius = distance - newRadius;
            newRadius = Mathf.Abs(newOuterRadius - newToothWidth);
            newCenter.x = newCenter.x + distance;
            rotation = -rotation;
            gearSystemData.Add((newCenter, newRadius, newToothWidth, rotation));
            n--;
        }

        for (int i = 0; i < gearSystemData.Count; i++)
        {
            SpawnTransform(gearSystemData[i]);
        }
    }

    void SpawnTransform((Vector3 Center, float Radius, float ToothWidth, int Rotation) tup)
    {
        GearMesh g = Instantiate<GearMesh>(proto, tup.Center, Quaternion.Euler(0,-90,0));
        g.center = tup.Center;
        g.radius = tup.Radius;
        g.toothWidth = tup.ToothWidth;
        g.rotationFactor = tup.Rotation;
        g.depth = depth;
        g.tag = "MyGear";
        gearSystem.Add(g);

    }
}
