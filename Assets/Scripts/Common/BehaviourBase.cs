using UnityEngine;
using System.Collections;
using PureMVC.Interfaces;
using SimpleFramework.Manager;

namespace SimpleFramework {

    public class BehaviourBase : MonoBehaviour {
        private AppFacade m_Facade;
        private LuaScriptMgr m_LuaMgr;
        private ResourceManager m_ResMgr;
        private NetworkManager m_NetMgr;
        private MusicManager m_MusicMgr;
        private TimerManager m_TimerMgr;
        private ThreadManager m_ThreadMgr;

        protected AppFacade facade {
            get {
                if (m_Facade == null) {
                    m_Facade = AppFacade.Instance;
                }
                return m_Facade;
            }
        }

        protected LuaScriptMgr LuaManager {
            get {
                if (m_LuaMgr == null) {
                    m_LuaMgr = facade.GetManager<LuaScriptMgr>(ManagerName.Lua);
                }
                return m_LuaMgr;
            }
            set { m_LuaMgr = value; }
        }

        protected ResourceManager ResManager {
            get {
                if (m_ResMgr == null) {
                    m_ResMgr = facade.GetManager<ResourceManager>(ManagerName.Resource);
                }
                return m_ResMgr;
            }
        }

        protected NetworkManager NetManager {
            get {
                if (m_NetMgr == null) {
                    m_NetMgr = facade.GetManager<NetworkManager>(ManagerName.Network);
                }
                return m_NetMgr;
            }
        }

        protected MusicManager MusicManager {
            get {
                if (m_MusicMgr == null) {
                    m_MusicMgr = facade.GetManager<MusicManager>(ManagerName.Music);
                }
                return m_MusicMgr;
            }
        }

        protected TimerManager TimerManger {
            get {
                if (m_TimerMgr == null) {
                    m_TimerMgr = facade.GetManager<TimerManager>(ManagerName.Timer);
                }
                return m_TimerMgr;
            }
        }

        protected ThreadManager ThreadManager {
            get {
                if (m_ThreadMgr == null) {
                    m_ThreadMgr = facade.GetManager<ThreadManager>(ManagerName.Thread);
                }
                return m_ThreadMgr;
            }
        }
    }
}