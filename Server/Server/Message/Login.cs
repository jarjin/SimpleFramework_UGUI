using System;
using Junfine.Dota.Common;
using Junfine.Dota.Utility;

namespace Junfine.Dota.Message {
    class Login : IMessage {
        public void OnMessage(ClientSession session, ByteBuffer buffer) {
            string str = buffer.ReadString();
            int uid = buffer.ReadInt();

            ushort commandId = (ushort)Protocal.Login;
            ByteBuffer newBuffer = new ByteBuffer();
            newBuffer.WriteShort(commandId);
            newBuffer.WriteByte(1);
            newBuffer.WriteString(str);
            SocketUtil.SendMessage(session, newBuffer);

            session.uid = uid;
            UserUtil.Add(uid, session);
            Console.WriteLine("OnMessage--->>>" + str + uid);
        }
    }
}
