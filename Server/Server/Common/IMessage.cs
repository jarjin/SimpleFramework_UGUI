using System;

namespace Junfine.Dota.Common {
    public interface IMessage {
        void OnMessage(ClientSession session, ByteBuffer buffer);
    }
}
