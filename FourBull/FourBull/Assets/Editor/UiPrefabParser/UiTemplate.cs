

public class UiTempLaterStr
{
	static public string  view_template=@"using UnityEngine;
using UnityEngine.UI;
using BoTing.Framework;

namespace BoTing.#7
{
	public class #1Window : View
	{
#3
		//一般此处为自动生成，主要解析ui，不去管即可.
    	public override void OnCreatedView()
    	{
#5	
    	}

		public override void OnInitialView()
    	{
			base.OnInitialView();
		
			//do your job
    	}

#6
	}
}
";

	
	static public string command_template = @"
using UnityEngine;
using System.Collections;
using BoTing.Framework;

namespace BoTing.#5
{
	public class #1Command: ViewCommand
	{
	#2
	
	
		/// <summary>
		/// view只能当接口使用,不能直接调用派生类view的方法，因为派生view
		/// 可能有多个。如手机端的view，pc端的view
		/// </summary>
		public override void OnAttachedView()
		{

		}

	
		/// <summary>
		/// 表示view界面被销毁
		/// </summary>
		public override void OnReleasedView( )
		{

		}



   		public static readonly #1Command Instance
 			= new #1Command();
	}
}

";

	static public string eventer_template = @"
//用于定义commander向view发送消息
namespace BoTing.#5
{
	public interface #1Interface
	{




	}
}
";

	static public string viewprefab_helper=@"
using UnityEngine;
using System.Collections;
using System;

public class UiPrefabHelper : MonoBehaviour 
{
	static public void AddCommpment(GameObject obj)
	{
		if(obj.GetComponent<#1>() == null)
			obj.AddComponent<#1>();
	}
}

";

	static public string viewprefab_helper2=@"
using UnityEngine;
using System.Collections;
using System;

public class UiPrefabHelper : MonoBehaviour 
{
	static public void AddCommpment(GameObject obj)
	{
	}
}

";
	



}