using UnityEngine;
using System;
using System.Collections;

public class TestDelegateListener : MonoBehaviour
{
    public Action onClick = null;
    public TestLuaDelegate.VoidDelegate onEvClick = null;

    // Update is called once per frame
    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 200, 20), "测试委托1")) {
            if (onClick != null) onClick();
        }
        if (GUI.Button(new Rect(10, 50, 200, 20), "测试委托2")) {
            if (onEvClick != null) onEvClick(gameObject);
        }
    }
}
