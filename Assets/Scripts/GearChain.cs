using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChain : MonoBehaviour
{
    public GearMesh lastCreated;
    Vector3 newPosition;

    private static int c = 0;

    void Start()
    {
        newPosition = lastCreated.transform.position;
        InvokeRepeating("SpawnGear", 2.0f, 0.3f);
    }


    void SpawnGear()
    {
        newPosition.x = newPosition.x + 5;
        print(string.Format("{0}", ++c));
        GearMesh l = lastCreated;
        print(string.Format("lastCreated: {0}", l.ToString()));
        Gear3D g = l.gearData;
        print(string.Format("oR: {0}", g.OuterRadius.ToString()));
        Gear3D last = new Gear3D(lastCreated.gearData);
        print(string.Format("last: {0}", last.OuterRadius.ToString()));
        lastCreated = Instantiate<GearMesh>(lastCreated, newPosition, Quaternion.identity);
        lastCreated.SetupGear(ref last);

    }
}
