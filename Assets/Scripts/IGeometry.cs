using UnityEngine;

public interface IGeometry
{
    Vector3 center {
        get;
        set;
    }

    void MakeTriangles();
    void Generate();
}
