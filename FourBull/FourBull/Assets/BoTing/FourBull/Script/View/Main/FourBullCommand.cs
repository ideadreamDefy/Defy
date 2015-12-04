using UnityEngine;
using BoTing.Framework;
using BoTing.Module;
using System;

namespace BoTing.FourBull
{
    public class FourBullCommand: GameClient
	{
        /// <summary>
        /// 网络解包派发数据中心
        /// </summary>
        /// ------------------------------------------------------------------------------------------////
        private FourBullNetCenter netCenter = new FourBullNetCenter();
        /// ------------------------------------------------------------------------------------------////
        /// 
        /// 表示view界面被创建
        public override void OnAttachedView( )
    	{
			RegisterNetwork();
            for (int i = 0; i < FourBullGlobalConst.playerCount; i++)
            {
                IPlayer playerInfo = this.GetPlayerByClientChairID((ushort)i);
                
                if (playerInfo != null)
                {
                    sitDownPosInTable(playerInfo, i);
                }
            }
            //如果是自己的
            //开始倒计时
            FourBull.Messager.Broadcast(FourBullEvent.InRoomClockStart);
            //do your initial		
            base.OnAttachedView();
        }
	

    	/// 表示view界面被销毁
    	public override void OnReleasedView()
    	{
			UnRegisterNetwork();
            //this.SendGamePacket
            //do your collegtion	
            base.OnReleasedView();
        }


    	/// 表示view界面被销毁
    	protected  void RegisterNetwork()
    	{
            //开始叫庄
            AddGameListener(this, netCenter.WhoCallBanker, (ushort)PROTOCOL_PACKET.SUB_S_CALL_BANKER, typeof(CMD_S_CallBanker));
            //开始下注
            AddGameListener(this, netCenter.WhoStartBet, (ushort)PROTOCOL_PACKET.SUB_S_GAME_START, typeof(CMD_S_GameStart));
            //开始发牌
            AddGameListener(this, netCenter.startPoker, (ushort)PROTOCOL_PACKET.SUB_S_SEND_CARD, typeof(CMD_S_SendCard));
            //加注结果
            AddGameListener(this, netCenter.BetResult, (ushort)PROTOCOL_PACKET.SUB_S_ADD_SCORE, typeof(CMD_S_AddScore));
            //翻牌结果
            AddGameListener(this,netCenter.turnPoker, (ushort)PROTOCOL_PACKET.SUB_S_OPEN_CARD, typeof(CMD_S_Open_Card));
            //结算面板
            AddGameListener(this, netCenter.showResult, (ushort)PROTOCOL_PACKET.SUB_S_GAME_END, typeof(CMD_S_GameEnd));
            //锁桌界面断线
            AddGameListener(this,netCenter.offlineOnReadyScene,(ushort)PROTOCOL_PACKET.GS_TK_FREE,typeof(CMD_S_StatusFree));
            //叫庄状态断线
            AddSceneListener(this, netCenter.offlineOnCallBankScene, (byte)PROTOCOL_PACKET.GS_TK_CALL, typeof(CMD_S_StatusCall));
            //下注状态短线
            AddSceneListener(this, netCenter.offlineOnBetScene, (byte)PROTOCOL_PACKET.GS_TK_SCORE, typeof(CMD_S_StatusScore));
            //游戏玩的时候掉线
            AddSceneListener(this, netCenter.offlineOnPlayingScene, (byte)PROTOCOL_PACKET.GS_TK_PLAYING, typeof(CMD_S_StatusPlay));
        }


        protected  void  UnRegisterNetwork()
		{
            //在此注销你对游戏事件的监听
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_CALL_BANKER);
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_GAME_START);
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_SEND_CARD);
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_ADD_SCORE);
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_OPEN_CARD);
            RemoveGameListener(this, (ushort)PROTOCOL_PACKET.SUB_S_GAME_END);
            RemoveSceneListener(this, (byte)PROTOCOL_PACKET.GS_TK_FREE);
            RemoveSceneListener(this, (byte)PROTOCOL_PACKET.GS_TK_CALL);
            RemoveSceneListener(this, (byte)PROTOCOL_PACKET.GS_TK_PLAYING);
        }

		#region GameLoop
		//游戏初始化
        protected override void OnGameClientReady()
        {
            FourBull.ViewManager.DisplayView(FourBull.ModuleName,FourBull.ViewManager.GameTransform);
        }

        protected override void OnGameClientClose()
        {
            FourBull.ViewManager.CloseView(FourBull.ModuleName);
        }



        //当有玩家进入被调用
        protected override void OnPlayerEnter(IPlayer player, ushort chairID, bool isLookOn)
    	{
            //当时观看者时
            if (isLookOn)
            {

            }
            else {
                sitDownPosInTable(player,(int)chairID);
            }
    	}

        //入座
        private void sitDownPosInTable(IPlayer player,int pos)
        {
            switch (pos)
            {
                case 0: 
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroPlayerInfo, player);//将第0个位置的人的信息发送给界面组件
                    break;
                case 1:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosOnePlayerInfo, player);//将第1个位置的人的信息发送给界面组件
                    break;
                case 2:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoPlayerInfo, player);//将第2个位置的人的信息发送给界面组件
                    break;
                case 3:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosThreePlayerInfo, player);//将第3个位置的人的信息发送给界面组件
                    break;
                default:
                    break;
            }
        }

		//当有玩家准备好被调用
   		protected override void OnPlayerReady(IPlayer target, ushort chairID)
    	{
            switch ((int)chairID) {
                case 0:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroIsReady);
                    break;
                case 1:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosOneIsReady);
                    break;
                case 2:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoIsReady);
                    break;
                case 3:
                    FourBull.Messager.Broadcast(FourBullEvent.TablePosThreeIsReady);
                    break;
                default:
                    break;
            }
    }
	
		//当有玩家离开被调用
    	protected override void OnPlayerLeave(IPlayer player, ushort chairID, bool isLookOn)
    	{
            if (isLookOn)
            {

            }
            else {
                switch ((int)chairID) {
                    case 0:
                        FourBull.Messager.Broadcast<IPlayer>(FourBullEvent.TablePosZeroPlayerLeave, player);
                        break;
                    case 1:
                        FourBull.Messager.Broadcast<IPlayer>(FourBullEvent.TablePosOnePlayerLeave, player);
                        break;
                    case 2:
                        FourBull.Messager.Broadcast<IPlayer>(FourBullEvent.TablePosTwoPlayerLeave, player);
                        break;
                    case 3:
                        FourBull.Messager.Broadcast<IPlayer>(FourBullEvent.TablePosThreePlayerLeave, player);
                        break;
                    default:
                        break;

                }
            }
    	}
        //开始按钮点击
        //private void 

	
		//当有玩家分数改变被调用
 		protected override void OnScoreChange(IPlayer player, ushort chairID)
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

    	private static FourBullCommand instance = null;
    	public static FourBullCommand Instance
    	{
        	get
        	{
            	if (instance == null)
                	instance = new FourBullCommand();
            	return instance;
        	}
    	}
	}
}

