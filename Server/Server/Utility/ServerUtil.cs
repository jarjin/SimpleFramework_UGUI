using System;
using System.Collections.Generic;
using System.Text;
using Junfine.Dota.Common;
using Junfine.Dota.Message;
using Junfine.Dota.Timer;

namespace Junfine.Dota.Utility {
    class ServerUtil {
        static ServerUtil server;
        private RedisTimer redis;
        private ConfigTimer cfg;

        public static ServerUtil instance {
            get {
                if (server == null)
                    server = new ServerUtil();
                return server;
            }
        }

        public ServerUtil() { 
        }

        /// <summary>
        /// 服务器初始化
        /// </summary>
        public void Init() {
            cfg = new ConfigTimer(); cfg.Start();
            redis = new RedisTimer(); redis.Start();

            Const.users = new Dictionary<long, ClientSession>();
            //var v = RedisUtil.Get("aaa");
            //Console.WriteLine(v);
        }

        /// <summary>
        /// 服务器关闭
        /// </summary>
        public void Close() {
            redis.Stop(); redis = null;
            cfg.Stop(); cfg = null;
        }
    }
}
