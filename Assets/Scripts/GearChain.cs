using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChain : MonoBehaviour
{
    public GearMesh proto;
    public GearMesh lastCreated;
    Vector3 newPosition;
    public float deltaX = 1f;

    private static int c = 0;

    void Start()
    {
        newPosition = lastCreated.transform.position;
        InvokeRepeating("SpawnGear", 2.0f, 0.3f);
    }


    void SpawnGear()
    {
        newPosition.x = newPosition.x + deltaX;
        Gear3D last = new Gear3D(lastCreated.gearData);
        lastCreated = Instantiate<GearMesh>(proto, newPosition, Quaternion.Euler(0, -90, 0));
        lastCreated.SetupGear(ref last);

    }
}
