using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleFramework {
    public class AppConst {
        public const bool DebugMode = false;                       //调试模式-用于内部测试
        public const bool UpdateMode = false;                     //调试模式

        public const int TimerInterval = 1;
        public const int GameFrameRate = 30;                       //游戏帧频

        public const bool UsePbc = true;                           //PBC
        public const bool UseLpeg = true;                          //LPEG
        public const bool UsePbLua = true;                         //Protobuff-lua-gen
        public const bool UseCJson = true;                         //CJson
        public const bool UseSproto = true;                        //Sproto
        public const bool LuaEncode = false;                        //使用LUA编码

        public const string AppName = "SimpleFramework";           //应用程序名称
        public const string AppPrefix = AppName + "_";             //应用程序前缀
        public const string ExtName = ".assetbundle";              //素材扩展名
        public const string AssetDirname = "StreamingAssets";      //素材目录 
        public const string WebUrl = "http://web01264.w31.vhost002.cn/";  //测试更新地址

        public static string UserId = string.Empty;                 //用户ID
        public static int SocketPort = 0;                           //Socket服务器端口
        public static string SocketAddress = string.Empty;          //Socket服务器地址
    }
}