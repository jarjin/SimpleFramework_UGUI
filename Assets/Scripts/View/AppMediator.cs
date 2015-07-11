using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class AppMediator : Mediator {

    public const string NAME = "AppMediator";

    public AppMediator(object viewComponent)
        : base(NAME, viewComponent) {
    }

    public AppView view {
        get {
            return ViewComponent as AppView;
        }
    }

    /// <summary>
    /// 监听的消息
    /// </summary>
    /// <returns></returns>
    override public IList<string> ListNotificationInterests() {
        return new List<string>()
        { 
            NotiConst.UPDATE_MESSAGE,
            NotiConst.UPDATE_EXTRACT,
            NotiConst.UPDATE_DOWNLOAD,
            NotiConst.UPDATE_PROGRESS,
        };
    }

    override public void HandleNotification(INotification notification) {
        string name = notification.Name;
        object body = notification.Body;
        switch (name) {
            case NotiConst.UPDATE_MESSAGE:      //更新消息
                view.UpdateMessage(body.ToString());
            break;
            case NotiConst.UPDATE_EXTRACT:      //更新解压
                view.UpdateExtract(body.ToString());
            break;
            case NotiConst.UPDATE_DOWNLOAD:     //更新下载
                view.UpdateDownload(body.ToString());
            break;
            case NotiConst.UPDATE_PROGRESS:     //更新下载进度
                view.UpdateProgress(body.ToString());
            break;
        }
    }
}
