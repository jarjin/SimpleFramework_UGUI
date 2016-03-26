本项目已经不再更新维护，由LuaFramework_UGUI代替，
LuaFramework_UGUI地址：https://github.com/jarjin/LuaFramework_UGUI

有问题请加：ulua技术交流群 434341400

支持平台：PC/MAC/Android(armv7-a + Intel x86)/iOS(armv7 + arm64)/
	  WP8(SimpleFramework_WP_v0.1.1 (nlua))/

视频教程地址 http://pan.baidu.com/s/1gd8fG4N
游戏案例地址 http://www.ulua.org/showcase.html
框架详细介绍 http://doc.ulua.org/default.asp?cateID=4
服务器框架:  https://github.com/jarjin/ServerFramework

//-------------2016-01-24-------------
(1)修复逻辑小bug，添加移除单击监听。

//-------------2016-01-09-------------
(1)优化安卓底层库armv7-a添加-mfloat-abi=softfp使用FPU硬件来做浮点运算。
(2)iOS底层库Optimization Level从-Os修改成-O3，提高优化等级。
(3)iOS底层库Dead Code Stripping改为NO，提高稳定性。
(4)将 UnityEngine.EventSystems 添加到 usingList;
(5)判断有无`符号的程序集分隔符, 避免一些本程序集的报错。
(6)修复miss using when exporting UnityEngine.UI

//-------------2016-01-06-------------
(1)修复在同步模式下首次运行异常：Please initialize AssetBundleManifest by calling...

//-------------2015-12-30-------------
(1)去掉了uLua的Linux平台底层库。
(2)重新编译底层库，异常信息文件路径从60扩展到256字符长度。

//-------------2015-12-18-------------
(1)iOS默认加载bit。
(2)修改functions.lua里面warn、error、newobject函数为logWarn、logError、newObject。

//-------------2015-11-10-------------
(1)添加lua中使用self关键字例子A6_LuaCall

//-------------2015-10-17-------------
(1)编译底层库使其支持在安卓、iOS进行Lua真机调试。
PS:现在可调试平台包括WIN/MAC/LINUX/iOS/Android全平台。
   真机调试方法：要调试的lua需要处于打开状态。
   第一个lua脚本里面 加上 require("mobdebug").start('192.168.217.112') 。
   感谢大神tangram发现并亲测。

//-------------2015-10-15-------------
(1)修复System/Event.lua里面Util判断平台缺框架命名空间。

//-------------2015-10-11-------------
(1)修复03反射模式例子。
(2)Wrap文件列表从BindLua.cs里面独立到WrapFile.cs。

//-------------2015-09-20-------------
修复调试步骤BUG：
(1)生成wrap文件列表。
(2)将AppConst.DebugMode = true;
(3)用ZeroBrane设置到框架Lua目录。
(4)单击菜单Project/Start Debugger Server。
(5)运行游戏！

//-------------2015-09-20-------------
ulua已经支持ZeroBraneStudio的调试功能，支持全平台WIN/MAC/LINUX。
ZeroBrane Studio下载地址：http://studio.zerobrane.com/download?not-this-time

调试步骤：
（1）打开Lua/debugger.lua 需要根据自身配置路径
（2）打开zbstudio.exe，单击菜单Project/Start Debugger Server。
（3）运行例子uLua\Examples\A5_Debugger\A5_Debugger.cs

//-------------2015-09-14-------------
(1)添加了NGUI/UGUI Button单击例子。

//-------------2015-09-12-------------
(1)添加新版LuaDelegate/委托例子代码。
(2)增加Lua协同WWW访问web内容例子代码。
(3)Event.lua里的luajit xpcall与luac xpcall的自动化判断处理。

//-------------2015-09-11-------------
(1)同步cstolua最新版本内容如下：
   <1>委托新方式,能够导出自定义类型涉及的所有委托类型。
   <2>修改GetErrorFunc 
   <3>lua协同cortuine增加www请求功能
   <4>延迟销毁没有GC，delaydestroy no gc
(2)iOS平台底层库集成位操作库LuaBitOp1.0.2版

//-------------2015-08-25-------------
(1)同步cstolua2.0最新版本。
(2)修复网络管理器未启动socket的bug。

//-------------2015-08-22-------------
(1)精简PureMVC，留下了消息层，使用方式大大简化。
(2)添加自动Wrap模式，开关在AppConst.AutoWrapMode;

//-------------2015-08-20-------------
(1)修改了Util类检查环境小BUG，给UGUI同步模式添加命名空间。

//-------------2015-08-13-------------
(1)添加Lua搜索路径，lua文件可存在多个不同路径，Util.AddLuaPath(path)\Util.RemoveLuaPath(path);

//-------------2015-08-08-------------
(1)修复几个小bug。

//-------------2015-08-05-------------
(1)同步cstolua修复委托bug，修复框架LuaClass大小写引用bug。

//-------------2015-07-31-------------
(1)添加import关键字，可以动态注册c#wrap文件，代码在Wrap.lua。

//-------------2015-07-30-------------
(1)底层库集成topameng的protobuff-lua-gen修复版本的pb.c。
(2)添加面板滚动项单击事件。

//-------------2015-07-27-------------
(1)为Binary/Pblua/pbc/sproto等格式协议增加网络通讯示例。
   相关代码在：PromptCtrl.lua、Network.lua

//-------------2015-07-26-------------
(1)框架自带服务器增加Webserver功能，与客户端自更新功能完美集成。
   请开启客户端AppConst.UpdateMode = true之前，先启动服务器监听。

//-------------2015-07-25-------------
(1)修复cstolua自带test场景运行环境。
(2)实现了框架与demo的分离解耦，可直接删除Assets/Examples。

//-------------2015-07-23-------------
(1)修复il2cpp iOS64 Release模式require文件崩溃异常。

//-------------2015-07-22-------------
(1)增加了@最后的骄傲实现的32/64bit统一编码luavm，使用自带的编码工具编码。

//-------------2015-07-17-------------
(1)增加luac 32位及其 luac 64位版lua编码工具。
(2)资源管理器通过ASYNC_MODE宏增加了同步加载模式。

//-------------2015-07-13-------------
(1)修复了部分在真机不能直接运行的小BUG.

//-------------2015-07-11-------------
(1)调整Lua层代码结构，实现Ctrl层与View层分离。
(2)更新代码增加线程下载文件，脱离主线程。
(3)增加云风大神的sproto协议支持。

//-------------2015-07-10-------------
(1)集成开源框架PureMVC。
(2)游戏框架做了调整，去掉了ioo类，使用facade代替。

//-------------2015-06-28-------------
(1)启动CSTOLUA帧驱动Lua中的协同。

//-------------2015-06-27-------------
(1)修改LUAC编码脚本BUG
(2)同步CSTOLUA，修复BUG

//-------------2015-06-19-------------
(1)修改MAC自动选择底层库。
(2)增加LUAJIT/LUAVM编码功能。

//-------------2015-06-14-------------
(1)不同平台需要统一文件编码，添加LUA UTF-8编码菜单。
(2)同步cstolua1.9.9， 生成重载函数check参数bug。
(3)同步cstolua1.9.9， Vector3.MoveTowards bug。

//-------------2015-06-13-------------
(1)感谢夜莺提供的新手引导面板代码。
(2)同步cstolua1.9.9最新版。

//-------------2015-06-11-------------
(1)同步cstolua1.9.8最新版。
(2)暂时去掉了自动生成wrap功能，需要的话在Editor/BindLua.cs中反注释构造函数。

//-------------2015-06-10-------------
(1)集成cstolua 1.9.8。
(2)效率进一步提升。
(3)修复编辑器崩溃BUG.

//-------------2015-06-01-------------
(1)集成cstolua 1.9.6。
(2)luajit升级为2.0.4，ulua底层库随cstolua更新。
(3)NGUI升级为3.8.2。
(4)ICSharpCode.SharpZLib更新为“夜莺”的il2cpp无错版。
(5)删除ulua/Source目录(压缩了)，防止在xcode编译需要lua.h。
(6)dofile没有文件崩溃。
(7)添加框架命名空间。
(8)添加Wrap忽略函数列表。
(9)修复了MessagePanel逻辑。
(10)network.lua使用Event监听

//-------------2015-04-20-------------
(1)集成了tolua #1.9.1。

//-------------2015-04-11-------------
(1)集成了tolua #1.8.9，修复了协成问题。
(2)增加最新文档。

//-------------2015-04-02-------------
(1)为了兼容il2cpp跟效率，去掉了C#版的sqliteKit。
(2)添加了C版的SQLite3底层库，并且在game.lua添加了示例代码。
(3)更新到tolua c# 1.8.5
(4)添加了lpeg的底层库，并在game.lua添加了示例代码。

//-------------2015-03-31-------------
(1)在BaseLua.cs添加设置transform\gameObject对象到Lua。
(2)添加了2个Examples（lua枚举+lua类继承）。

//-------------2015-03-30-------------
(1)增加了PBC\PB-Lua\cjson的示例代码，具体参考lua/logic/game.lua和LuaHelper.cs,
   帮所有同学把底层库跟上层lua、c#无缝连接起来，降低了开发门槛。
   PS:安卓平台需要有存储卡写权限，否则失败。

//-------------2015-03-24---------------
(1)uLua底层支持了lua-cjson库。
(2)Const.cs类增加了对各个库的开关。
(3)修复了ulua 7个例子。

//-------------2015-03-22---------------
(1)uLua底层库支持云风的pbc协议库。
(2)资源管理部分增加了简单的增量更新功能。

//-------------2015-03-18---------------
(1)修复了GUI的预制物体损坏。
(2)Intel atom x86模式替换成luajit库。
(3)兼容Unity4.6.x与Unity5版本。

//-------------2015-03-11---------------
(1)去掉了nlua模式，WP平台单独开出一个分支SimpleFramework_WP。
(2)ulua安卓底层库新支持了Intel atom x86模式。
(3)支持了Unity5.0开发环境（由于NGUI兼容性问题，不支持U5新打包格式）。
(4)修复了MAC OSX模式下因为ulua.bundle未更新运行崩溃的问题。
(5)修复了自带Server不能与框架通信的BUG。

//-------------2015-02-14---------------
(1)添加nlua兼容模式，可选择ulua模式或nlua模式。

//-------------2015-02-11---------------
(1)添加了ios armv7s arm64平台支持。
(2)luajit使用了最新版本2.1。
(3)修复了iPhone5s以上设备不能直接运行的路径bug。

//-------------2015-01-18---------------
(1)增加了简单的解包功能。
(2)直接运行到真机（安卓+ios），而不在需要copy资源到真机存储卡。

//-------------2015-01-08---------------
(1)集成最新版tolua c# 1.7.2版，修复某些生成Wrap类错误BUG。
(2)修复了手动copy到ios真机上FileStream读取权限失败的BUG。
(3)清除函数缓存增加了删除Wrap文件缓存功能。

//-------------2014-12-31---------------
(1)集成最新版tolua c# 1.7.1版

//-------------2014-12-18---------------
(1)添加的可加密的sqlite功能的工具
(2)添加了sqlitekit函数库。
(3)删除了LuaWrap在U3D4.6版本之前老版本打开错误提示问题。
(4)添加了Debuger.dll,以后可使用Debuger.DebugXXX函数，而不会跳转到其函数体内。

//-------------2014-11-29---------------
(1)集成tolua c# 2.03版本
(2)增加了Class.lua自定义类
(3)修改了tolua c#中生成自定义类与U3D类合并函数

//-------------2014-10-10---------------
(1)集成tolua c# 1.2版本

//-------------2014-09-27---------------
(1)添加了一个基于supersocket的服务器端框架。
(2)集成了网络模块，并且通过lua发送消息给，返回echo流程已完成。
服务器框架程序:SimpleFramework\Server\Server\bin\Debug\SuperSocket.SocketService.exe
服务器配置文件:同上目录\SuperSocket.SocketService.exe.config
PS:运行服务器程序，需要.Net(windows)/Mono(linux) 4.0以上版本

//-------------2014-09-26---------------
(1)集成了UIWrapGrid.cs，100个滚动列表项不卡（亲测2000不卡）。
(2)因同学需求，添加了弹出面板。

//-------------2014-09-25---------------
(1)集成了阿萌的tolua c#版插件.
(2)集成了UnityVS调试插件

