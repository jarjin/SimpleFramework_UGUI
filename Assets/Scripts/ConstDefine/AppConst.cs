using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleFramework {
    public class AppConst {
        public static bool DebugMode = false;                       //调试模式-用于内部测试
        public static bool UpdateMode = false;                     //调试模式

        public static int TimerInterval = 1;
        public static int GameFrameRate = 30;                       //游戏帧频

        public static bool UsePbc = true;                           //PBC
        public static bool UseLpeg = true;                          //LPEG
        public static bool UsePbLua = true;                         //Protobuff-lua-gen
        public static bool UseCJson = true;                         //CJson
        public static bool LuaEncode = false;                        //使用LUA编码

        public static string UserId = string.Empty;                 //用户ID
        public static string AppName = "simpleframework";           //应用程序名称
        public static string AppPrefix = AppName + "_";             //应用程序前缀
        public static string ExtName = ".unity3d";                  //素材扩展名
        public static string AssetDirname = "StreamingAssets";      //素材目录 

        public static string WebUrl = "http://web01264.w31.vhost002.cn/res/";  //测试更新地址
        public static int SocketPort = 0;                           //Socket服务器端口
        public static string SocketAddress = string.Empty;          //Socket服务器地址
    }
}