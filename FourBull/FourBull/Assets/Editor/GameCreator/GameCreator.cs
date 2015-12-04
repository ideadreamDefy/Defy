using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using BoTing.Framework;
using System.Reflection;
using BoTing.GamePublic;


public class GameCreator : MonoBehaviour 
{
	//基础文件夹路径
	static string PrefixPath = Application.dataPath + "/BoTing/";


	//基础文件夹结构
	static private string[] mGameDicts = {
		"Animation",
		"Atlas",
		"Atlas/Texture",
		"Atlas/Texture/Background",
		"Atlas/Texture/UiAtals",
		"Atlas/Icon",
		"Audio",
		"Audio/Effect",
		"Audio/Music",
		"Builds",
		"Builds/Magic",
		"Builds/Model",
		"Builds/View",
		"Builds/View/Main",
		"Builds/View/Victory",
		"Builds/View/SideBar",
		"Config",
		"Scene",
		"Script",
		"Script/Define",
		"Script/Scene",
		"Script/View",
		"Script/View/Main",
		"Script/View/Victory",
		"Script/View/SideBar",
		"Script/View/Control",
		"Script/World"
	};


	static public void CreateGame(string gameName,int kindId)
	{
		string path = PrefixPath + gameName;
		string moduleName = gameName;
		DirectoryInfo outdir =new DirectoryInfo(path);
		if (outdir.Exists) 
		{
			System.DateTime nowTime = System.DateTime.Now;
			string afterfix = "_"+nowTime.Year+nowTime.Month+nowTime.Day+nowTime.Hour+nowTime.Minute+nowTime.Second;
			moduleName += afterfix;
			Debug.Log("注意,该模块的文件夹已经存在,请检查是否重复！模块："+path);
		} 

		//创建基础文件夹
		CreateDir (moduleName,"");

		//创建文件夹
		for (int i = 0; i < mGameDicts.Length; i++) 
		{
			CreateDir(moduleName,mGameDicts[i]);
		}


		//拷贝基础场景
		string fromPath = Application.dataPath +"/Editor/GameCreator/Template/SceneTemplate.unity";
		string toPath = PrefixPath + moduleName + "/Scene/" + moduleName + ".unity";
		File.Copy(fromPath,toPath);


		//生成场景注册脚本
		string sceneReadPath = Application.dataPath +"/Editor/GameCreator/Template/ClientRegisterTemplate.txt";
		string sceneSavePath = PrefixPath + moduleName +"/Script/World/"+moduleName+".cs";
		string sceneText = File.ReadAllText (sceneReadPath);
		sceneText = sceneText.Replace ("#1",moduleName);
		WriteFile(sceneSavePath,sceneText,true);

		//生成场景脚本
		string sceneReadPath2 = Application.dataPath +"/Editor/GameCreator/Template/SceneTemplate.txt";
		string sceneSavePath2 = PrefixPath + moduleName +"/Script/Scene/"+moduleName+"Scene.cs";
		string sceneText2 = File.ReadAllText (sceneReadPath2);
		sceneText2 = sceneText2.Replace ("#1",moduleName);
		sceneText2 = sceneText2.Replace ("#2",kindId.ToString());
		WriteFile(sceneSavePath2,sceneText2,true);


		//生成小游戏脚本,逻辑脚本 xxlogic.cs
		string sceneSavePath3 = PrefixPath + moduleName +"/Script/View/Main/"+moduleName+"Logic.cs";
		string sceneText3 = GameCreatorTempleate.LogicStr;
		sceneText3 = sceneText3.Replace ("#1",moduleName);
		WriteFile(sceneSavePath3,sceneText3,true);


		//生成小游戏脚本,接口脚本 xxEvent.cs
		string sceneSavePath4 = PrefixPath + moduleName +"/Script/Define/"+moduleName+"Event.cs";
		string sceneText4 = GameCreatorTempleate.EventDefStr;
		sceneText4 = sceneText4.Replace ("#1",moduleName);
		sceneText4 = sceneText4.Replace ("#2","\"");
		WriteFile(sceneSavePath4,sceneText4,true);

		//生成小游戏window
		string sceneSavePath5 = PrefixPath + moduleName +"/Script/View/Main/"+moduleName+"Command.cs";
		string sceneText5 = GameCreatorTempleate.CommandStr;
		sceneText5 = sceneText5.Replace ("#1",moduleName);
		WriteFile(sceneSavePath5,sceneText5,true);


		//生成小游戏command
		string sceneSavePath6 = PrefixPath + moduleName +"/Script/View/Main/"+moduleName+"Window.cs";
		string sceneText6 = GameCreatorTempleate.WindowStr;
		sceneText6 = sceneText6.Replace ("#1",moduleName);
		WriteFile(sceneSavePath6,sceneText6,true);


		EditorUtility.DisplayProgressBar ("请稍候", "正在编译脚本，请稍候", 1f);
		AssetDatabase.Refresh ();
		EditorApplication.OpenScene (toPath);
	}

	[UnityEditor.Callbacks.DidReloadScripts]
	static public void OnClearProgress()
	{
		EditorUtility.ClearProgressBar ();
	}

	static public void CreatePrefab(string gameName)
	{
		string path = PrefixPath + gameName;
		DirectoryInfo outdir =new DirectoryInfo(path);
		if ( !outdir.Exists ) 
		{
			EditorUtility.DisplayDialog("注意","小游戏框架尚未生成！请先生成游戏框架！","知道了");
			return;
		} 

		var assembly = typeof(UIUtil).Assembly;

		//添加xxxScene
		GameObject worldObj = GameObject.Find ("World");
		if (assembly!= null && worldObj != null) {
			Type t = assembly.GetType ("BoTing."+gameName +"."+gameName + "Scene");

			if(t!= null && worldObj.GetComponent(t) == null)
			{
				worldObj.AddComponent (t);
				Debug.Log ("已经为World添加脚本:"+gameName+"Scene");
			}
		}

		//添加xxxWindow
		GameObject panelObj = GameObject.Find ("Canvas/MIDDLE/GameLayout");
		if (panelObj != null) {
			Type t = assembly.GetType ("BoTing."+gameName +"."+gameName + "Window");
			
			if(t!= null && panelObj.GetComponent(t) == null)
			{
				panelObj.AddComponent (t);
				Debug.Log ("已经为根节点添加脚本:"+gameName+"Window");
			}
		}

		//制作prefab,要判断是否存在
		string viewprefabTemp = "/BoTing/" + gameName + "/Builds/View/Main/"+gameName+".prefab";
		string viewprefabPath = Application.dataPath+ viewprefabTemp;
		if ( File.Exists(viewprefabPath) )
		{
			EditorUtility.DisplayDialog("注意","目标文件夹预制文件已存在！生成预制失败！\n预制名称:"+gameName,"知道了");
			return;
		}

		//生成基础界面预制
		string viewprefabName = "Assets" + viewprefabTemp;
		PrefabUtility.CreatePrefab(viewprefabName,panelObj,ReplacePrefabOptions.ConnectToPrefab);
		Debug.Log ("已经将选中的模块生成了prefab！");

		//从界面移除
		GameObject.DestroyImmediate (panelObj);
		EditorApplication.SaveScene ();
		AssetDatabase.Refresh ();
	}


	
	static public void CreateDir(string mudoleName,string dirName)
	{
		string path= PrefixPath + mudoleName +"/" + dirName;
		
		//如果文件夹存在，则文件夹名称后面+时间戳.防止对当前文件夹内容造成影响
		DirectoryInfo outdir =new DirectoryInfo(path);
		string rpath = path;
		if (outdir.Exists) 
		{
			System.DateTime nowTime = System.DateTime.Now;
			string afterfix = "_"+nowTime.Year+nowTime.Month+nowTime.Day+nowTime.Hour+nowTime.Minute+nowTime.Second;
			rpath += afterfix;
			Debug.LogError("注意,该模块的文件夹已经存在,请检查是否重复！模块："+dirName);
			EditorUtility.DisplayDialog("模块名称重复","目标路径已经存在相同模块，名称重复！文件夹自动更名为:"+dirName+afterfix,"我知道了");
		} 
		
		//创建文件夹
		Directory.CreateDirectory(rpath);
		Debug.Log("创建文件夹:"+rpath);
	}


	[MenuItem("GameTools/清除编辑器弹出窗")]
	public static void ClearPopupControl( )
	{
		EditorUtility.ClearProgressBar ();
	}

	//写文件
	static public void WriteFile (string name, string info, bool overwrite)  
	{     
		//文件流信息  
		StreamWriter sw;     
		FileInfo t = new FileInfo (name);     
		if (!t.Exists) {     
			//如果此文件不存在则创建  
			sw = t.CreateText ();     
		} else {     
			if (overwrite)  
				sw = t.CreateText ();  
			else  
				sw = t.AppendText ();     
		}     
		//以行的形式写入信息  
		sw.WriteLine (info);     
		//关闭流  
		sw.Close ();     
		//销毁流  
		sw.Dispose ();     
	}


}
