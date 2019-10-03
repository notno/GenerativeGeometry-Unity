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
    public float rotationFactor = 1f;

    List<(Vector3 Center, float Radius, float ToothWidth, float RotationFactor)> gearSystemData;
    public List<GearMesh> gearSystem;

    void Start()
    {
        //InvokeRepeating("SpawnGear", 2.0f, 0.3f);
    }

    public void GenerateGearSystem()
    {
        // Reinitialize data list
        gearSystemData = new List<(Vector3 Center, float Radius, float ToothWidth, float RotationFactor)>();
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
        float newRotation = 1f;

        // Add first gear datum
        gearSystemData.Add((newCenter, newRadius, newToothWidth, newRotation * rotationFactor));

        int n = NumToGenerate;
        while (n-1 > 0)
        {
            Debug.Assert(newRadius > 0);

            float distance = UnityEngine.Random.Range(1.2f * (newRadius + newToothWidth), 3f * (newRadius + newToothWidth));
            float newOuterRadius = distance - newRadius;
            newRadius = Mathf.Abs(newOuterRadius - newToothWidth);
            newCenter.x = newCenter.x + distance;
            newRotation = -newRotation;
            gearSystemData.Add((newCenter, newRadius, newToothWidth, newRotation));
            n--;
        }

        for (int i = 0; i < gearSystemData.Count; i++)
        {
            SpawnTransform(gearSystemData[i]);
        }
    }

    void SpawnTransform((Vector3 Center, float Radius, float ToothWidth, float RotationFactor) tup)
    {
        GearMesh g = Instantiate<GearMesh>(proto, tup.Center, Quaternion.Euler(0,-90,0));
        g.center = tup.Center;
        g.radius = tup.Radius;
        g.toothWidth = tup.ToothWidth;
        g.rotationFactor = tup.RotationFactor * rotationFactor / (2f * Mathf.PI * tup.Radius);
        g.depth = depth;
        g.tag = "MyGear";
        gearSystem.Add(g);

    }
}
