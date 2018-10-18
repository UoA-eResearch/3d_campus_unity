using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAssetBundles : MonoBehaviour
{
	public Text status;

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
				yield break;
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
		Debug.Log("Waiting for cache");
		while (!Caching.ready)
			yield return null;
		Debug.Log("Loading");
		var leigh = StartCoroutine(LoadModel("leigh"));
		var city = StartCoroutine(LoadModel("city with skytower packed mat"));
		var epsom = StartCoroutine(LoadModel("epsom"));
		var tamaki = StartCoroutine(LoadModel("tamaki packed mat"));
		var wider = StartCoroutine(LoadModel("wider_akl2"));
		var tai = StartCoroutine(LoadModel("tai"));

		yield return leigh;
		yield return city;
		yield return epsom;
		yield return tamaki;
		yield return wider;
		yield return tai;
		Debug.Log("All models loaded!");
		status.text = "";
	}
}
