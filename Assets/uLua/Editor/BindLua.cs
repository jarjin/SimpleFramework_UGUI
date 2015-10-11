using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

using Object = UnityEngine.Object;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine.Rendering;
using SimpleFramework;

public class BindType {
    public string name;                 //类名称
    public Type type;
    public bool IsStatic;
    public string baseName = null;      //继承的类名字
    public string wrapName = "";        //产生的wrap文件名字
    public string libName = "";         //注册到lua的名字

    string GetTypeStr(Type t) {
        string str = t.ToString();

        if (t.IsGenericType) {
            str = GetGenericName(t);
        } else if (str.Contains("+")) {
            str = str.Replace('+', '.');
        }

        return str;
    }

    private static string[] GetGenericName(Type[] types) {
        string[] results = new string[types.Length];

        for (int i = 0; i < types.Length; i++) {
            if (types[i].IsGenericType) {
                results[i] = GetGenericName(types[i]);
            } else {
                results[i] = ToLuaExport.GetTypeStr(types[i]);
            }

        }

        return results;
    }

    private static string GetGenericName(Type type) {
        if (type.GetGenericArguments().Length == 0) {
            return type.Name;
        }

        Type[] gArgs = type.GetGenericArguments();
        string typeName = type.Name;
        string pureTypeName = typeName.Substring(0, typeName.IndexOf('`'));

        return pureTypeName + "<" + string.Join(",", GetGenericName(gArgs)) + ">";
    }

    public BindType(Type t) {
        type = t;

        name = ToLuaExport.GetTypeStr(t);

        if (t.IsGenericType) {
            libName = ToLuaExport.GetGenericLibName(t);
            wrapName = ToLuaExport.GetGenericLibName(t);
        } else {
            libName = t.FullName.Replace("+", ".");
            wrapName = name.Replace('.', '_');

            if (name == "object") {
                wrapName = "System_Object";
            }
        }

        if (t.BaseType != null) {
            baseName = ToLuaExport.GetTypeStr(t.BaseType);

            if (baseName == "ValueType") {
                baseName = null;
            }
        }

        if (t.GetConstructor(Type.EmptyTypes) == null && t.IsAbstract && t.IsSealed) {
            baseName = null;
            IsStatic = true;
        }
    }

    public BindType SetBaseName(string str) {
        baseName = str;
        return this;
    }

    public BindType SetWrapName(string str) {
        wrapName = str;
        return this;
    }

    public BindType SetLibName(string str) {
        libName = str;
        return this;
    }
}

[InitializeOnLoad]
public static class LuaBinding
{
    static bool beAutoGen = false;

    //static LuaBinding()
    //{        
    //    string dir = Application.dataPath + "/Source/LuaWrap/";

    //    if (!Directory.Exists(dir))
    //    {
    //        Directory.CreateDirectory(dir);
    //    }
        
    //    string[] files = Directory.GetFiles(dir);

    //    if (files.Length <= 0)
    //    {
    //        if (EditorUtility.DisplayDialog("自动生成", "点击确定自动注册常用unity类和委托， 也可通过菜单完成此功能", "确定", "取消"))
    //        {
    //            beAutoGen = true;
    //            Binding();
    //            GenLuaDelegates();
    //            GenLuaBinder();
    //            beAutoGen = false;
    //        }
    //    }
    //}

    [MenuItem("Lua/Gen Lua Wrap Files", false, 11)]
    public static void Binding()
    {
        if (!Application.isPlaying)
        {
            EditorApplication.isPlaying = true;
        }

        BindType[] list = WrapFile.binds;

        for (int i = 0; i < list.Length; i++)
        {
            ToLuaExport.Clear();
            ToLuaExport.className = list[i].name;
            ToLuaExport.type = list[i].type;
            ToLuaExport.isStaticClass = list[i].IsStatic;
            ToLuaExport.baseClassName = list[i].baseName;
            ToLuaExport.wrapClassName = list[i].wrapName;
            ToLuaExport.libClassName = list[i].libName;
            ToLuaExport.Generate(null);
        }

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < list.Length; i++)
        {
            sb.AppendFormat("\t\t{0}Wrap.Register();\r\n", list[i].wrapName);
        }

        EditorApplication.isPlaying = false;
        //StringBuilder sb1 = new StringBuilder();

        //for (int i = 0; i < binds.Length; i++)
        //{
        //    sb1.AppendFormat("\t\t{0}Wrap.Register(L);\r\n", binds[i].wrapName);
        //}

        GenLuaBinder();
        GenLuaDelegates();
        Debug.Log("Generate lua binding files over");
        AssetDatabase.Refresh();        
    }

    //[MenuItem("Lua/Gen LuaBinder File", false, 12)]
    static void GenLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("public static class LuaBinder");
        sb.AppendLine("{");
        sb.AppendLine("\tpublic static List<string> wrapList = new List<string>();");
        sb.AppendLine("\tpublic static void Bind(IntPtr L, string type = null)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (type == null || wrapList.Contains(type)) return;");
        sb.AppendLine("\t\twrapList.Add(type); type += \"Wrap\";");
        sb.AppendLine("\t\tswitch (type) {");
        string[] files = Directory.GetFiles("Assets/uLua/Source/LuaWrap/", "*.cs", SearchOption.TopDirectoryOnly);

        List<string> wrapfiles = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            string wrapName = Path.GetFileName(files[i]);
            int pos = wrapName.LastIndexOf(".");
            wrapName = wrapName.Substring(0, pos);
            sb.AppendFormat("\t\t\tcase \"{0}\": {0}.Register(L); break;\r\n", wrapName);

            string wrapfile = wrapName.Substring(0, wrapName.Length - 4);
            wrapfiles.Add("import '" + wrapfile + "'");
        }
        if (AppConst.AutoWrapMode) {
            string wrapfile = Application.dataPath + "/Lua/System/Wrap.lua";
            File.WriteAllLines(wrapfile, wrapfiles.ToArray());
        }
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string file = AppConst.LuaBasePath + "Base/LuaBinder.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
    }

    [MenuItem("Lua/Clear LuaBinder File + Wrap Files", false, 13)]
    public static void ClearLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("public static class LuaBinder");
        sb.AppendLine("{");
        sb.AppendLine("\tpublic static List<string> wrapList = new List<string>();");
        sb.AppendLine("\tpublic static void Bind(IntPtr L, string type = null)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string file = AppConst.LuaBasePath + "Base/LuaBinder.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
        if (AppConst.AutoWrapMode) {
            string wrapfile = Application.dataPath + "/Lua/System/Wrap.lua";
            File.WriteAllText(wrapfile, string.Empty);
        }
        ClearFiles(AppConst.LuaWrapPath);
        AssetDatabase.Refresh();
    }

    static DelegateType _DT(Type t)
    {
        return new DelegateType(t);
    }

    static HashSet<Type> GetCustomDelegateTypes()
    {
        BindType[] list = WrapFile.binds;
        HashSet<Type> set = new HashSet<Type>();
        BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Instance;

        for (int i = 0; i < list.Length; i++)
        {
            Type type = list[i].type;
            FieldInfo[] fields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | binding);
            PropertyInfo[] props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | binding);
            MethodInfo[] methods = null;

            if (type.IsInterface)
            {
                methods = type.GetMethods();
            }
            else
            {
                methods = type.GetMethods(BindingFlags.Instance | binding);
            }

            for (int j = 0; j < fields.Length; j++)
            {
                Type t = fields[j].FieldType;

                if (typeof(System.Delegate).IsAssignableFrom(t))
                {
                    set.Add(t);
                }
            }

            for (int j = 0; j < props.Length; j++)
            {
                Type t = props[j].PropertyType;

                if (typeof(System.Delegate).IsAssignableFrom(t))
                {
                    set.Add(t);
                }
            }

            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo m = methods[j];

                if (m.IsGenericMethod)
                {                    
                    continue;
                }

                ParameterInfo[] pifs = m.GetParameters();

                for (int k = 0; k < pifs.Length; k++)
                {
                    Type t = pifs[k].ParameterType;

                    if (typeof(System.MulticastDelegate).IsAssignableFrom(t))
                    {
                        set.Add(t);
                    }
                }
            }

        }

        return set;
    }

    /// <summary>
    /// 清除缓存文件
    /// </summary>
    /// <param name="path"></param>
    static void ClearFiles(string path) {
        string[] names = Directory.GetFiles(path);
        foreach (var filename in names) {
            File.Delete(filename); //删除缓存
        }
    }

    [MenuItem("Lua/Gen Lua Delegates", false, 2)]
    static void GenLuaDelegates()
    {
        ToLuaExport.Clear();
        List<DelegateType> list = new List<DelegateType>();

        list.AddRange(new DelegateType[] {
            _DT(typeof(Action<GameObject>)),
            _DT(typeof(Action)),
            _DT(typeof(UnityEngine.Events.UnityAction)),                     
        });

        HashSet<Type> set = beAutoGen ? ToLuaExport.eventSet : GetCustomDelegateTypes();                
        List<Type> typeList = new List<Type>();

        foreach (Type t in set)
        {
            if (null == list.Find((p) => { return p.type == t; }))
            {
                list.Add(_DT(t));
            }
        }

        ToLuaExport.GenDelegates(list.ToArray());
        set.Clear();
        ToLuaExport.Clear();
        AssetDatabase.Refresh();
        Debug.Log("Create lua delegate over");
    }

    static void CopyLuaToOut(string dir)
    {
        string[] files = Directory.GetFiles(Application.dataPath + "/Lua/" + dir, "*.lua", SearchOption.TopDirectoryOnly);
        string outDir = Application.dataPath + "/Lua/Out/" + dir + "/";

        if (!File.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        for (int i = 0; i < files.Length; i++)
        {
            string fname = Path.GetFileName(files[i]);
            FileUtil.CopyFileOrDirectory(files[i], outDir + fname + ".bytes");
        }
    }

    static string GetOS()
    {
#if UNITY_STANDALONE
        return "Win";
#elif UNITY_ANDROID
        return "Android";
#elif UNITY_IPHONE
        return "IOS";
#endif

    }

    static void CreateDir(string dir)
    {        
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void BuildLuaBundle(string dir)
    {
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;

        string[] files = Directory.GetFiles("Assets/Lua/Out/" + dir, "*.lua.bytes");
        List<Object> list = new List<Object>();
        string bundleName = dir == null ? "Lua.unity3d" : "Lua_" + dir + ".unity3d";

        CreateDir(Application.dataPath + "/Bundle/");
        CreateDir(string.Format("{0}/{1}/", Application.persistentDataPath, GetOS()));

        for (int i = 0; i < files.Length; i++)
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(files[i]);
            list.Add(obj);
        }

        if (files.Length > 0)
        {            
            string output = string.Format("{0}/Bundle/" + bundleName, Application.dataPath);
            BuildPipeline.BuildAssetBundle(null, list.ToArray(), output, options, EditorUserBuildSettings.activeBuildTarget);
            string output1 = string.Format("{0}/{1}/" + bundleName, Application.persistentDataPath, GetOS());
            File.Delete(output1);
            File.Copy(output, output1);
            AssetDatabase.Refresh();
        }
    }
    /// <summary>
    /// 编码LUA文件用UTF-8
    /// </summary>
    [MenuItem("Lua/Encode LuaFile with UTF-8", false, 50)]
    public static void EncodeLuaFile() {
        string path = Application.dataPath + "/Lua";
        string[] files = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);
        foreach (string f in files) {
            string file = f.Replace('\\', '/');

            string content = File.ReadAllText(file);
            using (var sw = new StreamWriter(file, false, new UTF8Encoding(false))) {
                sw.Write(content);
            }
            Debug.Log("Encode file::>>" + file + " OK!");
        }
    }
	
    [MenuItem("Lua/Build Lua without jit", false, 4)]
    public static void BuildLuaNoJit()
    {        
        string dir = Application.dataPath + "/Lua/Out/";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string[] files = Directory.GetFiles(dir, "*.lua.bytes", SearchOption.AllDirectories);

        for (int i = 0; i < files.Length; i++)
        {
            FileUtil.DeleteFileOrDirectory(files[i]);
        }

        CopyLuaToOut(null);
        AssetDatabase.Refresh();
        BuildLuaBundle(null);
        UnityEngine.Debug.Log("编译lua without jit结束");
    }

    [MenuItem("Lua/Gen u3d Wrap Files(慎用)", false, 51)]
    public static void U3dBinding()
    {
        List<string> dropList = new List<string>
        {      
            //特殊修改
            "UnityEngine.Object",

            //一般情况不需要的类, 编辑器相关的
            "HideInInspector",
            "ExecuteInEditMode",
            "AddComponentMenu",
            "ContextMenu",
            "RequireComponent",
            "DisallowMultipleComponent",
            "SerializeField",
            "AssemblyIsEditorAssembly",
            "Attribute",  //一些列文件，都是编辑器相关的     
            "FFTWindow",
  
            "Types",
            "UnitySurrogateSelector",            
            "TypeInferenceRules",            
            "ThreadPriority",
            "Debug",        //自定义debugger取代
            "GenericStack",

            //异常，lua无法catch
            "PlayerPrefsException",
            "UnassignedReferenceException",            
            "UnityException",
            "MissingComponentException",
            "MissingReferenceException",

            //RPC网络
            "RPC",
            "Network",
            "MasterServer",
            "BitStream",
            "HostData",
            "ConnectionTesterStatus",

            //unity 自带编辑器GUI
            "GUI",
            "EventType",
            "EventModifiers",
            //"Event",
            "FontStyle",
            "TextAlignment",
            "TextEditor",
            "TextEditorDblClickSnapping",
            "TextGenerator",
            "TextClipping",
            "TextGenerationSettings",
            "TextAnchor",
            "TextAsset",
            "TextWrapMode",
            "Gizmos",
            "ImagePosition",
            "FocusType",
            

            //地形相关
            "Terrain",                            
            "Tree",
            "SplatPrototype",
            "DetailPrototype",
            "DetailRenderMode",

            //其他
            "MeshSubsetCombineUtility",
            "AOT",
            "Random",
            "Mathf",
            "Social",
            "Enumerator",       
            "SendMouseEvents",               
            "Cursor",
            "Flash",
            "ActionScript",
            
    
            //非通用的类
            "ADBannerView",
            "ADInterstitialAd",            
            "Android",
            "jvalue",
            "iPhone",
            "iOS",
            "CalendarIdentifier",
            "CalendarUnit",
            "CalendarUnit",
            "FullScreenMovieControlMode",
            "FullScreenMovieScalingMode",
            "Handheld",
            "LocalNotification",
            "Motion",   //空类
            "NotificationServices",
            "RemoteNotificationType",      
            "RemoteNotification",
            "SamsungTV",
            "TextureCompressionQuality",
            "TouchScreenKeyboardType",
            "TouchScreenKeyboard",
            "MovieTexture",

            //我不需要的
            //2d 类
            "AccelerationEventWrap", //加速
            "AnimatorUtility",
            "AudioChorusFilter",		
		    "AudioDistortionFilter",
		    "AudioEchoFilter",
		    "AudioHighPassFilter",		    
		    "AudioLowPassFilter",
		    "AudioReverbFilter",
		    "AudioReverbPreset",
		    "AudioReverbZone",
		    "AudioRolloffMode",
		    "AudioSettings",		    
		    "AudioSpeakerMode",
		    "AudioType",
		    "AudioVelocityUpdateMode",
            
            "Ping",
            "Profiler",
            "StaticBatchingUtility",
            "Font",
            "Gyroscope",                        //不需要重力感应
            "ISerializationCallbackReceiver",   //u3d 继承的序列化接口，lua不需要
            "ImageEffectOpaque",                //后处理
            "ImageEffectTransformsToLDR",
            "PrimitiveType",                // 暂时不需要 GameObject.CreatePrimitive           
            "Skybox",                       //不会u3d自带的Skybox
            "SparseTexture",                // mega texture 不需要
            "Plane",
            "PlayerPrefs",

            //不用ugui
            "SpriteAlignment",
		    "SpriteMeshType",
		    "SpritePackingMode",
		    "SpritePackingRotation",
		    "SpriteRenderer",
		    "Sprite",
            "UIVertex",
            "CanvasGroup",
            "CanvasRenderer",
            "ICanvasRaycastFilter",
            "Canvas",
            "RectTransform",
            "DrivenRectTransformTracker",
            "DrivenTransformProperties",
            "RectTransformAxis",
		    "RectTransformEdge",
		    "RectTransformUtility",
		    "RectTransform",
            "UICharInfo",
		    "UILineInfo",

            //不需要轮子碰撞体
            "WheelCollider",
		    "WheelFrictionCurve",
		    "WheelHit",

            //手机不适用雾
            "FogMode",

            "UnityEventBase",
		    "UnityEventCallState",
		    "UnityEvent",

            "LightProbeGroup",
            "LightProbes",

            "NPOTSupport", //只是SystemInfo 的一个枚举值

            //没用到substance纹理
            "ProceduralCacheSize",
		    "ProceduralLoadingBehavior",
		    "ProceduralMaterial",
		    "ProceduralOutputType",
		    "ProceduralProcessorUsage",
		    "ProceduralPropertyDescription",
		    "ProceduralPropertyType",
		    "ProceduralTexture",

            //物理关节系统
		    "JointDriveMode",
		    "JointDrive",
		    "JointLimits",		
		    "JointMotor",
		    "JointProjectionMode",
		    "JointSpring",
            "SoftJointLimit",
            "SpringJoint",
            "HingeJoint",
            "FixedJoint",
            "ConfigurableJoint",
            "CharacterJoint",            
		    "Joint",

            "LODGroup",
		    "LOD",

            "DataUtility",          //给sprite使用的
            "CrashReport",
            "CombineInstance",
        };

        List<BindType> list = new List<BindType>();
        Assembly assembly = Assembly.Load("UnityEngine");
        Type[] types = assembly.GetExportedTypes();

        for (int i = 0; i < types.Length; i++)
        {
            //不导出： 模版类，event委托, c#协同相关, obsolete 类
            if (!types[i].IsGenericType && types[i].BaseType != typeof(System.MulticastDelegate) &&
                !typeof(YieldInstruction).IsAssignableFrom(types[i]) && !ToLuaExport.IsObsolete(types[i]))
            {
                list.Add(WrapFile._GT(types[i]));
            }
            else
            {
                Debug.Log("drop generic type " + types[i].ToString());
            }
        }

        for (int i = 0; i < dropList.Count; i++)
        {
            list.RemoveAll((p) => { return p.type.ToString().Contains(dropList[i]); });
        }

        //for (int i = 0; i < list.Count; i++)
        //{
        //    if (!typeof(UnityEngine.Object).IsAssignableFrom(list[i].type) && !list[i].type.IsEnum && !typeof(UnityEngine.TrackedReference).IsAssignableFrom(list[i].type)
        //        && !list[i].type.IsValueType && !list[i].type.IsSealed)            
        //    {
        //        Debug.Log(list[i].type.Name);
        //    }
        //}

        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                ToLuaExport.Clear();
                ToLuaExport.className = list[i].name;
                ToLuaExport.type = list[i].type;
                ToLuaExport.isStaticClass = list[i].IsStatic;
                ToLuaExport.baseClassName = list[i].baseName;
                ToLuaExport.wrapClassName = list[i].wrapName;
                ToLuaExport.libClassName = list[i].libName;
                ToLuaExport.Generate(null);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Generate wrap file error: " + e.ToString());
            }
        }

        GenLuaBinder();
        Debug.Log("Generate lua binding files over， Generate " + list.Count + " files");
        AssetDatabase.Refresh();
    }
}
