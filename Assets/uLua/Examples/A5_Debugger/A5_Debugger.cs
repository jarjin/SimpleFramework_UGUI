using UnityEngine;
using System.Collections;

public class A5_Debugger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //LuaState l = new LuaState();
        //LuaDLL.luaopen_socket_core(l.L);
        //l.DoFile("C:/Users/Administrator/Documents/New Unity Project/Assets/uLua/Lua/debugger.lua");

        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.DoFile("debugger");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
