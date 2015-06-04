using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Junfine.Dota.Common;
using Junfine.Dota.Message;
using SuperSocket.SocketBase.Protocol;

namespace Junfine.Dota.Utility {
    class SocketUtil {
        static SocketUtil socket;
        public static SocketUtil instance {
            get {
                if (socket == null)
                    socket = new SocketUtil();
                return socket;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public static void SendMessage(ClientSession session, ByteBuffer buffer) {
            byte[] message = buffer.ToBytes();
            using (MemoryStream ms = new MemoryStream()) {
                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);
                ushort msglen = (ushort)message.Length;
                writer.Write(msglen);
                writer.Write(message);
                writer.Flush();
                if (session != null && session.Connected) {
                    byte[] payload = ms.ToArray();
                    session.Send(payload, 0, payload.Length);
                } else {
                    Console.WriteLine("client.connected----->>false");
                }
            }
        }

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="session"></param>
        public void OnSessionConnected(ClientSession session) {
            Console.WriteLine("OnSessionConnected--->>>" + session.RemoteEndPoint.Address);
        }

        /// <summary>
        /// 数据接收
        /// </summary>
        public void OnRequestReceived(ClientSession session, BinaryRequestInfo requestInfo) {
            ByteBuffer buffer = new ByteBuffer(requestInfo.Body);
            int commandId = buffer.ReadShort();
            Protocal c = (Protocal)commandId;
            string className = "Junfine.Dota.Message." + c;
            Console.WriteLine("OnRequestReceived--->>>" + className);

            Type t = Type.GetType(className);
            IMessage obj = (IMessage)Activator.CreateInstance(t);
            if (obj != null) obj.OnMessage(session, buffer);
            obj = null; t = null;   //释放内存
        }
    }
}
