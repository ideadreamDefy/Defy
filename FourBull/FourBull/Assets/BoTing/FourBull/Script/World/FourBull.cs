using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.FourBull
{
	public class FourBull
	{ 
		/// <summary>
		/// 私有模块目录名称
		/// </summary>
		public static string  ModuleName = "FourBull";
	
		/// <summary>
		///共有模块目录名称 
		/// </summary>
		private static string  PublicName = "GamePublic";


		/// <summary>
		/// 游戏内部消息中心 
		/// </summary>
		private static Messager mMessager;
		public static Messager Messager
		{
			get{ return mMessager; }
		}

		/// <summary>
		/// UI管理器
		/// </summary>
		private static ViewManager mViewManager;
		public static ViewManager ViewManager
		{
			get{return mViewManager;}
		}
	
		/// <summary>
		/// 场景管理器
		/// </summary>
		private static SceneManager mSceneManager;
		public static SceneManager SceneManager
		{
			get{return mSceneManager;}
		}
	
	
		/// <summary>
		/// 资源加载器，用于加载GamePublic目录下的资源
		/// </summary>
		private static AssetHelper mPublicLoader;
		public static  AssetHelper PublicLoader
		{
			get{return mPublicLoader;}
		}

		/// <summary>
		/// 资源加载器，用于加载私有目录，即本游戏目录FourBull下的资源
		/// </summary>
		private static AssetHelper mPrivateLoader;
		public static  AssetHelper PrivateLoader
		{
			get{return mPrivateLoader;}
		}

	
		/// <summary>
		/// 音频管理器
		/// </summary>
		private static AudioManager mAudioManager;
		public static AudioManager AudioManager
		{
			get{return mAudioManager;}
		}
	
		/// <summary>
		/// 小游戏管理器
		/// </summary>
		private static GameClientManager mClientManager;
		public static GameClientManager ClientManager
		{
			get{return mClientManager;}
		}


    	//Regist your client
		private static void RegisterClient()
    	{
        	mClientManager.RegisterClient("FourBull",FourBullCommand.Instance);
    	}


    	//Regist your view!
    	private static void RegisterView()
   		{
        	mViewManager.RegisterView("FourBull","FourBull","Main",0,FourBullCommand.Instance);
    	}

    	public static void Initial()
   		{
			RegisterClient();
        	RegisterView();
    	}


		//获取管理器单例
		static FourBull()
		{
			mPublicLoader = new AssetHelper (PublicName);
			mPrivateLoader = new AssetHelper (ModuleName);
			mMessager = new Messager ();
			mViewManager = ViewManager.Instance;
			mAudioManager = AudioManager.Instance;
			mSceneManager = SceneManager.Instance;
			mClientManager = GameClientManager.Instance;
		}
	}
}

