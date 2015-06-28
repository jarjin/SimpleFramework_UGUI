using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using com.junfine.simpleframework;

namespace com.junfine.simpleframework.manager {
    public class NetworkManager : BaseLua {
        private int count;
        private TimerInfo timer;
        private bool islogging = false;
        private static Queue<KeyValuePair<int, ByteBuffer>> sEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

        new void Start() {
            base.Start();
        }

        public void OnInit() {
            if (uluaMgr == null) return;
            uluaMgr.CallLuaFunction("Network.Start");
        }

        public void Unload() {
            if (uluaMgr == null) return;
            uluaMgr.CallLuaFunction("Network.Unload");
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, ByteBuffer data) {
            sEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
        }

        void Update() {
            if (sEvents.Count > 0) {
                while (sEvents.Count > 0) {
                    KeyValuePair<int, ByteBuffer> _event = sEvents.Dequeue();
                    switch (_event.Key) {
                        default: CallMethod("OnSocket", _event.Key, _event.Value); break;
                    }
                }
            }
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect() {
            SocketClient.SendConnect();
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(ByteBuffer buffer) {
            SocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        new void OnDestroy() {
            Debug.Log("~NetworkManager was destroy");
        }
    }
}