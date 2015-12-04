using BoTing.Framework;
using UnityEngine;

namespace BoTing.GamePublic
{
/// <summary>
///  扩展自TabView,TabWindow是和gui框架相关的tabview。
/// </summary>
public class TabWindow : TabView
{

	/// <summary>
	/// 此处并非传入prefab路径，而是传入注册的view唯一名称
	/// gui框架可以根据名称，自动寻找对应prefab
	/// </summary>
	/// <returns>The tab.</returns>
	/// <param name="viewName">wind名称</param>
	public virtual GameObject CreateTab(string viewName,Transform node)
	{
		GameObject vobj = ViewManager.Instance.DisplayView(viewName,node);
		return vobj;
	}


}

}

