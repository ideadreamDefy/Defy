
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Collections;
using BoTing.Framework;

public class TableLoader
{
    public Dictionary<TableEnum, string> nameMap;
    public Dictionary<Type, TableEnum> indexMap;

    public TableLoader()
    {
        nameMap = new Dictionary<TableEnum, string>();
        indexMap = new Dictionary<Type, TableEnum>();

        BindTableName(nameMap);
        BindTableType(indexMap);
    }

    public void BindTableName(Dictionary<TableEnum, string> dict)
    {
        dict.Clear();
        dict[TableEnum.GAMETYPECONFIG] = "GameTypeConfig";

    }
	
    public void  BindTableType(Dictionary<Type, TableEnum> dict)
    {
        dict.Clear();
        dict[typeof(GameTypeConfig)]=TableEnum.GAMETYPECONFIG;

    }

 
    public void loadTable<T>(Hashtable tableMap)
    {
     	TableEnum e = indexMap[typeof(T)];
        if(e == TableEnum.NOTEXSIST ){
            Debug.Log("Table.loadTable Failed!,TableEnum:" + e + "is not exsist!!!");
            return;
        }

        string path =  nameMap[e];
		TextAsset binAsset = AssetManager.AssetLoader.LoadConfig("GamePublic",path);
        if (binAsset == null)
        {
            Debug.Log("Table.loadTable Failed!,File:" + path);
            return;
        }
        string[] lines = binAsset.text.Split("\n"[0]);
        Dictionary<int, T> dictory = new Dictionary<int, T>();
		int ilenth = lines.Length;

        //陆芒脦枚鲁枚卤铆脥路
        string[] attrs = lines[0].Trim('\r').Split(',');
        int attlen = attrs.Length;

        //赂鲁脰碌
        Type t = typeof(T);
        for (int i = 1; i < ilenth; i++)
        {
            string linestr = lines[i];
			if (linestr == "\n" || linestr == ""|| linestr == "\r") break;
			string[] values = linestr.Trim('\r').Split(',');
			if(values == null || values.Length <= 0 )break;
			if(values[0] == "" || values[0] == "0")break;
			if(values.Length < attlen)break;

            T o = System.Activator.CreateInstance<T>();
            int key = 0;
            for (int k = 0; k < attlen; k++)
            {
                //Debug.Log("new value:" + values[k] +",field:"+ attrs[k]);
                FieldInfo field = t.GetField(attrs[k]);
                //Debug.Log("field222:" + field.ToString() + "," + field.FieldType+","+attrs[k]+","+values[k]);

                Type fildtp =  field.FieldType;
                object v = null;
                if (fildtp == typeof(string)) {
                    v = Convert.ToString(values[k]);
                }
                else if (fildtp == typeof(int)){
                    v = values[k] == "" ? 0 : Convert.ToInt32(values[k]);
                    v = v == null ? 0 : v;
                }
                else if (fildtp == typeof(float)){
                    v = values[k] == "" ? 0 : Convert.ToSingle(values[k]);
                    v = v == null ? 0.0f : v;
                }
                else if (fildtp == typeof(bool)){
                    v = (values[k] == "0") ? false : true;
                }
                else
                { //脙露戮脵赂鲁脰碌
                    if (values[k] == "") v = 0;
                    else{
                        foreach (int ee in Enum.GetValues(fildtp)){
                            if (values[k] == Enum.GetName(fildtp, ee)) v = ee;
                        }
                    }
                }

                if (v == null){
                    Debug.Log("loadTable set value error!!table:" + e + ",filed:" + attrs[k]);
                    return;
                }
                field.SetValue(o, v);
                if (attrs[k] == "Id") key = Convert.ToInt32(values[k]);
            }
            if (key == 0) {
                Debug.Log("TableLoader Failed!!have no key!");
                return;
            }
            dictory.Add(key, o);
        }
        tableMap[e] = dictory;
       // Debug.Log("parse table:"+e+",line num:"+dictory.Count);
    }

	
    public void loadAllTable(Hashtable tableMap)
    {
        //录脫脭脴卤铆赂帽
       loadTable<GameTypeConfig>(tableMap);

    }
}



//generated automatic on 11/28/15 15:55:38