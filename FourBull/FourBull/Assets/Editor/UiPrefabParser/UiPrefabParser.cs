using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using System;

/*
 * 
public class UiPrefabParser : MonoBehaviour {

//	static string outType = "lua";// lua or csharp
	
	//types
	static private readonly string[] typeNames = {
		"Canvas",
		"Button",
		"Text",
		"Image",
		"RawImage",
		"Slider",
		"ScrollBar",
		"Toggle",
		"InputField",
		"Transform"
		//to be add!!添加你想获取的组件名称
	};

	static private readonly string[] variNames = {
		"canvas",
		"button",
		"text",
		"image",
		"rawimage",
		"slider",
		"scroll",
		"toggle",
		"input",
		"panel"
		//to be add!!添加你想获取的组件名称
	};


    static public string headerTitilStr = @"
#1
    ";


	

    ///Text回调
    static public string textCallBack = @"
    public void Set#1Text(#3string text)
    {
       //#2.text= text;
    }
    ";


    ///Button事件注册
    static public string buttonRegister = @"        #1.onClick.AddListener(#2Clicked);
    ";

	static public string buttonRegisterArray = @"            int ii = #3;        
            #1.onClick.AddListener(delegate ()
            {
               #2Clicked(ii,#1);
            });
	";

    //button事件回调
    static public string buttonCallback1 = @"
    public void  #1Clicked(#2#3)
    {

    }
    ";


    //Image回调
    static public string imageCallBack = @"
    public void Set#1Image(#3string name)
    {
       //#2.sprite = null;
    }
    ";


    //Texture回调
    static public string textureCallBack = @"
    public void Set#1Texture(#3string name)
    {
       //#2.sprite = null;
    }
    ";

    //Slider
    static public string sliderCallback = @"
    public void Set#1Progress(#3float progress)
    {
        //do yourself;
    }        
    ";


	public static void ClearEditorControl () 
	{
		EditorUtility.ClearProgressBar ();

		GameObject canvasobj = Selection.gameObjects.Length > 0 ? Selection.gameObjects[0] : null;
		if (canvasobj == null)
		{
			Debug.Log("视图中没有选中ui根节点,请先选中物体~");
			return;
		}

		canvasobj.AddComponent (System.Type.GetType("RecentPlayBarWindow"));
	}


    static string headerString;
	static string bodyString;
	static string pathString;
    static string funcString;
	
	//--------------------------解析非数组------------------------------
	//如果发现有需要导出的子类控件
	//则必先找到父控件的transform
	static bool HasOuterChild(Transform transf)
	{
		if (transf.name.StartsWith ("_"))
			return true;
		int childCout = transf.childCount;
		for (int i = 0; i<childCout; i++) 
		{
			Transform ts = transf.GetChild(i);
			if(ts.name.StartsWith("_"))
				return true;

			bool bfind = HasOuterChild(ts);
			if( bfind )
				return true;
		}

		return false;
	}


	//transfrom
	private static readonly string ArrayParseStr1=@"
       for(int k#1 =0;k#1 < k#2;k#1++)
       {
          k#3[k#1] = k#4.GetChild(k#1);
       }
";
	
	//compment
	private static readonly string ArrayParseStr2=@"
        for(int k#1 =0;k#1 < k#2;k#1++)
        {
            k#3[k#1] = k#4.GetChild(k#1).GetComponent<k#5>();k#6
        }
";
	
	static void ParseObj(Transform obj,string path,string tsName,string order)
	{
		int childCout = obj.childCount;
		for (int i = 0; i<childCout; i++)
		{
			Transform ts = obj.GetChild(i);
			string  pathname = path +"/"+ts.name;
			string  tschildName = (ts.name+ order + i).Replace(" ","");//移除空格
			if( !HasOuterChild(ts) )continue;
	
			bodyString+=("        Transform "+tschildName+" = "+tsName+".FindChild(\""+ts.name+"\");\n");
			//解析名称
			if(ts.name.StartsWith("_") && !ts.name.StartsWith("__"))
			{
				string[] lines = ts.name.Substring(1).Split('_');
				if(lines == null || lines.Length <= 1)
				{
					DebugKit.Log("未能成功匹配类型和名称！正确格式:_类型名_字段名！name:"+ts.name);
					return;
				}

				//match type
				string temptype = lines[0].ToLower();
				int ifind = -1;
				for(int k = 0;k<variNames.Length;k++)
				{
					string varitemp = variNames[k].ToLower();
					if(temptype.Contains(varitemp))
					{
						ifind = k;
						break;
					}
				}

				if(ifind == -1)
				{
					DebugKit.Log("自动解析ui失败！未找到匹配的组件类型！请检查名称！name:"+ts.name);
					return;
				}

				string vname = lines[0].ToLower()+char.ToUpper(lines[1][0])+lines[1].Substring(1);
				for(int m = 2;m < lines.Length;m++)
				{
					vname += lines[m];
				}
				vname += (""+order+i);
				vname = vname.Replace(" ","");
				headerString += ("    private " + typeNames[ifind] +"  "+vname+";\n");
				if( typeNames[ifind]== "Transform" ) 
					bodyString += ("        "+vname+" = " + tschildName+";\n");
				else
					bodyString += ("        "+vname+" = " +tschildName+".GetComponent<"+typeNames[ifind]+">();\n");

                //注册相关接口
                string newtemp = "";
                if (typeNames[ifind] == "Button")
                {
					string buttonadd = buttonRegister;
                    buttonadd = buttonadd.Replace("#1",vname);
                    buttonadd = buttonadd.Replace("#2",vname);
                    bodyString += buttonadd+"\n";


                    //注册回调
                    newtemp = buttonCallback1;
                    newtemp = newtemp.Replace("#1", vname);
					newtemp = newtemp.Replace("#2","");
					newtemp = newtemp.Replace("#3","");
                }
                else if (typeNames[ifind] == "Text")
                {
                    newtemp = textCallBack;
                    newtemp =  newtemp.Replace("#1", vname);
                    newtemp =  newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "");
                }
                else if (typeNames[ifind] == "Image")
                {
                    newtemp = imageCallBack;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "");
                }
                else if (typeNames[ifind] == "RawImage")
                {
                    newtemp = textureCallBack;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "");
                }
                else if (typeNames[ifind] == "Slider")
                {
                    newtemp = sliderCallback;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "");
                }
                funcString += newtemp + "\n\n";
            }
			else if(ts.name.StartsWith("__"))
			{
				string  sstemp = ts.name.Substring(2);
				string[] lines = sstemp.Split('_');
				if(lines == null || lines.Length <= 1)
				{
					DebugKit.Log("未能成功匹配类型和名称！正确格式:_类型名_字段名！name:"+ts.name);
					return;
				}

				//match type
				string temptype = lines[0].ToLower();
				int ifind = -1;
				for(int k = 0;k<variNames.Length;k++)
				{
					string varitemp = variNames[k].ToLower();
					if(temptype.Contains(varitemp))
					{
						ifind = k;
						break;
					}
				}
				
				if(ifind == -1)
				{
					DebugKit.Log("自动解析ui失败！未找到匹配的组件类型！请检查名称！name:"+ts.name);
					return;
				}

				string vtype = typeNames[ifind];
				string vname = (lines[0].ToLower()+char.ToUpper(lines[1][0])+lines[1].Substring(1)+"s");
//				string vorder = ""+order;
			//	int   typeinx = ifind;
				int  childcout = ts.childCount;

				vname += (""+order+i);
				vname = vname.Replace(" ","");
				headerString += ("     private "+ vtype +"[] " + vname + ";\n");
				bodyString += ("        "+ vname +" = new "+vtype+"["+childcout+"];");
				string bodytemp= "";
				if(vtype == "Transform")
				{
					bodytemp = ArrayParseStr1;
					bodytemp = bodytemp.Replace("k#1","i"+order+i);
					bodytemp = bodytemp.Replace("k#2",""+childcout);
					bodytemp = bodytemp.Replace("k#3",vname);
					bodytemp = bodytemp.Replace("k#4",tschildName);
				}
				else
				{
					bodytemp = ArrayParseStr2;
					bodytemp = bodytemp.Replace("k#1","i"+order+i);
					bodytemp = bodytemp.Replace("k#2",""+childcout);
					bodytemp = bodytemp.Replace("k#3",vname);
					bodytemp = bodytemp.Replace("k#4",tschildName);
					bodytemp = bodytemp.Replace("k#5",vtype);

					//button是特殊的控件
					if(vtype == "Button")
					{
						string buttonitem = vname+"["+"i"+order+i+"]";
						string buttonadd = buttonRegisterArray;
						buttonadd = buttonadd.Replace("#1",buttonitem);
						buttonadd = buttonadd.Replace("#2",vname);
						buttonadd = buttonadd.Replace("#3","i"+order+i);
						bodytemp = bodytemp.Replace("k#6","\n"+buttonadd);
					}
					else
					{
						bodytemp = bodytemp.Replace("k#6","");
					}
				}
				bodyString += bodytemp;

                //注册相关接口
                string newtemp = "";
                if (typeNames[ifind] == "Button")
                {
                    //注册回调
                    newtemp = buttonCallback1;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", "int index,");
					newtemp = newtemp.Replace("#3", "Button go");

                }
                else if (typeNames[ifind] == "Text")
                {
                    newtemp = textCallBack;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "int index,");
                }
                else if (typeNames[ifind] == "Image")
                {
                    newtemp = imageCallBack;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "int index,");
                }
                else if (typeNames[ifind] == "RawImage")
                {
                    newtemp = textureCallBack;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "int index,");
                }
                else if (typeNames[ifind] == "Slider")
                {
                    newtemp = sliderCallback;
                    newtemp = newtemp.Replace("#1", vname);
                    newtemp = newtemp.Replace("#2", vname);
                    newtemp = newtemp.Replace("#3", "int index,");
                }
                funcString += newtemp + "\n\n";
            }

			//find child
			ParseObj(ts,pathname,tschildName,order+i);
		}
	}


	[MenuItem("GameTools/UI自动解析工具/输出ui解析代码")]
	public static void ExecuteCsharpModule()
	{
		headerString = "";
		funcString = "";
		bodyString = "";
		pathString = "";
		
		
		GameObject canvasobj = Selection.gameObjects.Length > 0 ? Selection.gameObjects[0] : null;
		if (canvasobj == null)
		{
			DebugKit.Log("视图中没有选中ui根节点,请先选中物体~");
			return;
		}
		
		string moduleName = canvasobj.transform.name;
		ParseObj(canvasobj.transform,moduleName, "transform", "");


		string context = UiTempLaterStr.view_template;
		context = context.Replace ("#1",moduleName);
		context = context.Replace ("#2",moduleName+"Interface");
		context = context.Replace ("#3",headerString);
		context = context.Replace ("#4", "\""+moduleName+"\"");
		context = context.Replace ("#5", bodyString);
		context = context.Replace ("#6", funcString);

		Debug.Log ("解析ui文件:"+moduleName);
		Debug.Log (context);
	}



	// Use this for initialization
	static private string prepath_of_view = "/Editor/UiPrefabParser/";
	[MenuItem("GameTools/UI自动解析工具/1,生成ui模块文件")]
	public static void ExecuteCsharp () 
	{
		headerString = "";
		funcString = "";
		bodyString = "";
		pathString = "";
		
		
		GameObject canvasobj = Selection.gameObjects.Length > 0 ? Selection.gameObjects[0] : null;
		if (canvasobj == null)
		{
			Debug.Log("视图中没有选中ui根节点,请先选中物体~");
			return;
		}
		string objname = canvasobj.transform.name;

		//解析出模块名称
		string[] temps = objname.Split ('_');
		if (temps == null || temps.Length < 3) 
		{
			EditorUtility.DisplayDialog("警告","层次视图选中的GameObject命名格式不正确!\nGameObject命名格式为:_游戏名称_模块名称!生成失败!","知道了");
			return;
		}


		if (string.IsNullOrEmpty (temps [1]) || string.IsNullOrEmpty (temps [2])) 
		{
			EditorUtility.DisplayDialog("警告","游戏名称或者模块名称为空!\nGameObject命名格式为:_游戏名称_模块名称!生成失败!","知道了");
			return;
		}

	
		//解析出属于哪个模块
		string destinaPath = temps [1];
		string moduleName = temps [2];
		string destinaFull = Application.dataPath + "/BoTing/" + destinaPath;
		DirectoryInfo destinaDir = new DirectoryInfo (destinaFull);
		if (!destinaDir.Exists) 
		{
			EditorUtility.DisplayDialog("警告","输出游戏文件夹为:"+destinaPath+",但Assets/BoTing下不存在该文件夹,请确认!\n请确认格式为_游戏名称_模块名称!","知道了");
			return;
		}

		prepath_of_view = "/BoTing/"+destinaPath+"/Script/View/";
		ParseObj(canvasobj.transform,moduleName, "transform","");
		string context = UiTempLaterStr.view_template;
		context = context.Replace ("#1",moduleName);
		context = context.Replace ("#2",moduleName+"Interface");
		context = context.Replace ("#3",headerString);
		context = context.Replace ("#4", "\""+moduleName+"\"");
		context = context.Replace ("#5", bodyString);
		context = context.Replace ("#6", funcString);

		Debug.Log ("生成ui模块相关文件:"+moduleName);
		CreateUiModuleFile (moduleName, context);

		AssetDatabase.Refresh ();
	}


	[MenuItem("GameTools/UI自动解析工具/2,生成ui相关prefab(绑定view)")]
	static public void WhenScriptReloaded()
	{
		GameObject canvasobj = Selection.gameObjects.Length > 0 ? Selection.gameObjects[0] : null;
		if (canvasobj == null)
		{
			return;
		}

		string objname = canvasobj.transform.name;
		string[] temps = objname.Split ('_');
		if (temps == null || temps.Length < 3 || string.IsNullOrEmpty(temps[1]) || string.IsNullOrEmpty(temps[2]))
			return;

		string destinaPath = temps [1];
		string moduleName = temps [2];
		string destinaFull = Application.dataPath + "/BoTing/" + destinaPath;
		DirectoryInfo destinaDir = new DirectoryInfo (destinaFull);
		if (!destinaDir.Exists) 
		{
			EditorUtility.DisplayDialog("警告","输出游戏文件夹为:"+destinaPath+",但Assets/BoTing下不存在该文件夹,请确认!\n请确认格式为_游戏名称_模块名称!","知道了");
			return;
		}

		var assembly = typeof(World).Assembly;
		Type t = assembly.GetType (moduleName + "Window");
		if (t == null)
			return;

		canvasobj.AddComponent (t);
		Debug.Log ("已经为选中的GameObject添加脚本!");

		//view生成prefab
		string viewprefabTemp = "/BoTing/" + destinaPath + "/Builds/View/" + moduleName;
		string viewprefabPath = Application.dataPath+ viewprefabTemp;
		DirectoryInfo prefabDir = new DirectoryInfo(viewprefabPath);
		if ( !prefabDir.Exists )
		{
			prefabDir.Create();
		}
		
		string viewprefabName = "Assets"+viewprefabTemp + "/" + moduleName + ".prefab";
		PrefabUtility.CreatePrefab(viewprefabName,canvasobj,ReplacePrefabOptions.ConnectToPrefab);
		Debug.Log ("已经将选中的模块生成了prefab！");


		AssetDatabase.Refresh ();
	}


	static public void CreateUiModuleFile(string name,string content)
	{
		string path=Application.dataPath + prepath_of_view + name;

		//如果文件夹存在，则文件夹名称后面+时间戳.防止对当前文件夹内容造成影响
		DirectoryInfo outdir =new DirectoryInfo(path);
		string rpath = path;
		if (outdir.Exists) 
		{
			System.DateTime nowTime = System.DateTime.Now;
			string afterfix = "_"+nowTime.Year+nowTime.Month+nowTime.Day+nowTime.Hour+nowTime.Minute+nowTime.Second;
			rpath += afterfix;
			Debug.LogError("注意,该模块的文件夹已经存在,请检查是否重复！模块："+name);
			EditorUtility.DisplayDialog("模块名称重复","目标路径已经存在相同模块，名称重复！文件夹自动更名为:"+name+afterfix,"我知道了");
		} 

		//创建文件夹
		Directory.CreateDirectory(rpath);
		Debug.Log("创建文件夹:"+rpath);


		//写入view
		string viewpath = rpath + "/" + name + "Window.cs";
		WriteFile (viewpath,content,true);
		Debug.Log ("创建文件:"+viewpath);

		//写入command
		string comdstr = UiTempLaterStr.command_template;
		comdstr = comdstr.Replace ("#1",name);
		comdstr = comdstr.Replace ("#2","");
		comdstr = comdstr.Replace ("#3",name+"Interface");

		string comdpath = rpath + "/" + name + "Command.cs";
		WriteFile (comdpath,comdstr,true);
		Debug.Log ("创建文件:"+comdpath);

		//写入event
		string evtstr = UiTempLaterStr.eventer_template;
		evtstr = evtstr.Replace ("#1",name);


		string evtpath = rpath + "/" + name + "Interface.cs";
		WriteFile (evtpath,evtstr,true);
		Debug.Log ("创建文件:"+evtpath);
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

*/
