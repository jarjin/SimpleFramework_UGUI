using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SimpleFramework.Manager;
using SimpleFramework;

/**
 * 注册Command ，建立 Command 与Notification 之间的映射
 */
public class BootstrapViewMediators : SimpleCommand {

    /// <summary>
    /// 执行启动命令
    /// </summary>
    /// <param name="notification"></param>
    public override void Execute(INotification noti) {
        GameObject gameMgr = GameObject.Find("GlobalGenerator");

        AppView appView = gameMgr.GetComponent<AppView>();
        Facade.RegisterMediator(new AppMediator(appView));
    }
}
