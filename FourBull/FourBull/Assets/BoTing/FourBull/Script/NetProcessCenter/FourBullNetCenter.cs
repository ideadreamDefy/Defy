/**
 * Curpyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：NetCenter
 * 简    述：
 * 创建时间：
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
//using unityengine;
using System.Collections;
using System;
using BoTing.Module;
/// <summary>
/// 回调中心
/// </summary>
/// 
namespace BoTing.FourBull
{
    public class FourBullNetCenter
    {
        //--------------------------Tool--------------------------------------
        //拷贝数组
        private void copyArrayFromDataStruct<T>(T[] array, ref T[] arrayContainer)
        {
            int arrayLenth = array.Length;
            if (arrayLenth == 0)
            {
                return;
            }
            for (int i = 0; i < array.Length; i++)
            {
                arrayContainer[i] = array[i];
            }
        }

        //---------------------------Tool--------------------------------------
        public FourBullNetCenter()
        {
            //Console.WriteLine("FourBullNetCenter init Success!!!");
        }

        //用户叫庄 ---------CMD_S_StatusCall--------------------
        public void WhoCallBanker(Object obj)
        {
            //struct CMD_S_CallBanker
            //{
            //    public ushort wCallBanker;                      //叫庄用户 
            //    public bool bFirstTimes;                        //首次叫庄 
            //};
            FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroIsReady);
            CMD_S_CallBanker dataStruct = (CMD_S_CallBanker)obj;
            ushort wCallBanker = dataStruct.wCallBanker;
            bool bFirstTimes = dataStruct.bFirstTimes;
            //空的座位隐藏
            for (int i = 0; i < 4; i++)
            {
                var player = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)i);
                
                if (player == null)
                {
                    switch (i) {
                        case 1:
                                FourBull.Messager.Broadcast(FourBullEvent.hidePlayerOne);
                                break;
                        case 2:
                                FourBull.Messager.Broadcast(FourBullEvent.hidePlayerTwo);
                                break;
                        case 3:
                                FourBull.Messager.Broadcast(FourBullEvent.hidePlayerThree);
                                break;
                        default:
                                break;
                    }
                }
            }

            //移动玩家Icon
            FourBull.Messager.Broadcast(FourBullEvent.BtnStartMoveIcon);
            //叫庄状态更新
            if (wCallBanker == FourBullCommand.Instance.Self.ChairID)
            {
                FourBull.Messager.Broadcast(FourBullEvent.StartCallZhuang);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }else if (wCallBanker == FourBullCommand.Instance.SwitchViewChairID(1))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, true);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }
            else if(wCallBanker == FourBullCommand.Instance.SwitchViewChairID(2))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, true);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }
            else if (wCallBanker == FourBullCommand.Instance.SwitchViewChairID(3))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, true);
            }
        }

        public void WhoStartBet(Object obj)
        {
            //struct CMD_S_GameStart
            //{
            //    //下注信息 
            //    public long lTurnMaxScore;                      //最大下注 
            //    public ushort wBankerUser;                      //庄家用户 
            //};
            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);


            CMD_S_GameStart dataStruct = (CMD_S_GameStart)obj;
            int lTurnMaxScore = (int)(dataStruct.lTurnMaxScore);
            //服务端
            ushort wBankerUser = dataStruct.wBankerUser;
            //显示庄家标识
            if (0 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroPlayerIsBanker);
            }
            else if (1 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                //初始化下注按钮
                FourBull.Messager.Broadcast(FourBullEvent.ShowBetComponet, lTurnMaxScore);
                FourBull.Messager.Broadcast(FourBullEvent.TablePosOnePlayerIsBanker);
            }
            else if (2 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoPlayerIsBanker);
                //初始化下注按钮
                FourBull.Messager.Broadcast(FourBullEvent.ShowBetComponet, lTurnMaxScore);
            }
            else if (3 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosThreePlayerIsBanker);
                //初始化下注按钮
                FourBull.Messager.Broadcast(FourBullEvent.ShowBetComponet, lTurnMaxScore);
            }
        }

        //开始发牌
        public void startPoker(Object obj)
        {
            //CMD_S_SendCard
            //struct CMD_S_SendCard
            //{
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[][] cbCardData; //用户扑克 
            //    public ushort wPlayerCount;
            //};

            CMD_S_SendCard dataStruct = (CMD_S_SendCard)obj;
            byte[] userPoker = dataStruct.cbCardData;
            ushort wPlayerCount = dataStruct.wPlayerCount;
            //发牌
            FourBull.Messager.Broadcast(FourBullEvent.StartdealPoker, dataStruct);
        }

        public void BetResult(Object obj)
        {
            //AddGameListener(this, netCenter.BetResult, (ushort)PROTOCOL_PACKET.SUB_S_ADD_SCORE, typeof(CMD_S_AddScore));
            //struct CMD_S_AddScore
            //{
            //    public ushort wAddScoreUser;                        //加注用户 
            //    public long lAddScoreCount;                     //加注数目 
            //};
            CMD_S_AddScore dataStruct = (CMD_S_AddScore)obj;
            ushort wAddScoreUser = dataStruct.wAddScoreUser;
            int lAddScoreCount = (int)dataStruct.lAddScoreCount;
            if (1 == FourBullCommand.Instance.SwitchViewChairID(wAddScoreUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosOne, lAddScoreCount);
            }
            else if (2 == FourBullCommand.Instance.SwitchViewChairID(wAddScoreUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosTwo, lAddScoreCount);
            }
            else if (3 == FourBullCommand.Instance.SwitchViewChairID(wAddScoreUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosThree, lAddScoreCount);
            }
            else if (0 == FourBullCommand.Instance.SwitchViewChairID(wAddScoreUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosZero, lAddScoreCount);
            }
        }

        public void turnPoker(Object obj)
        {
            //    //用户摊牌
            //struct CMD_S_Open_Card
            //{
            //    public ushort wPlayerID;                            //摊牌用户 
            //    public byte bOpen;                              //摊牌标志 
            //};
            CMD_S_Open_Card cmd = (CMD_S_Open_Card)obj;
            //摊牌消息
            FourBull.Messager.Broadcast(FourBullEvent.showCow,(int)FourBullCommand.Instance.SwitchViewChairID(cmd.wPlayerID));


        }

        public void showResult(Object obj)
        {
            CMD_S_GameEnd ge = (CMD_S_GameEnd)obj;
            //struct CMD_S_GameEnd
            //{
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lGameTax;              //游戏税收 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lGameScore;            //游戏得分 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[] cbCardData;            //用户扑克 
            //    public long lUserGold;                          //用户累计 
            //    public long lMoneyScore;                        //彩金数目 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public ushort[] wLookTable;         //无法加入 
            //};
            FourBull.Messager.Broadcast(FourBullEvent.showResult, ge);

         }


        ///--------------------------------------------------断线处理-------------------------------------------------------
        //在准备界面断线
        public void offlineOnReadyScene(Object obj)
        {
            //struct CMD_S_StatusFree
            //{
            //    public long lCellScore;                         //基础积分 
            //    public long lWeekBonsesInfo;
            //    public long lMoneyScore;
            //    tagSGetBonsesInfo BonsesInfo;
            //    历史积分 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTurnScore;            //积分信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lCollectScore;         //积分信息 
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
            //    public string[] szGameRoomName;           //房间名称 
            //    public int bGameOption;                     //游戏配置 
            //                                                //锁桌信息 
            //    public bool bUserLookTable;                     //锁桌信息 
            //};

            //CMD_S_StatusFree cs = (CMD_S_StatusFree)obj;
            //无需处理
            FourBullSceneManager.getInstance().initPlayerInfo();


        }

        public void offlineOnCallBankScene(Object obj)
        {
            //struct CMD_S_StatusCall
            //{
            //    public ushort wCallBanker;                      //叫庄用户 
            //    public byte cbDynamicJoin;                      //动态加入  
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[] cbPlayStatus;          //用户状态 
            //    public long lWeekBonsesInfo;
            //    public long lMoneyScore;
            //    public tagSGetBonsesInfo BonsesInfo;
            //    //历史积分 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTurnScore;            //积分信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lCollectScore;         //积分信息 
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
            //    public string[] szGameRoomName;           //房间名称 
            //    public int bGameOption;                     //游戏配置 
            //    public bool bUserLookTable;                     //锁桌信息 
            //};

            //struct tagSGetBonsesInfo
            //{
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            //    public string[] szNickname;
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            //    public string[] szCardData;
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            //    public long[] lWinScore;
            //};

            CMD_S_StatusCall cs = (CMD_S_StatusCall)obj;
            ushort wCallBanker = cs.wCallBanker;
            //byte[] cbPlayStatus = cs.cbPlayStatus;
            //long lWeekBonsesInfo = cs.lWeekBonsesInfo;
            //long lMoneyScore = cs.lMoneyScore;
            //tagSGetBonsesInfo BonsesInfo = cs.BonsesInfo;
            long[] lTurnScore = cs.lTurnScore;
            long[] lCollectScore = cs.lCollectScore;
            //string[] szGameRoomName = cs.szGameRoomName;
            //int bGameOption = cs.bGameOption;
            //bool bUserLookTable = cs.bUserLookTable;

            //初始化玩家信息
            FourBullSceneManager.getInstance().initPlayerInfo();
            FourBullSceneManager.getInstance().initPlayerWhoCallBankerScene(wCallBanker);


        }

        public void offlineOnBetScene(Object obj)
        {
            //struct CMD_S_StatusScore
            //{
            //    //下注信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[] cbPlayStatus;          //用户状态 
            //    public byte cbDynamicJoin;                      //动态加入 
            //    public long lTurnMaxScore;                      //最大下注 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTableScore;           //下注数目 
            //    public ushort wBankerUser;                      //庄家用户 
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
            //    public string[] szGameRoomName;           //房间名称 
            //    public long lWeekBonsesInfo;
            //    public long lMoneyScore;
            //    public tagSGetBonsesInfo BonsesInfo;
            //    //历史积分 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTurnScore;            //积分信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] CollectScore;         //积分信息 
            //    public int bGameOption;                     //游戏配置 
            //    public bool bUserLookTable;                     //锁桌信息 
            //};

            CMD_S_StatusScore cs = (CMD_S_StatusScore)obj;
            FourBullSceneManager.getInstance().initPlayerInfo();
            FourBullSceneManager.getInstance().initPlayerBetScene(cs);
        }

        public void offlineOnPlayingScene(Object obj)
        {
            //struct CMD_S_StatusPlay
            //{
            //    //状态信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[] cbPlayStatus;          //用户状态 
            //    public byte cbDynamicJoin;                      //动态加入 
            //    public long lTurnMaxScore;                      //最大下注 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTableScore;           //下注数目 
            //    public ushort wBankerUser;                      //庄家用户 
            //    public long lWeekBonsesInfo;
            //    public long lMoneyScore;
            //    public tagSGetBonsesInfo BonsesInfo;
            //    //扑克信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[][] cbHandCardData;//桌面扑克 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[] bOxCard;               //牛牛数据 
            //                                         //历史积分 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lTurnScore;            //积分信息 
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public long[] lCollectScore;         //积分信息 
            //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
            //    public string[] szGameRoomName;           //房间名称 
            //    public int bGameOption;                     //游戏配置 
            //    public bool bUserLookTable;                     //锁桌信息 
            //};

            CMD_S_StatusPlay cs = (CMD_S_StatusPlay)obj;
            FourBullSceneManager.getInstance().initPlayerInfo();
            FourBullSceneManager.getInstance().initPlayScene(cs);
        }
    }
}