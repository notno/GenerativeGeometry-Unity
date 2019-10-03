using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GearChain))]
public class GearChainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GearChain myScript = (GearChain)target;
        if (GUILayout.Button("Generate New Chain"))
        {
            myScript.GenerateGearSystem();
        }
    }
}