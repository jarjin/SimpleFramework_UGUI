using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SimpleFramework;
using SimpleFramework.Manager;
using UnityEditor;

public class StartUpCommand : MacroCommand {

    protected override void InitializeMacroCommand() {
        base.InitializeMacroCommand();

        int resultId = Util.CheckRuntimeFile();
        if (resultId == -1) {
            Debug.LogError("没有找到框架所需要的资源，单击Game菜单下Build xxx Resource生成！！");
            EditorApplication.isPlaying = false;
            return;
        } else if (resultId == -2) {
            Debug.LogError("没有找到Wrap脚本缓存，单击Lua菜单下Gen Lua Wrap Files生成脚本！！");
            EditorApplication.isPlaying = false;
            return;
        }
        //BootstrapModels
        AddSubCommand(typeof(BootstrapModels));

        //BootstrapCommands
        AddSubCommand(typeof(BootstrapCommands));

        //BootstrapViewMediators
        AddSubCommand(typeof(BootstrapViewMediators));
    }

}