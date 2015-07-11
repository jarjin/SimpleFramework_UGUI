using UnityEngine;
using SimpleFramework;

public class AppView : BehaviourBase {
    private string message;

    void Start() { 
    }

    public void UpdateMessage(string data) {
        this.message = data;
    }

    public void UpdateExtract(string data) {
        this.message = data;
    }

    public void UpdateDownload(string data) {
        this.message = data;
    }

    public void UpdateProgress(string data) {
        this.message = data;
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 120, 960, 50), message);

        GUI.Label(new Rect(10, 0, 500, 50), "(1) 单击 \"Lua/Gen Lua Wrap Files\"。");
        GUI.Label(new Rect(10, 20, 500, 50), "(2) 运行Unity游戏");
        GUI.Label(new Rect(10, 40, 500, 50), "PS: 清除缓存，单击\"Lua/Clear LuaBinder File + Wrap Files\"。");
        GUI.Label(new Rect(10, 60, 900, 50), "PS: 若运行到真机，请设置Const.DebugMode=false，本地调试请设置Const.DebugMode=true");
        GUI.Label(new Rect(10, 80, 500, 50), "PS: 加Unity+ulua技术讨论群：>>341746602");
    }
}
