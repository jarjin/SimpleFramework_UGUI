
    using System;
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public class Appload {
        static Appload() {
            int isShow = PlayerPrefs.GetInt("ShowWelcomeScreen", 1);
            if (isShow == 1) {
                EditorApplication.update += Update;
            }
        }

        static void Update() {
            bool isSuccess = EditorApplication.ExecuteMenuItem("Lua/Welcome Screen");
            if (isSuccess) EditorApplication.update -= Update;
        }
    }

    public class WelcomeScreen : EditorWindow
    {
        private bool flag = true;
        private Rect mContactDescriptionRect = new Rect(70f, 344f, 250f, 30f);
        private Rect mContactHeaderRect = new Rect(70f, 324f, 250f, 20f);
        private Texture mContactImage;
        private Rect mContactImageRect = new Rect(15f, 322f, 50f, 50f);
        private Rect mDocDescriptionRect = new Rect(70f, 143f, 260f, 30f);
        private Rect mDocHeaderRect = new Rect(70f, 123f, 250f, 20f);
        private Texture mDocImage;
        private Rect mDocImageRect = new Rect(15f, 124f, 53f, 50f);
        private Rect mForumDescriptionRect = new Rect(70f, 278f, 250f, 30f);
        private Rect mForumHeaderRect = new Rect(70f, 258f, 250f, 20f);
        private Texture mForumImage;
        private Rect mForumImageRect = new Rect(15f, 256f, 50f, 50f);
        private Rect mSamplesDescriptionRect = new Rect(70f, 77f, 250f, 30f);
        private Rect mSamplesHeaderRect = new Rect(70f, 57f, 250f, 20f);
        private Texture mSamplesImage;
        private Rect mSamplesImageRect = new Rect(15f, 58f, 50f, 50f);
        private Rect mToggleButtonRect = new Rect(220f, 385f, 125f, 20f);
        private Rect mVersionRect = new Rect(5f, 385f, 125f, 20f);
        private Rect mVideoDescriptionRect = new Rect(70f, 209f, 280f, 30f);
        private Rect mVideoHeaderRect = new Rect(70f, 189f, 250f, 20f);
        private Texture mVideoImage;
        private Rect mVideoImageRect = new Rect(15f, 190f, 50f, 50f);
        private Rect mWelcomeIntroRect = new Rect(46f, 12f, 306f, 40f);
        private Texture mWelcomeScreenImage;
        private Rect mWelcomeScreenImageRect = new Rect(0f, 0f, 340f, 44f);

        public void OnEnable()
        {
            //this.mWelcomeScreenImage = EditorGUIUtility.Load("WelcomeScreenHeader.png") as Texture;
                //BehaviorDesignerUtility.LoadTexture("WelcomeScreenHeader.png", false, this);

            flag = PlayerPrefs.GetInt("ShowWelcomeScreen", 1) == 1;
            this.mSamplesImage = EditorGUIUtility.Load("WelcomeScreenSamplesIcon.png") as Texture;
            this.mDocImage = EditorGUIUtility.Load("WelcomeScreenDocumentationIcon.png") as Texture;
            this.mVideoImage = EditorGUIUtility.Load("WelcomeScreenVideosIcon.png") as Texture;
            this.mForumImage = EditorGUIUtility.Load("WelcomeScreenForumIcon.png") as Texture;
            this.mContactImage = EditorGUIUtility.Load("WelcomeScreenContactIcon.png") as Texture;
        }

        public void OnGUI()
        {
            //GUI.DrawTexture(this.mWelcomeScreenImageRect, this.mWelcomeScreenImage);
            GUI.Label(this.mWelcomeIntroRect, "Welcome To SimpleFramework");
            GUI.DrawTexture(this.mSamplesImageRect, this.mSamplesImage);
            GUI.Label(this.mSamplesHeaderRect, "新手入门 - 生成Wrap文件" );
            GUI.Label(this.mSamplesDescriptionRect, "单击Lua菜单里面Gen Lua Wrap File子菜单.");
            GUI.DrawTexture(this.mDocImageRect, this.mDocImage);
            GUI.Label(this.mDocHeaderRect, "新手入门 - 根据操作系统生成AssetBundle资源");
            GUI.Label(this.mDocDescriptionRect, "单击Game菜单里面Build XXX Resources子菜单.");
            GUI.DrawTexture(this.mVideoImageRect, this.mVideoImage);
            GUI.Label(this.mVideoHeaderRect, "新手入门 - 清除Wrap文件缓存");
            GUI.Label(this.mVideoDescriptionRect, "单击Lua菜单里面Clear LuaBinder File + Wrap Files子菜单.");
            GUI.DrawTexture(this.mForumImageRect, this.mForumImage);
            GUI.Label(this.mForumHeaderRect, "框架下载地址");
            GUI.Label(this.mForumDescriptionRect, "https://github.com/jarjin/");
            GUI.DrawTexture(this.mContactImageRect, this.mContactImage);
            GUI.Label(this.mContactHeaderRect, " 加入技术支持社群");
            GUI.Label(this.mContactDescriptionRect, "QQ群:341746602 或者 QQ群:62978170");
            GUI.Label(this.mVersionRect, "Version : 0.3.0" );

            flag = GUI.Toggle(this.mToggleButtonRect, flag, "开始时候显示对话框");
            if (flag) {
                PlayerPrefs.SetInt("ShowWelcomeScreen", 1);
            } else {
                PlayerPrefs.SetInt("ShowWelcomeScreen", 0);
            }
            EditorGUIUtility.AddCursorRect(this.mSamplesImageRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mSamplesHeaderRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mSamplesDescriptionRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mDocImageRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mDocHeaderRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mDocDescriptionRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mVideoImageRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mVideoHeaderRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mVideoDescriptionRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mForumImageRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mForumHeaderRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mForumDescriptionRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mContactImageRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mContactHeaderRect, MouseCursor.Link);
            EditorGUIUtility.AddCursorRect(this.mContactDescriptionRect, MouseCursor.Link);
            if (Event.current.type == EventType.MouseUp)
            {
                Vector2 mousePosition = Event.current.mousePosition;
                if ((this.mSamplesImageRect.Contains(mousePosition) || this.mSamplesHeaderRect.Contains(mousePosition)) || this.mSamplesDescriptionRect.Contains(mousePosition))
                {
                    LuaBinding.Binding();
                }
                else if ((this.mDocImageRect.Contains(mousePosition) || this.mDocHeaderRect.Contains(mousePosition)) || this.mDocDescriptionRect.Contains(mousePosition))
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor) {
                        Packager.BuildWindowsResource();
                    }
                    if (Application.platform == RuntimePlatform.OSXEditor) {
                        Packager.BuildiPhoneResource();
                    }
                }
                else if ((this.mVideoImageRect.Contains(mousePosition) || this.mVideoHeaderRect.Contains(mousePosition)) || this.mVideoDescriptionRect.Contains(mousePosition))
                {
                    LuaBinding.ClearLuaBinder();
                }
                else if ((this.mForumImageRect.Contains(mousePosition) || this.mForumHeaderRect.Contains(mousePosition)) || this.mForumDescriptionRect.Contains(mousePosition))
                {
                    Application.OpenURL("https://github.com/jarjin/");
                }
                else if ((this.mContactImageRect.Contains(mousePosition) || this.mContactHeaderRect.Contains(mousePosition)) || this.mContactDescriptionRect.Contains(mousePosition))
                {
                    Application.OpenURL("http://shang.qq.com/wpa/qunwpa?idkey=20a9db3bac183720c13a13420c7c805ff4a2810c532db916e6f5e08ea6bc3a8f");
                }
            }
        }

        [UnityEditor.MenuItem("Lua/Welcome Screen", false, 3)]
        public static void ShowWindow()
        {
            WelcomeScreen window = EditorWindow.GetWindow<WelcomeScreen>(true, "Welcome to SimpleFramework");
            window.minSize = window.maxSize = new Vector2(370f, 410f);
            UnityEngine.Object.DontDestroyOnLoad(window);
        }
    }


