/**
 * Copyright (c) Killliu
 * All rights reserved.
 * 
 * 文件名称：KeywordReplace
 * 简    述：
 * 创建时间：2015年09月06日16:38:05
 * 创 建 人：Killliu
 * 修改描述：
 * 修改时间：
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{
	/*
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo") return;
        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#TIME#", System.DateTime.Now.ToString("yyyy年MM月dd日HH:mm:ss"));
        file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
        file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
    */


}