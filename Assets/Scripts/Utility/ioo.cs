using UnityEngine;
using System.Collections;
using System.Text;
using com.junfine.simpleframework.manager;

/// <summary>
/// Interface Manager Object 
/// </summary>
namespace com.junfine.simpleframework {
    public class ioo {
        private static Hashtable prefabs = new Hashtable();

        /// <summary>
        /// 游戏管理器对象
        /// </summary>
        private static GameObject _manager = null;
        public static GameObject manager {
            get {
                if (_manager == null)
                    _manager = GameObject.FindWithTag("GameManager");
                return _manager;
            }
        }

        /// <summary>
        /// 游戏管理器
        /// </summary>
        private static GameManager _gameManager = null;
        public static GameManager gameManager {
            get {
                if (_gameManager == null)
                    _gameManager = manager.GetComponent<GameManager>();
                return _gameManager;
            }
        }

        /// <summary>
        /// 面板管理器
        /// </summary>
        private static PanelManager _panelManager = null;
        public static PanelManager panelManager {
            get {
                if (_panelManager == null)
                    _panelManager = manager.GetComponent<PanelManager>();
                return _panelManager;
            }
        }

        /// <summary>
        /// 资源管理器
        /// </summary>
        private static ResourceManager _resourceManager = null;
        public static ResourceManager resourceManager {
            get {
                if (_resourceManager == null)
                    _resourceManager = manager.GetComponent<ResourceManager>();
                return _resourceManager;
            }
        }

        /// <summary>
        /// 计时器管理器
        /// </summary>
        private static TimerManager _timerManager = null;
        public static TimerManager timerManager {
            get {
                if (_timerManager == null)
                    _timerManager = manager.GetComponent<TimerManager>();
                return _timerManager;
            }
        }

        /// 声音管理器
        /// </summary>
        private static MusicManager _musicManager = null;
        public static MusicManager musicManager {
            get {
                if (_musicManager == null)
                    _musicManager = manager.GetComponent<MusicManager>();
                return _musicManager;
            }
        }

        /// <summary>
        /// 网络管理器
        /// </summary>
        private static NetworkManager _networkManager = null;
        public static NetworkManager networkManager {
            get {
                if (_networkManager == null)
                    _networkManager = manager.GetComponent<NetworkManager>();
                return _networkManager;
            }
        }

        /// <summary>
        /// 获取描点对象
        /// </summary>
        private static Transform _mainUi;
        public static Transform MainUI {
            get {
                if (_mainUi == null)
                    _mainUi = GameObject.FindWithTag("MainUI").transform;
                return _mainUi;
            }
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        public static string f(string format, params object[] args) {
            StringBuilder sb = new StringBuilder();
            return sb.AppendFormat(format, args).ToString();
        }

        /// <summary>
        /// 字符串连接
        /// </summary>
        public static string c(params object[] args) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++) {
                sb.Append(args[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 添加Prefab
        /// </summary>
        public static void AddPrefab(string name, GameObject prefab) {
            prefabs.Add(name, prefab);
        }

        /// <summary>
        /// 获取Prefab
        /// </summary>
        public static GameObject GetPrefab(string name) {
            if (!prefabs.ContainsKey(name)) return null;
            return prefabs[name] as GameObject;
        }

        /// <summary>
        /// 移除Prefab
        /// </summary>
        /// <param name="name"></param>
        public static void RemovePrefab(string name) {
            prefabs.Remove(name);
        }

        /// <summary>
        /// 载入Prefab
        /// </summary>
        /// <param name="name"></param>
        public static GameObject LoadPrefab(string name) {
            GameObject go = GetPrefab(name);
            if (go != null) return go;
            go = Resources.Load("Prefabs/" + name, typeof(GameObject)) as GameObject;
            AddPrefab(name, go);
            return go;
        }

        /// <summary>
        /// GUI摄像机
        /// </summary>
        public static Transform guiCamera {
            get {
                GameObject go = GameObject.FindWithTag("GuiCamera");
                if (go != null) return go.transform;
                return null;
            }
        }
    }
}