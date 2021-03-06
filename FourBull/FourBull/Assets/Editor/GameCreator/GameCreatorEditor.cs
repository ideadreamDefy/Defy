using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BoTing.GamePublic;

public class GameCreatorEditor : EditorWindow 
{
	private static int CreateType = 0;//0表示创建框架  1表示生成prefab

	[MenuItem("GameTools/小游戏框架工具/生成游戏框架")]
	public static void CreateGameFramwork( )
	{
		//创建窗口
		Rect  wr = new Rect (100,50,800,600);
		GameCreatorEditor window = (GameCreatorEditor)EditorWindow.GetWindowWithRect (typeof (GameCreatorEditor),wr,true,"小游戏框架生成工具");	
		window.Show();
		CreateType = 0;
	}

	//[MenuItem("GameTools/小游戏框架工具/2.生成框架预设")]
	public static void CreateGamePrefab( )
	{
		//创建窗口
		Rect  wr = new Rect (100,50,800,600);
		GameCreatorEditor window = (GameCreatorEditor)EditorWindow.GetWindowWithRect (typeof (GameCreatorEditor),wr,true,"小游戏框架生成工具");	
		window.Show();
		CreateType = 1;
	}

	public void InitData()
	{
		string strtemp = PlayerPrefs.GetString ("CreateGameTemp");
		if (string.IsNullOrEmpty (strtemp))
			strtemp = "";

		gameList.Clear();
		Dictionary<int,GameTypeConfig> dict = TableCenter.GetTable<GameTypeConfig> ();
		foreach (GameTypeConfig v in dict.Values) 
		{
			gameList.Add(v);
		}

		mGameStatus = new bool[gameList.Count];
		for (int i = 0; i<gameList.Count; i++) 
		{
			if(strtemp.Contains("_"+gameList[i].GameName))
				mGameStatus[i] = true;
		}
	}


	List<GameTypeConfig> gameList = new List<GameTypeConfig>();
	bool[] mGameStatus;
	public void Awake () 
	{
		//InitData ();
	}
	
	//绘制窗口时调用
	void OnGUI () 
	{
		if (gameList.Count == 0) 
		{
			InitData();
		}

		if (gameList.Count == 0) 
		{
			return;
		}

		CreateAxisCheckboxes ("请选择游戏类型");

		//if (CreateType == 0) 
		{
			if (GUILayout.Button ("生成游戏", GUILayout.Width (200))) {

				string savedStr = "";
				int icouter = 0;
				//关闭窗口
				for (int i = 0; i < mGameStatus.Length; i++) {
					if (mGameStatus [i]) {
						Debug.Log ("Game:" + gameList [i].KindName);
						savedStr += "_"+gameList [i].GameName;
						GameCreator.CreateGame (gameList [i].GameName, gameList [i].Id);
						icouter ++;
					}
				}

				if(icouter == 0)
					EditorUtility.DisplayDialog("注意","没有勾选任何游戏!","确认");

				//保存
				PlayerPrefs.SetString("CreateGameTemp",savedStr);
			}
		}

		//else
		{
			//GUILayout.TextArea("需要等待第1步生成的脚本编译完成，可能需要等待一会！请检查确保生成成功！",GUILayout.Width(200));
	
			if (GUILayout.Button ("生成预设", GUILayout.Width (200))) {

				int icouter = 0;
				//关闭窗口
				for (int i = 0; i < mGameStatus.Length; i++) {
					if (mGameStatus [i]) {
						icouter ++;
						Debug.Log ("Game:" + gameList [i].KindName);

						GameCreator.CreatePrefab (gameList [i].GameName);
					}
				}

				if(icouter == 0)
					EditorUtility.DisplayDialog("注意","没有勾选任何游戏!","确认");
				else
					Debug.Log("Create Prefab Succeed!");
			}


		}
	}

	
	private void CreateAxisCheckboxes(string operationName)  
	{  
		GUILayout.Label(operationName, EditorStyles.boldLabel);  
		EditorGUILayout.Space();

		GUILayout.BeginVertical();  
		for(int i = 0;i<gameList.Count;i++)
		{
			GameTypeConfig c = gameList[i];
			mGameStatus[i] = GUILayout.Toggle(mGameStatus[i],string.Format("{0} ({1})",c.KindName,c.GameName),GUILayout.Height(20)); 
		}

		GUILayout.EndHorizontal();  
		EditorGUILayout.Space();  
	}  


	//更新
	void Update()
	{
		
	}
	
	void OnFocus()
	{
		//Debug.Log("当窗口获得焦点时调用一次");
	}
	
	void OnLostFocus()
	{
		//Debug.Log("当窗口丢失焦点时调用一次");
	}
	
	void OnHierarchyChange()
	{
		//Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
	}
	
	void OnProjectChange()
	{
		//Debug.Log("当Project视图中的资源发生改变时调用一次");
		//InitData ();
	}
	
	void OnInspectorUpdate()
	{
		//Debug.Log("窗口面板的更新");
		//这里开启窗口的重绘，不然窗口信息不会刷新
		this.Repaint();
	}
	
	void OnSelectionChange()
	{
		//当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
		foreach(Transform t in Selection.transforms)
		{
			//有可能是多选，这里开启一个循环打印选中游戏对象的名称
			//Debug.Log("OnSelectionChange" + t.name);
		}
	}
	
	void OnDestroy()
	{
		//Debug.Log("当窗口关闭时调用");
	}
}