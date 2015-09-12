using UnityEngine;
using System.Collections;

public class TestLuaDelegate : MonoBehaviour {
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
}
