using UnityEngine;
using System.Collections;
using LuaInterface;
using System;

public class CreateGameObject : MonoBehaviour {

    private string script = @"
            luanet.load_assembly('UnityEngine')
            luanet.load_assembly('Assembly-CSharp')
            Util = luanet.import_type('Util')
            GameObject = luanet.import_type('UnityEngine.GameObject')

            local newGameObj = GameObject('NewObj')
            Util.AddComponent(newGameObj, 'UnityEngine', 'ParticleSystem')
        ";

	// Use this for initialization
	void Start () {
        LuaState l = new LuaState();
        LuaScriptMgr._translator = l.GetTranslator();
        l.DoString(script); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
