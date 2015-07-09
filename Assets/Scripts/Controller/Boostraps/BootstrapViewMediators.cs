using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

/**
 * 注册Command ，建立 Command 与Notification 之间的映射
 */
public class BootstrapViewMediators : SimpleCommand {

    public override void Execute(INotification notification) {
    }
}
