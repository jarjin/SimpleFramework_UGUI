using UnityEngine;
using System.Collections;
using LuaInterface;

public class ScriptsFromFile : MonoBehaviour {

    public TextAsset scriptFile;

	// Use this for initialization
	void Start () {
        LuaState l = new LuaState();
        LuaScriptMgr._translator = l.GetTranslator();
		l.DoString(scriptFile.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
