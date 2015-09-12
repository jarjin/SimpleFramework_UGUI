using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaWWW : MonoBehaviour {
    LuaScriptMgr lua;

    string script = @"      
        local WWW = UnityEngine.WWW
                             
        function testFunc()
            local www = WWW('http://bbs.ulua.org/readme.txt');
            coroutine.www(www);
            print(www.text);    
        end
        
        coroutine.start(testFunc)
    ";

	// Use this for initialization
	void Start () {
        lua = new LuaScriptMgr();
        lua.Start();
        lua.DoString(script);
	}

    void Update() {
        lua.Update();
    }

    void LateUpdate() {
        lua.LateUpate();
    }

    void FixedUpdate() {
        lua.FixedUpdate();
    }
}
