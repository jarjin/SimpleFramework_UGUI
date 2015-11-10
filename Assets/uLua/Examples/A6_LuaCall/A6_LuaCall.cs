using UnityEngine;
using System.Collections;
using LuaInterface;

public class A6_LuaCall : MonoBehaviour {

    const string script = @"
        A6_LuaCall = luanet.import_type('A6_LuaCall')  

        LuaClass = {}
        LuaClass.__index = LuaClass

        function LuaClass:New() 
            local self = {};   
            setmetatable(self, LuaClass); 
            return self;    
        end

        function LuaClass:test() 
            A6_LuaCall.OnSharpCall(self, self.callback);
        end

        function LuaClass:callback()
            print('test--->>>');
        end

        LuaClass:New():test();
    ";

	// Use this for initialization
	void Start () {
        LuaState lua = new LuaState();
        lua.DoString(script);
	}

    public static void OnSharpCall(LuaTable self, LuaFunction func) {
        func.Call(self);
    } 
}
