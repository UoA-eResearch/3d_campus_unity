using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour {

	IEnumerator Start()
    {
        while (!Caching.ready)
            yield return null;

		var path = Application.streamingAssetsPath;
		if (Application.platform == RuntimePlatform.LinuxEditor) {
			path = "file://" + path + "/Linux/";
			Caching.ClearCache();
		} else if (Application.platform == RuntimePlatform.WebGLPlayer) {
			path += "/WebGL/";
		}
		Debug.Log(path);
        
		using (var www = WWW.LoadFromCacheOrDownload(path + "leigh", 1))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
				Debug.LogError(www.error);
                yield return null;
            }
			AssetBundle ab = www.assetBundle;
			Debug.Log(String.Join(",", ab.GetAllAssetNames()));
			var mesh = ab.LoadAsset<GameObject>("assets/leigh.fbx");
            var leigh = Instantiate(mesh);
        }
    }
}
