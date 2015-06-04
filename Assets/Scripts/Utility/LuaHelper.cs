/*************************************************
author：ricky pu
data：2014.4.12
email:32145628@qq.com
**********************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;

namespace com.junfine.simpleframework {
    public static class LuaHelper {

        /// <summary>
        /// getType
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static System.Type GetType(string classname) {
            Assembly assb = Assembly.GetExecutingAssembly();  //.GetExecutingAssembly();
            System.Type t = null;
            t = assb.GetType(classname); ;
            if (t == null) {
                t = assb.GetType(classname);
            }
            return t;
        }

        /// <summary>
        /// GetComponentInChildren
        /// </summary>
        public static Component GetComponentInChildren(GameObject obj, string classname) {
            System.Type t = GetType(classname);
            Component comp = null;
            if (t != null && obj != null) comp = obj.GetComponentInChildren(t);
            return comp;
        }

        /// <summary>
        /// GetComponent
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static Component GetComponent(GameObject obj, string classname) {
            if (obj == null) return null;
            return obj.GetComponent(classname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static Component[] GetComponentsInChildren(GameObject obj, string classname) {
            System.Type t = GetType(classname);
            if (t != null && obj != null) return obj.transform.GetComponentsInChildren(t);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Transform[] GetAllChild(GameObject obj) {
            Transform[] child = null;
            int count = obj.transform.childCount;
            child = new Transform[count];
            for (int i = 0; i < count; i++) {
                child[i] = obj.transform.GetChild(i);
            }
            return child;
        }

        /// <summary>
        /// pbc/pblua函数回调
        /// </summary>
        /// <param name="func"></param>
        public static void OnCallLuaFunc(LuaStringBuffer data, LuaFunction func) {
            byte[] buffer = data.buffer;
            Debug.LogWarning("OnCallLuaFunc buffer:>>" + buffer + " lenght:>>" + buffer.Length);
            if (func != null) Util.PushBufferToLua(func, buffer);
        }

        /// <summary>
        /// cjson函数回调
        /// </summary>
        /// <param name="data"></param>
        /// <param name="func"></param>
        public static void OnJsonCallFunc(string data, LuaFunction func) {
            Debug.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
            if (func != null) func.Call(data);
        }
    }
}