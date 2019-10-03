using UnityEngine;

public class Gear : Circle
{
    private float toothWidth;
    private float outerRadius;

    public float OuterRadius { get => outerRadius; protected set => outerRadius = value; }
    public float ToothWidth { get => toothWidth; protected set => toothWidth = value; }

    public int GetNumTeeth() {  return numSpokes/2; }
	public void SetNumTeeth(int nT) { numSpokes = nT * 2; }

    public Gear(Vector3 center, float radius, int numTeeth) : base(center, radius, numTeeth* 2)
	{
        ToothWidth = 2.0f * Mathf.PI * radius / (numTeeth * 2.0f);
		OuterRadius = radius + ToothWidth;
	}

    public Gear(Vector3 center, float radius, float toothWidth) : base(center, radius, (int)(2.0f*Mathf.PI*radius/toothWidth))
    {
        ToothWidth = toothWidth;
        outerRadius = radius + toothWidth;
    }

    // Constructor that sets everything explicitly
    public Gear(Vector3 center, float radius, float oR, int nT, float tW) : base(center, radius, nT* 2)
    {
        ToothWidth = tW;
        OuterRadius = oR;
    }
}
