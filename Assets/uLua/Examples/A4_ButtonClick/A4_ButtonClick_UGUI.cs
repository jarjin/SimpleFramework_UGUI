using UnityEngine;
using System.Collections;
using LuaInterface;
using UnityEngine.UI;

public class A4_ButtonClick_UGUI : MonoBehaviour
{
    public Button button;

    private string script =
    @"                  
        function doClick()
            print('Button Click:>>>')
        end

        function TestClick(button)    
            button.onClick:AddListener(doClick);
        end
    ";
	// Use this for initialization
	void Start () {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.DoString(script);

        LuaFunction func = mgr.GetLuaFunction("TestClick");
        func.Call(button);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
