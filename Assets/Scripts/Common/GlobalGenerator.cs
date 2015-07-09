using UnityEngine;
using System.Collections;

namespace SimpleFramework {
    /// <summary>
    /// 全局构造器，每个场景里都有，所以每个场景都会初始化一遍，也会初始化游戏管理器一次
    /// 如果游戏管理器已经存在了，就跳过了，否则创建游戏管理器，来保证游戏里只有一个GameManager
    /// </summary>
    public class GlobalGenerator : MonoBehaviour {

        void Awake() {
            InitGameMangager();
        }

        /// <summary>
        /// 实例化游戏管理器
        /// </summary>
        public void InitGameMangager() {
            string name = "GameManager";
            GameObject manager = GameObject.Find(name);
            if (manager == null) {
                manager = new GameObject(name);
                manager.name = name;

                AppFacade.Instance.StartUp();   //启动游戏
            }
        }

        void OnGUI() {
            GUI.Label(new Rect(10, 0, 500, 50), "(1) 单击 \"Lua/Gen Lua Wrap Files\"。");
            GUI.Label(new Rect(10, 20, 500, 50), "(2) 运行Unity游戏");
            GUI.Label(new Rect(10, 40, 500, 50), "PS: 清除缓存，单击\"Lua/Clear LuaBinder File + Wrap Files\"。");
            GUI.Label(new Rect(10, 60, 900, 50), "PS: 若运行到真机，请设置Const.DebugMode=false，本地调试请设置Const.DebugMode=true");
            GUI.Label(new Rect(10, 80, 500, 50), "PS: 加Unity+ulua技术讨论群：>>341746602");
        }
    }
}