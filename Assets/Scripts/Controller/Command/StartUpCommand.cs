using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SimpleFramework;
using SimpleFramework.Manager;

public class StartUpCommand : MacroCommand {

    protected override void InitializeMacroCommand() {
        base.InitializeMacroCommand();

        if (!Util.CheckEnvironment()) return;

        //BootstrapModels
        AddSubCommand(typeof(BootstrapModels));

        //BootstrapCommands
        AddSubCommand(typeof(BootstrapCommands));

        //BootstrapViewMediators
        AddSubCommand(typeof(BootstrapViewMediators));
    }

}