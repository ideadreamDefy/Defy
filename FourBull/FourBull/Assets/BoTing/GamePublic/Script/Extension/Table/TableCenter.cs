using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BoTing.GamePublic
{

public class TableCenter
{
    public  static Dictionary<Type, TableEnum> indexMap;
    private static Hashtable tableMap = null;
	public static T QueryTable<T>(int basid)
    {
        TableEnum e = indexMap[typeof(T)];
		if (!tableMap.ContainsKey (e)) {
			Debug.Log("Table "+e +" is not exsist!!");
			return default(T);
		}

		Dictionary<int,T> ditory = tableMap [e] as Dictionary<int,T>;
		if (!ditory.ContainsKey (basid)) {
			Debug.Log("query Table "+e+" failed!,baseid is not exsist!"+basid);
			return default(T);
		}
		return ditory[basid];
	}

    public static Dictionary<int, T> GetTable<T>()
    {
        TableEnum e = indexMap[typeof(T)];
        if (!tableMap.ContainsKey(e))
        {
            Debug.Log("Table " + e + " is not exsist!!");
            return null;
        }
        return tableMap[e] as Dictionary<int,T>;
    }

    public static void Initial()
    {
        if (tableMap != null) return;
        //加载表格
        DateTime  startTime = DateTime.Now;

        tableMap = new Hashtable();
        indexMap = new Dictionary<Type, TableEnum>();

        TableLoader tb = new TableLoader();
        tb.loadAllTable(tableMap);
        tb.BindTableType(indexMap);

        Debug.Log("Loading Table Time:" + (DateTime.Now - startTime).TotalSeconds );
    }

	//for test!!
	static TableCenter()
	{
		TableCenter.Initial();
	}

#if DEBUG
    public static void test()
    {


        //查找Item表，表头id为10003的行，结果以ItemConfig方式返回
       // TestConfig c = TableCenter.queryTable<TestConfig>(1020000);
       // Debug.Log("test 0 "+c.Id+","+c.Name+","+c.Descript);
        /**占位符7**/
	}
#endif

}

}

