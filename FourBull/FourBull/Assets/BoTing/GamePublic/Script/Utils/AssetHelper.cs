using UnityEngine;
using System.Collections;
using BoTing.Framework;

namespace BoTing.GamePublic
{

//与小游戏相关的管理器，方便小游戏进一步内部管理资源,记载资源的时候可以去掉prefix
public class AssetHelper 
{
	/// <summary>
	/// 小游戏模块名称
	/// </summary>
	private string mModuleName;

	/// <summary>
	///资源加载管理器 
	/// </summary>
	private IAssetLoader mAssetLoader;


	/// <summary>
	/// 传入模块名称，构造
	/// </summary>
	public AssetHelper(string module)
	{
		mModuleName = module;
		mAssetLoader = AssetManager.AssetLoader;
	}


	/// <summary>
	/// <para> 用途:加载角色头像</para> 
	/// <para> 参数:prefix传入游戏名称，name传入头像名称</para>
	/// <para> 路径:Asset/BoTing/[prefix]/Atlas/Icon/[path]/[name].png</para>
	/// </summary>
	public Sprite LoadIcon (string path,string name)
	{
		return mAssetLoader.LoadIcon (mModuleName,path,name);
	}
	
	
	/// <summary>
	/// <para> 用途:加载一个聊天表情</para>
	/// <para> 参数:prefix传入游戏名称，name传入表情分类，index传入表情序号</para> 
	/// <para> 路径:Assets/BoTing/[prefix]/Atlas/Face/[name]/[index].png"</para> 
	/// </summary>
	public Sprite  LoadFace(string name, int index)
	{
		return mAssetLoader.LoadFace (mModuleName,name,index);
	}
	
	
	/// <summary>
	/// <para> 用途:加载一张扑克牌</para> 
	/// <para> 参数:prefix传入游戏名称，name传入卡牌名称</para> 
	/// <para> 路径:Asset/BoTing/PublicResouce/PublicResouce/Card/[name].png</para> 
	/// <para> 说明:使用LoadUiSprite方法也可以,但是要手动传入prefix为PublicResouce</para> 
	/// </summary>
	public Sprite LoadCard(string name)
	{
		return mAssetLoader.LoadCard (name);
	}
	
	
	/// <summary>
	/// <para> 用途:加载一张ui相关的图片</para> 
	/// <para> 参数:prefix传入游戏名称，path传入图集所在文件夹名称，name传入图片名称</para> 
	/// <para> 路径:Asset/BoTing/[prefix]/Atalas/Texture/[path]/[name].png"</para> 
	/// </summary>
	public Sprite LoadTexture (string path, string name)
	{
		return mAssetLoader.LoadTexture (mModuleName, path, name);
	}
	
	
	/// <summary>
	/// <para> 用途:加载一首背景音乐</para>
	/// <para> 参数:prefix传入游戏文件夹名称，name传入背景音乐名称</para>
	/// <para> 路径:Assets/BoTing/[prefix]/Audio/Music/[name].mp3</para>
	/// </summary>
	public AudioClip LoadBGAudio (string name)
	{
		return mAssetLoader.LoadBGAudio (mModuleName,name);
	}
	
	
	/// <summary>
	/// <para> 用途:加载游戏音效</para>
	/// <para> 参数:prefix传入游戏名称，name传入音效名称</para>
	/// <para> 路径:Assets/BoTing/[prefix]/Audio/Effect/[name].mp3</para>
	/// </summary>
	public AudioClip LoadEffectAudio (string name)
	{
		return mAssetLoader.LoadEffectAudio (mModuleName,name);
	}
	
	
	/// <summary>
	///	<para> 用途:加载界面相关prefab</para>
	/// <para> 参数:prefix传入游戏名称，path传入界面prefab所在文件夹名称，name传入prefab名称</para>
	/// <para> 路径:Assets/BoTing/[prefix]/Builds/View/[path]/[name].prefab</para>
	/// <para> 注意:每个界面prefab的文件夹会打成一个AssetBundle!因为他们之间被认为依赖较深</para>
	/// </summary>
	public GameObject LoadUiPrefab (string path, string name)
	{
		return mAssetLoader.LoadUiPrefab (mModuleName,path,name);
	}
	
	
	/// <summary>
	///	<para>用途:加载模型相关prefab</para>
	/// <para>参数:prefix传入游名称，path传入模型prefab所在文件夹名称，name传入prefab名称</para>
	/// <para>路径:Assets/BoTing/[prefix]/Builds/Model/[path]/[name].prefab</para>
	/// <para>注意:每个模型prefab的文件夹会打成一个AssetBundle!因为他们之间被认为依赖较深</para>
	/// </summary>
	public GameObject LoadModelPrefab (string path, string name)
	{
		return mAssetLoader.LoadModelPrefab (mModuleName, path, name);
	}
	
	
	/// <summary>
	///	<para>用途:加载3d粒子特效制作的相关的魔法prefab</para>
	/// <para>参数:prefix传入游戏文件夹名称，path传入魔法prefab文件夹名称，name传入prefab名称</para>
	/// <para>注意:每个魔法prefab的文件夹会打成一个AssetBundle!因为他们之间被认为依赖较深</para>
	/// </summary>
	public GameObject LoadMagicPrefab(string path, string name)
	{
		return mAssetLoader.LoadMagicPrefab(mModuleName,path,name);
	}
	
	
	/// <summary>
	///	<para>用途:加载策划配置表格csv</para>
	/// <para>参数:prefix传入游戏文件夹名称，name传入表格名称</para>
	/// </summary>
	public TextAsset  LoadConfig(string name)
	{
		return mAssetLoader.LoadConfig(mModuleName,name);
	}
	

	/// <summary>
	/// <para>用途:加载帧动画做制作的效果</para>
	/// </summary>
	public AnimationClip LoadAnimation(string path,string name)
	{
		return mAssetLoader.LoadAnimation(mModuleName,path,name);
	}

}


}