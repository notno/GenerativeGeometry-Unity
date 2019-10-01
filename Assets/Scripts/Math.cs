using UnityEngine;


namespace GenerativeGeometry { 
public class Math
{
    public static float Distance(Vector3 p1, Vector3 p2) {
        float aSquared = Mathf.Pow(p2.x - p1.x, 2.0f);
        float bSquared = Mathf.Pow(p2.y - p1.y, 2.0f);
        float cSquared = Mathf.Pow(p2.z - p1.z, 2.0f);
        return Mathf.Sqrt(aSquared + bSquared + cSquared);
    }
}

} // GenerativeGeometry
