using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace com.junfine.simpleframework {
    public class BaseLua : MonoBehaviour {
        private string data = null;
        private bool initialize = false;
        private Transform trans = null;
        private LuaScriptMgr umgr = null;
        private List<LuaFunction> buttons = new List<LuaFunction>();

        public LuaScriptMgr uluaMgr {
            get {
                if (umgr == null) {
                    umgr = ioo.gameManager.uluaMgr;
                }
                return umgr;
            }
        }

        protected void Start() {
            trans = transform;
            if (uluaMgr != null) {
                LuaState l = uluaMgr.lua;
                l[trans.name + ".transform"] = transform;
                l[trans.name + ".gameObject"] = gameObject;
            }
            CallMethod("Start");
        }

        protected void OnClick() {
            CallMethod("OnClick");
        }

        protected void OnClickEvent(GameObject go) {
            CallMethod("OnClick", go);
        }

        /// <summary>
        /// 添加单击事件
        /// </summary>
        public void AddClick(string button, LuaFunction luafunc) {
            Transform to = trans.Find(button);
            if (to == null) return;
            buttons.Add(luafunc);
            GameObject go = to.gameObject;
            go.GetComponent<Button>().onClick.AddListener(
                delegate() {
                    luafunc.Call(go);
                }
            );
        }

        /// <summary>
        /// 清除单击事件
        /// </summary>
        public void ClearClick() {
            for (int i = 0; i < buttons.Count; i++) {
                if (buttons[i] != null) {
                    buttons[i].Dispose();
                    buttons[i] = null;
                }
            }
        }

        //-----------------------------------------------
        /// <summary>
        /// 执行Lua方法-无参数
        /// </summary>
        protected object[] CallMethod(string func) {
            if (uluaMgr == null) return null;
            string funcName = name + "." + func;
            funcName = funcName.Replace("(Clone)", "");
            return umgr.CallLuaFunction(funcName);
        }

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        protected object[] CallMethod(string func, GameObject go) {
            if (uluaMgr == null) return null;
            string funcName = name + "." + func;
            funcName = funcName.Replace("(Clone)", "");
            return umgr.CallLuaFunction(funcName, go);
        }

        /// <summary>
        /// 执行Lua方法-Socket消息
        /// </summary>
        protected object[] CallMethod(string func, int key, ByteBuffer buffer) {
            if (uluaMgr == null) return null;
            string funcName = "Network." + func;
            funcName = funcName.Replace("(Clone)", "");
            return umgr.CallLuaFunction(funcName, key, buffer);
        }

        //-----------------------------------------------------------------
        protected void OnDestroy() {
            ClearClick();
            umgr = null;
            Util.ClearMemory();
            Debug.Log("~" + name + " was destroy!");
        }
    }
}