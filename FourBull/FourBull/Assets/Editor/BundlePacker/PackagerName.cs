using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 自动为对应的各个模块资源设置AssetBundle名称，及后缀
/// 自动化设置AB名称
/// </summary>
public class PackagerName
{
	//Atlas/Icon/
	//Atlas/ChatFace
	//Atlas/Card/
	//Atlas/Texture/
	//Audio/Music/
	//Audio/Effect/
	//Builds/View/
	//Builds/Model/
	//Builds/Magic/


	static private string[] PrefixModules =
	{
		"GamePublic",
		"GameHall"
	};

	//设置资源的AssetBundle名称
	static private void SetPackNameInDir(DirectoryInfo dir,string dirName,int ioffset,string suffix)
	{
		Debug.Log ("Packing dir:"+dirName);
		FileInfo[] files = dir.GetFiles();
		for (int i = 0; i < files.Length; ++i)
		{
			FileInfo fileInfo = files[i];
			if (fileInfo.Name.EndsWith(suffix))
			{
				string basePath = fileInfo.FullName.Substring(ioffset).Replace('\\', '/');
				AssetImporter importer = AssetImporter.GetAtPath(basePath);
				if (importer && importer.assetBundleName != dirName)
				{
					importer.assetBundleName=dirName;
					importer.assetBundleVariant="bt";
				}
			}
		}
	}


	//设置图盘的类型为Sprite，并且将他们的PackingTag设置为文件夹名称
	static private void SetSpritePackingTag(DirectoryInfo dir,string tag,int ioffset,string suffix)
	{
		FileInfo[] files = dir.GetFiles();
		for (int i = 0; i < files.Length; ++i)
		{
			FileInfo fileInfo = files[i];
			if (fileInfo.Name.EndsWith(suffix))
			{
				string basePath = fileInfo.FullName.Substring(ioffset).Replace('\\', '/');
				TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(basePath);
				if(importer && importer.textureType != TextureImporterType.Sprite)
				{
					importer.textureType = TextureImporterType.Sprite;
				}

				if(importer && importer.spritePackingTag != tag)
				{
					importer.spritePackingTag = tag;
				}
			}
		}
	}



	//--------------------为各个模块打包指定规则----------------------
	//各个模块自动化打包
	static private string prefixIcon="/Atlas/Icon";
	static private string formatIcon="{0}_icon_{1}";
	static private void NamedIcon(string module)
	{
		string versionDir = "/BoTing/"+module + prefixIcon;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;
			
			//设置图片类型和PackingTag
			for(int m = 0;m< dirs.Length;m++)
			{
				string packtag = dirs[m].Name.ToLower();
				SetSpritePackingTag(dirs[m],"icon"+packtag,offset,".png");
			}
			
			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatIcon,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".png");
			}
		}
	}

	static private string prefixFace="/Atlas/ChatFace";
	static private string formatFace="{0}_face_{1}";
	static private void NamedChatFace (string module)
	{
		string versionDir = "/BoTing/"+ module + prefixFace;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;
			
			//设置图片类型和PackingTag
			for(int m = 0;m< dirs.Length;m++)
			{
				string packtag = dirs[m].Name.ToLower();
				SetSpritePackingTag(dirs[m],"face"+packtag,offset,".png");
			}
			
			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatFace,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".png");
			}
		}
	}


	//图集，最好将每个图片的PackingTag修改成相同的
	static private string prefixTexture="/Atlas/Texture";
	static private string formatTexture = "{0}_texture_{1}";
	static private void NamedPackTexture (string module)
	{
		string versionDir = "/BoTing/"+ module + prefixTexture;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;

			//设置图片类型和PackingTag
			for(int m = 0;m< dirs.Length;m++)
			{
				string packtag = dirs[m].Name.ToLower();
				SetSpritePackingTag(dirs[m],"ui"+packtag,offset,".png");
			}

			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatTexture,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".png");
			}
		}
	}


	static private string prefixUiPrefab="/Builds/View/";
	static private string formatUiPrefab="{0}_ui_{1}";
	static private void NamedUiPrefab(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixUiPrefab;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;
			
			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatUiPrefab,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".prefab");
			}
		}
	}

	//背景音乐里面的，每个mp3单独达成一个AssetBundle
	static private string prefixMusic = "/Audio/Music/";
	static private string formatMusic = "{0}_music_{1}";
	static private void NamedAudioBK(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixMusic;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			FileInfo[] files = maindir.GetFiles();
			int offset = fullPath.Length - relativeLen;

			for(int i = 0;i< files.Length;i++)
			{
				FileInfo fileInfo = files[i];
				if (fileInfo.Name.EndsWith(".mp3"))
				{
					string basePath = fileInfo.FullName.Substring(offset).Replace('\\', '/');
					AssetImporter importer = AssetImporter.GetAtPath(basePath);

					string bname = string.Format(formatMusic,module,fileInfo.Name.TrimEnd(".mp3".ToCharArray())).ToLower();
					if (importer && importer.assetBundleName != bname)
					{

						importer.assetBundleName = bname;
						importer.assetBundleVariant="bt";
					}
				}
			}
		}
	}

	//所有的音效，达成一个AssetBundle
	static private string prefixEffect = "/Audio/Effect/";
	static private string formatEffect = "{0}_effect_main";
	static private void NamedAundioEffect(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixEffect;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			FileInfo[] files = maindir.GetFiles();
			int offset = fullPath.Length - relativeLen;
			string bname = string.Format(formatEffect,module).ToLower();

			for(int i = 0;i< files.Length;i++)
			{
				FileInfo fileInfo = files[i];
				if (fileInfo.Name.EndsWith(".mp3"))
				{
					string basePath = fileInfo.FullName.Substring(offset).Replace('\\', '/');
					AssetImporter importer = AssetImporter.GetAtPath(basePath);

					if (importer && importer.assetBundleName != bname)
					{
						importer.assetBundleName = bname;
						importer.assetBundleVariant="bt";
					}
				}
			}
		}
	}

	static private string prefixModel="/Builds/Model/";
	static private string formatModel="{0}_model_{1}";
	static private void NamedModel(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixModel;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;
			
			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatModel,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".prefab");
			}
		}
	}

	static private string prefixMagic="/Builds/Magic/";
	static private string formatMagic="{0}_magic_{1}";
	static private void NamedMagic(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixMagic;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			DirectoryInfo[] dirs = maindir.GetDirectories();
			int offset = fullPath.Length - relativeLen;
			
			//设置AssetBundle名称
			for(int k = 0;k < dirs.Length;k++)
			{
				string bname = string.Format(formatMagic,module,dirs[k].Name).ToLower();
				SetPackNameInDir(dirs[k],bname,offset,".prefab");
			}
		}
	}


	static private string prefixConfig="/Config/";
	static private string formatConfig="{0}_config_main";
	static private void NamedConfig(string module)
	{
		string versionDir = "/BoTing/"+ module + prefixConfig;
		string fullPath = Application.dataPath + "/" + versionDir + "/";
		int  relativeLen = versionDir.Length + 8;
		if (Directory.Exists(fullPath))
		{
			DirectoryInfo maindir = new DirectoryInfo(fullPath);
			FileInfo[] files = maindir.GetFiles();
			int offset = fullPath.Length - relativeLen;
			string bname = string.Format(formatEffect,module).ToLower();
			
			for(int i = 0;i< files.Length;i++)
			{
				FileInfo fileInfo = files[i];
				if (fileInfo.Name.EndsWith(".csv"))
				{
					string basePath = fileInfo.FullName.Substring(offset).Replace('\\', '/');
					AssetImporter importer = AssetImporter.GetAtPath(basePath);
					
					if (importer && importer.assetBundleName != bname)
					{
						importer.assetBundleName = bname;
						importer.assetBundleVariant="bt";
					}
				}
			}
		}
	}

	
	[MenuItem("GameTools/自动化打包工具/>>自动设置AssetBundle名称和后缀<<")]
	public static void AutoSetBundleName()
	{
		//循环，遍历!!
		for (int i = 0; i < PrefixModules.Length; i++) 
		{
			string module = PrefixModules[i];
			Debug.Log("设置模块AssetBundle名称:"+module);

			NamedIcon(module);
			NamedChatFace(module);
			NamedPackTexture(module);

			NamedAudioBK(module);
			NamedAundioEffect (module);

			NamedModel(module);
			NamedUiPrefab(module);
			NamedMagic(module);

			NamedConfig(module);
		}
		Debug.Log ("自动设置AssetBundle完成！");
	}

	
}