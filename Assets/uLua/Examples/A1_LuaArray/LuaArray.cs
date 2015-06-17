using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class LuaArray : MonoBehaviour {

    string source = @"
        function luaFunc(objs, len)
            for i = 0, len - 1 do
                print(objs[i])
            end
            local table1 = {'111', '222', '333'}
            return unpack(table1)
        end
    ";

    string[] objs = { "aaa", "bbb", "ccc" };

    // Use this for initialization
    void Start() {
        LuaScriptMgr luaMgr = new LuaScriptMgr();
        luaMgr.Start();
        LuaState l = luaMgr.lua;
        l.DoString(source);

        //c# array to lua table
        LuaFunction f = l.GetFunction("luaFunc");
        object[] rs = f.Call(objs, objs.Length);
        f.Release();

        //lua table to c# array
        foreach (object de in rs) {
            Debug.Log(de.ToString());
        }
    }
}
