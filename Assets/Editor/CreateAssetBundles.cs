using System.IO;
using UnityEditor;

public class CreateAssetBundles
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
	}
}