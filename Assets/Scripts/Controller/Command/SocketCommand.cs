using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using SimpleFramework;

public class SocketCommand : SimpleCommand {

    public override void Execute(INotification notification) {
        object body = notification.Body;
        if (body == null) return;

        KeyValuePair<int, ByteBuffer> message = (KeyValuePair<int, ByteBuffer>)body;
        switch (message.Key) {
            default: Util.CallMethod("Network", "OnSocket", message.Key, message.Value); break;
        }
	}
}
