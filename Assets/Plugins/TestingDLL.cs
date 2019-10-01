
using UnityEngine;
using System.Runtime.InteropServices;

public class TestingDLL : MonoBehaviour
{

	[DllImport("TestDLL", EntryPoint = "TestSort")]
	public static extern void TestSort(int [] a, int length);

	public int[] a;

    void Start()
    {
        TestSort(a, a.Length);
    }
}
