using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.junfine.simpleframework;

namespace com.junfine.simpleframework.manager {

    public class PanelManager : MonoBehaviour {
        private Transform parent;

        Transform Parent {
            get {
                if (parent == null) {
                    parent = ioo.guiCamera;
                }
                return parent;
            }
        }

        /// <summary>
        /// 创建面板，请求资源管理器
        /// </summary>
        /// <param name="type"></param>
        public void CreatePanel(string name) {
            StartCoroutine(OnCreatePanel(name));
        }

        IEnumerator OnCreatePanel(string name) {
            yield return StartCoroutine(Initialize());

            string assetName = name + "Panel";
            // Load asset from assetBundle.
            string abName = name.ToLower() + ".unity3d";
            AssetBundleAssetOperation request = ResourceManager.LoadAssetAsync(abName, assetName, typeof(GameObject));
            if (request == null) yield break;
            yield return StartCoroutine(request);

            // Get the asset.
            GameObject prefab = request.GetAsset<GameObject>();

            if (Parent.FindChild(name) != null || prefab == null) {
                yield break;
            }
            GameObject go = Instantiate(prefab) as GameObject;
            go.name = assetName;
            go.layer = LayerMask.NameToLayer("Default");
            go.transform.SetParent(Parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.AddComponent<BaseLua>();

            Debug.LogWarning("CreatePanel::>> " + name + " " + prefab);
        }

        IEnumerator Initialize() {
            ResourceManager.BaseDownloadingURL = Util.GetRelativePath();

            // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
            var request = ResourceManager.Initialize(Const.AssetDirname);
            if (request != null)
                yield return StartCoroutine(request);
        }
    }
}