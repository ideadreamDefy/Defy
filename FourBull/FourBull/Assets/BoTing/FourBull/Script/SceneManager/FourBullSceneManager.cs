/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullSceneManager
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== 这里写自己的名字 ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.Framework;
using BoTing.Module;

namespace BoTing.FourBull
{
    public class FourBullSceneManager
    {
        private static FourBullSceneManager _instance = null;
        private FourBullSceneManager()
        {


        }

        public static FourBullSceneManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new FourBullSceneManager();
            }
            return _instance;

        }

        internal void initPlayerInfo()
        {
            //初始化玩家信息
            for (int i = 0; i < FourBullGlobalConst.playerCount; i++)
            {
                IPlayer playerInfo = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)i);
                
                if (playerInfo != null)
                {
                    sitDownPosInTable(playerInfo, i);
                }
            }
        }

        internal void hideAllReadyState()
        {
            FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroIsReady);
            FourBull.Messager.Broadcast(FourBullEvent.TablePosOneIsReady);
            FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoIsReady);
            FourBull.Messager.Broadcast(FourBullEvent.TablePosThreeIsReady);
        }

        internal void sitDownPosInTable(IPlayer player, int pos)
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
            //隐藏所有玩家的准备状态
            hideAllReadyState();
            //移动玩家Icon
            FourBull.Messager.Broadcast(FourBullEvent.BtnStartMoveIcon);
        }
        //初始化谁叫庄
        internal void initPlayerWhoCallBankerScene(ushort wCallBanker)
        {
            //初始化玩家谁叫庄
            if (0 == FourBullCommand.Instance.SwitchViewChairID(wCallBanker))
            {
                FourBull.Messager.Broadcast(FourBullEvent.StartCallZhuang);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }
            else if (1 == FourBullCommand.Instance.SwitchViewChairID(wCallBanker))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, true);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }
            else if (2 == FourBullCommand.Instance.SwitchViewChairID(wCallBanker))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, true);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);
            }
            else if (3 == FourBullCommand.Instance.SwitchViewChairID(wCallBanker))
            {
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
                FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, true);
            }
        }

        //初始化下注状态包括自己
        internal void initPlayerBetScene(CMD_S_StatusScore cs)
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

            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker1, false);
            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker2, false);
            FourBull.Messager.Broadcast(FourBullEvent.isCallBanker3, false);


            long lTurnMaxScore = cs.lTurnMaxScore; //最大下注
            long[] lTableScore = cs.lTableScore; // 下注数目
            ushort wBankerUser = cs.wBankerUser; //庄家用户
            //下注结果
            for (int index = 0; index < 4; index++)
            {
                if (0 == FourBullCommand.Instance.SwitchViewChairID((ushort)index))
                {
                    //如果下注结果为0，则表示还未下注--------（可能有下注中的需求）
                    if ((int)(lTableScore[0]) == 0)
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.ShowBetComponet, lTurnMaxScore);
                    }
                    else
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.BetPosZero,(int)lTableScore[0]);
                    }
                }
                else if (1 == FourBullCommand.Instance.SwitchViewChairID((ushort)index))
                {
                    if ((int)(lTableScore[1]) == 0)
                    {

                    }
                    else
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.BetPosZero, (int)lTableScore[1]);
                    }
                }
                else if (2 == FourBullCommand.Instance.SwitchViewChairID((ushort)index))
                {
                    if ((int)(lTableScore[2])== 0)
                    {

                    }
                    else
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.BetPosZero, (int)lTableScore[2]);
                    }
                }
                else if (3 == FourBullCommand.Instance.SwitchViewChairID((ushort)index))
                {
                    if ((int)(lTableScore[3] )== 0)
                    {

                    }
                    else
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.BetPosZero, (int)lTableScore[3]);
                    }
                }
            }

            //显示庄家标识
            if (0 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroPlayerIsBanker);
            }
            else if (1 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosOnePlayerIsBanker);
            }
            else if (2 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoPlayerIsBanker);
            }
            else if (3 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosThreePlayerIsBanker);
            }

        }

        internal void initPlayScene(CMD_S_StatusPlay cs)
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
            //    public byte[] cbOxInfo;
            //};

            long[] lTableScore = cs.lTableScore; //下注数目
            ushort wBankerUser = cs.wBankerUser; //庄家用户
            //显示玩家下注结果
            if ((int)lTableScore[0] != 0)
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosZero, (int)lTableScore[0]);
            }

            if ((int)lTableScore[1] != 0)
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosOne, (int)lTableScore[1]);
            }

            if ((int)lTableScore[2] != 0)
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosTwo, (int)lTableScore[2]);
            }

            if ((int)lTableScore[3] != 0)
            {
                FourBull.Messager.Broadcast(FourBullEvent.BetPosThree, (int)lTableScore[3]);
            }

            //显示庄家标识
            if (0 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosZeroPlayerIsBanker);
            }
            else if (1 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosOnePlayerIsBanker);
            }
            else if (2 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosTwoPlayerIsBanker);
            }
            else if (3 == FourBullCommand.Instance.SwitchViewChairID(wBankerUser))
            {
                FourBull.Messager.Broadcast(FourBullEvent.TablePosThreePlayerIsBanker);
            }

            //struct CMD_S_SendCard
            //{
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[][] cbCardData; //用户扑克 
            //    public ushort wPlayerCount;
            //};
            CMD_S_SendCard cardStruct = new CMD_S_SendCard();
            cardStruct.cbCardData = cs.cbHandCardData;
            //发牌
            FourBull.Messager.Broadcast(FourBullEvent.StartdealPoker, cardStruct);
            //摊牌结果
            for (ushort index = 0; index < 4; index++)
            {
                if (0 == FourBullCommand.Instance.SwitchViewChairID(index))
                {
                    //未摊牌
                    if (cs.cbOxInfo[index] != 0xff) { 
                        FourBull.Messager.Broadcast(FourBullEvent.showCow, 0);
                    }
                }
                else if (1 == FourBullCommand.Instance.SwitchViewChairID(index))
                {
                    if (cs.cbOxInfo[index] != 0xff)
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.showCow, 1);
                    }
                }
                else if (2 == FourBullCommand.Instance.SwitchViewChairID(index))
                {
                    if (cs.cbOxInfo[index] != 0xff)
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.showCow, 2);
                    }
                }
                else if (3 == FourBullCommand.Instance.SwitchViewChairID(index))
                {
                    if (cs.cbOxInfo[index] != 0xff)
                    {
                        FourBull.Messager.Broadcast(FourBullEvent.showCow, 3);
                    }
                }
            }
        }
    }
}