using UnityEngine;
using BoTing.Framework;
using BoTing.GamePublic;
using BoTing.Module;

namespace BoTing.FourBull
{
	public class FourBullScene : Scene
 	{
    	void Awake()
    	{
        	FourBull.Initial();
			//FourBull.ViewManager.DisplayView("FourBull");
        	LoginManager.Instance.OnLoginEvent += OnLoginEvent;
            //登录
        	LoginManager.Instance.Login("vshappy", "111111",new ushort[] { 205 });
    	}

    	void Update()
    	{
            NetWorkManager.Instance.Update();
    	}

    	private void OnLoginEvent(ENUM_LOGIN_EVENT ev, string message)
    	{
        	if (ev == ENUM_LOGIN_EVENT.LOGIN_SUCCESS)
        	{
            	RoomManager.Instance.OnRoomEvent += OnRoomEvent;
                //服务器
            	if (GameList.GameServers.Count > 0)
           	 	{
                	tagGameServer srv = GameList.GameServers[0];
                	tagGameKind kind = GameList.GameKinds.Find(f => { return f.wKindID == srv.wKindID; });
                	RoomManager.Instance.EnterRoom(srv, kind);
                	DebugKit.Log("FourBull", "FourBull.OnLoginEvent: Enter Room. KindID:" + kind.wKindID.ToString() +
                                     " port:" +  srv.wServerPort.ToString() );
            	}
            	else
            	{
                    DebugKit.LogError("没有游戏房间 room is not exist!");
                }
        	}
    	}

    	private void OnRoomEvent(RoomEvent ev)
    	{
            switch (ev.ev)
            {
                case ENUM_ROOM_EVENT.SHOW_ROOM:
                    DebugKit.Todo(" ===== ShowHand.OnRoomEvent:  ====");
                    FourBull.ClientManager.GetGameClient(FourBull.ModuleName).OnInitRoom(ev.Target);
                    break;
                case ENUM_ROOM_EVENT.SHOW_ERROR:
                    break;
            }
        }
	}
}
