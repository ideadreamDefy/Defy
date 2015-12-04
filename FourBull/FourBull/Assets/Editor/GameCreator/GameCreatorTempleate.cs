
public class GameCreatorTempleate
{
	public static string LogicStr=@"using UnityEngine;

namespace BoTing.#1
{
	public class #1Logic
	{
		
	}

}
";


	public static string CommandStr=@"using UnityEngine;
using BoTing.Framework;
using BoTing.Module;

namespace BoTing.#1
{
	public class #1Command: GameClient
	{
    	/// 表示view界面被销毁
    	private  void RegisterNetwork()
    	{
			//在此添加你游戏事件的监听	
    	}


		private void  UnRegisterNetwork()
		{
			//在此注销你对游戏事件的监听
		}

    	/// 表示view界面被创建
    	public override void OnAttachedView( )
    	{
			RegisterNetwork();

			//do your initial		
		}
	

    	/// 表示view界面被销毁
    	public override void OnReleasedView()
    	{
			UnRegisterNetwork();
			

			//do your collegtion		
    	}

		#region GameLoop
		//游戏初始化
		protected override void OnInitPlayersInfo()
    	{
			
    	}


		//场景事件包，区分游戏的状态进行解包
		protected override void OnSceneEvent(object buffer)
    	{
			
    	}


		//当有玩家进入被调用
    	protected override void OnPlayerEnter(IPlayer player,ushort chairID, bool isLookOn)
    	{
			
    	}

		//当有玩家准备好被调用
   		protected override void OnPlayerReady(IPlayer target, ushort chairID)
    	{

    	}
	
		//当有玩家离开被调用
    	protected override void OnPlayerLeave(IPlayer player,ushort chairID, bool isLookOn)
    	{
			
    	}
	
		//当有玩家分数改变被调用
 		protected override void OnScroeChange(IPlayer player)
    	{

    	}
	
		//当游戏开始被调用,大厅直接开始
		protected override void OnPlayerGameStart(IPlayer player)
    	{
			
    	}
	
		//当游戏结束被调用
		protected override void OnPlayerGameOver(IPlayer player)
    	{
			
    	}
	
		//当游戏断线被调用
    	protected override void OnPlayerOffline(IPlayer player, ushort chairID, bool isBack)
    	{
			
    	}
 		#endregion

    	private static #1Command instance = null;
    	public static #1Command Instance
    	{
        	get
        	{
            	if (instance == null)
                	instance = new #1Command();
            	return instance;
        	}
    	}
	}
}
";


	public static string WindowStr = @"using BoTing.Framework;

namespace BoTing.#1
{
	public class #1Window : View
	{
		private GameLayout  mViewLayout;
		private #1Command mCommand;

	
		//一般此处为自动生成，主要解析ui。先于OnInitialView执行
    	public override void OnCreatedView()
    	{
			mViewLayout = transform.GetComponent<GameLayout>();
    	}
	
		//界面ui已经创建好，准备初始化。后于OnCreateView执行
		public override void OnInitialView()
    	{
			base.OnInitialView();
			mCommand = mViewCommand as #1Command;

			//do your job
    	}
	}
}

";


	static public string InterfaceStr = @"
namespace BoTing.#1
{
	//用于定义commander向view发送消息
	public interface I#1
	{










	}

}

";
	static public string EventDefStr = @"
namespace BoTing.#1
{
	//用于定义commander向view发送消息
	public class #1Event
	{
		//public const string GameCreated =#2#1_GAME_CREATED#2;
		



		
	}

}";

}