using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SimpleFramework.Manager;
using SimpleFramework;

/**
 * 注册Command ，建立 Command 与Notification 之间的映射
 */
public class BootstrapCommands : SimpleCommand {

    /// <summary>
    /// 执行启动命令
    /// </summary>
    /// <param name="notification"></param>
    public override void Execute(INotification notification) {
        //-----------------关联命令-----------------------
        Facade.RegisterCommand(NotiConst.DISPATCH_MESSAGE, typeof(SocketCommand));

        //-----------------初始化管理器-----------------------
        Facade.AddManager(ManagerName.Lua, new LuaScriptMgr());

        Facade.AddManager<PanelManager>(ManagerName.Panel);
        Facade.AddManager<MusicManager>(ManagerName.Music);
        Facade.AddManager<TimerManager>(ManagerName.Timer);
        Facade.AddManager<NetworkManager>(ManagerName.Network);
        Facade.AddManager<ResourceManager>(ManagerName.Resource);
        Facade.AddManager<ThreadManager>(ManagerName.Thread);

        Facade.AddManager<GameManager>(ManagerName.Game);
        Debug.Log("SimpleFramework StartUp-------->>>>>");
    }
}
