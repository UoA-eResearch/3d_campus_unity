using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles : MonoBehaviour
{
	[MenuItem("Assets/Build WebGL AssetBundles")]
	static void BuildAllAssetBundles()
	{
		string assetBundleDirectory = "Assets/StreamingAssets/WebGL";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
		Debug.Log("Assets built");
	}

	[MenuItem("Assets/Prep scene for streaming")]
	static void StreamSceneSetup()
	{
		foreach (var mesh in GameObject.FindObjectsOfType<MeshFilter>())
		{
			if (mesh.tag == "stream")
			{
				Debug.Log("removing " + mesh.name + mesh.tag);
				DestroyImmediate(mesh.GetComponent<MeshCollider>());
				DestroyImmediate(mesh.GetComponent<MeshRenderer>());
				DestroyImmediate(mesh);
			}
		}
		if (Camera.main.gameObject.GetComponent<LoadAssetBundles>() == null)
		{
			Camera.main.gameObject.AddComponent<LoadAssetBundles>();
		}
		Debug.Log("Scene prepped!");
	}

	[MenuItem("Assets/Build Linux64 AssetBundles")]
	static void BuildAllLinuxAssetBundles()
	{
		string assetBundleDirectory = "Assets/StreamingAssets/Linux64";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);
		Debug.Log("Assets built");
	}
}