using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BoTing.GamePublic;

public class UiPrefabWindow : EditorWindow 
{
	private static string textShow;
	public static void CreateTextWindow(string text)
	{
		textShow = text;
		//创建窗口
		Rect  wr = new Rect (50,50,1000,800);
		UiPrefabWindow window = (UiPrefabWindow)EditorWindow.GetWindowWithRect (typeof (UiPrefabWindow),wr,true,"小游戏框架生成工具");	
		window.Show();
	}
	

	public void Awake () 
	{
		//InitData ();

	}
	
	//绘制窗口时调用
	void OnGUI () 
	{
		GUILayout.TextArea (textShow);
		if (GUILayout.Button ("复制代码", GUILayout.Width (200))) 
		{
			TextEditor te = new TextEditor();
			te.content = new GUIContent(textShow);
			te.OnFocus();
			te.Copy();
			this.ShowNotification(new GUIContent("代码已复制"));
		}

	}


	void OnInspectorUpdate()
	{
		//Debug.Log("窗口面板的更新");
		//这里开启窗口的重绘，不然窗口信息不会刷新
		this.Repaint();
	}
	
}
