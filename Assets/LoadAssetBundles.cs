using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{

	IEnumerator LoadModel(string model)
	{
		var path = Application.streamingAssetsPath;
		if (Application.platform == RuntimePlatform.LinuxEditor)
		{
			path = "file://" + path + "/Linux64/";
			Caching.ClearCache();
		}
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			path += "/WebGL/";
		}
		Debug.Log(path);

		using (var www = WWW.LoadFromCacheOrDownload(path + model, 5))
		{
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.LogError(www.error);
				yield return null;
			}
			AssetBundle ab = www.assetBundle;
			Debug.Log(String.Join(",", ab.GetAllAssetNames()));
			var mesh = ab.LoadAsset<GameObject>("assets/" + model + ".fbx");
			var parent = GameObject.Find(model).transform;
			var go = Instantiate(mesh, parent);
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			go.transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	IEnumerator Start()
	{
		while (!Caching.ready)
			yield return null;
		StartCoroutine(LoadModel("leigh"));
		StartCoroutine(LoadModel("city with skytower packed mat"));
		StartCoroutine(LoadModel("epsom"));
		StartCoroutine(LoadModel("tamaki packed mat"));
		StartCoroutine(LoadModel("wider_akl2"));
		StartCoroutine(LoadModel("tai"));
	}
}
