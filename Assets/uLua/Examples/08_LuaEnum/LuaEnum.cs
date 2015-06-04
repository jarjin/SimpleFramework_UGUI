using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaEnum : MonoBehaviour {
    const string source = @"
        local type = LuaEnumType.IntToEnum(1);
        print(type == LuaEnumType.AAA);
    ";

	// Use this for initialization
	void Start () {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.lua.DoString(source);
	}
}
