using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace SimpleFramework {
    public class LuaBehaviour : BehaviourBase {
        private string data = null;
        private Transform trans = null;
        private List<LuaFunction> buttons = new List<LuaFunction>();
        protected static bool initialize = false;

        protected void Start() {
            trans = transform;
            if (LuaManager != null && initialize) {
                LuaState l = LuaManager.lua;
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

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        protected object[] CallMethod(string func, params object[] args) {
            if (!initialize) return null;
            return Util.CallMethod(name, func, args);
        }

        //-----------------------------------------------------------------
        protected void OnDestroy() {
            ClearClick();
            LuaManager = null;
            Util.ClearMemory();
            Debug.Log("~" + name + " was destroy!");
        }
    }
}