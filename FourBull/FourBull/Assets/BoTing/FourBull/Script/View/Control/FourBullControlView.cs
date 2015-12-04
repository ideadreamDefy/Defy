/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullControlView
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BoTing.Framework;
using System;
using BoTing.GamePublic;
using BoTing.Module;

namespace BoTing.FourBull {
    public class FourBullControlView : ViewBase
    {
        //牌的预制件
        public GameObject pokerPreb;

        //所有牌的GameObject
        private GameObject[] pokerPool = new GameObject[FourBullGlobalConst.pokerTotalCount];

        private float dealInterval = 0.1f;//扑克发牌时间间隔

        private float dealPlayerPokerSpace = 30.0f;
        //花色信息存储
        private PokerCard[,] pokerInfoList = new PokerCard[4, 5];

        private float dealSpace = 10.0f;//发牌区 牌位置间隔

        private int roundCount = 0;

        public GameObject[] PlayerSet = new GameObject[FourBullGlobalConst.playerCount];
        void Start()
        {

        }
        void Update()
        {

        }

        public override void InitView()
        {
            initGameView();
            FourBull.Messager.AddListener(FourBullEvent.BtnStartMoveIcon, moveIcon);
            FourBull.Messager.AddListener<CMD_S_SendCard>(FourBullEvent.StartdealPoker, dealPoker);
            FourBull.Messager.AddListener<int>(FourBullEvent.showCow, showPlayerCow);
            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener(FourBullEvent.BtnStartMoveIcon, moveIcon);
            FourBull.Messager.RemoveListener<CMD_S_SendCard>(FourBullEvent.StartdealPoker, dealPoker);
            FourBull.Messager.RemoveListener<int>(FourBullEvent.showCow, showPlayerCow);
            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
        }

        private void initGameView()
        {
            labelPoker();
        }

        private void showPlayerCow(int serverPos)
        {
            PokerCard[,] pokerData = FourBullPlayerData.getInstance().playerPokerSet;
            PokerCard[] pokerArray = new PokerCard[5];
            for (int i = 0; i < 5; i++)
            {
                pokerArray[i] = pokerData[serverPos, i];
            }
            string cowPoints = FourBullLogic.getInstance().getPokerPoint(pokerArray);

            IPlayer down = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)0);
            IPlayer right = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)1);
            IPlayer top = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)2);
            IPlayer left = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)3);
            if (down != null &&  down.ChairID == serverPos)
            {
                //隐藏poker
                PlayerSet[0].gameObject.SetActive(false);
                //显示分组poker
                FourBull.Messager.Broadcast(FourBullEvent.showDownCow, cowPoints);
            }
            else if (right != null && right.ChairID == serverPos)
            {
                PlayerSet[1].gameObject.SetActive(false);
                FourBull.Messager.Broadcast(FourBullEvent.showRightCow, cowPoints);
            }
            else if (top != null && top.ChairID == serverPos)
            {
                PlayerSet[2].gameObject.SetActive(false);
                FourBull.Messager.Broadcast(FourBullEvent.showTopCow, cowPoints);
            }
            else if (left != null && left.ChairID == serverPos)
            {
                PlayerSet[3].gameObject.SetActive(false);
                FourBull.Messager.Broadcast(FourBullEvent.showLeftCow, cowPoints);
            }
        }

        private void dealPoker(CMD_S_SendCard obj)
        {
            //CMD_S_SendCard
            //struct CMD_S_SendCard
            //{
            //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
            //    public byte[][] cbCardData; //用户扑克 
            //    public ushort wPlayerCount;
            //};
            

            for (int i = 0; i < FourBullGlobalConst.pokerTotalCount; i++)
            {
                if (pokerPool != null && pokerPool[i] != null)
                {
                    pokerPool[i].SetActive(true);
                }
            }

            for (int i = 0; i < FourBullGlobalConst.pokerTotalCount; i++)
            {
                int tag = (int)(i % 5);
                int chairId = (int)FourBullCommand.Instance.SwitchViewChairID((ushort)(i/5));
                PokerCard pokerInfo = new PokerCard();
                pokerInfo.SetFront((int)(obj.cbCardData[i]));
                pokerInfoList[chairId,tag] = pokerInfo;
            }

            //将数据存储
            FourBullPlayerData.getInstance().initData(pokerInfoList);

            StartCoroutine(StartDealPoke());
            
        }

        private void  labelPoker()
        {
            //牌
            for (int i = 0; i < FourBullGlobalConst.pokerTotalCount; i++)
            {
                //隐藏剩余的牌
                GameObject obj = Instantiate(pokerPreb) as GameObject;

                pokerPool[i] = obj;

                obj.SetActive(false);

                obj.transform.parent = transform.parent.FindChild("pokerDrawPos").transform; ;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
            }
        }

        IEnumerator StartDealPoke()
        {
            roundCount = 0;
            for (int round = 0; round < 5; round++)
            {
                
                for (int players = 0; players < FourBullGlobalConst.playerCount; players++)
                {
                    if (players == 0)
                    {
                        pokerPool[round*4+players].GetComponent<Image>().sprite = FourBull.PublicLoader.LoadCard( pokerInfoList[players, round].FrontSpriteName);
                    }
                    if (!pokerInfoList[players, round].FrontSpriteName.Equals("bm"))
                    {
                        dealOnePoker(PlayerSet[players], pokerPool[round * 4 + players]);
                    }
                    else
                    {
                        pokerPool[round * 4 + players].SetActive(false);
                    }
                    //延时0.5毫秒
                    yield return new WaitForSeconds(dealInterval);
                }
                roundCount = round + 1;
            }
            //发牌结束显示确定(摊牌)按钮，开启定时器
            FourBull.Messager.Broadcast(FourBullEvent.bluffPokerClock);
            FourBull.Messager.Broadcast(FourBullEvent.showPoker);
        }

        void dealOnePoker(GameObject player, GameObject poker)//发一张牌
        {
            //得到玩家的座位pos
            poker.transform.parent = player.transform;
            poker.transform.localScale = Vector3.one;
            Vector3 pos = new Vector3();
            if (player.name != null && player.name.Equals("playerTopDrawPos"))
            {
                pos = transform.parent.FindChild("playerTopDrawPos").transform.position;
                pos.x += player.transform.childCount * dealPlayerPokerSpace;
            }
            else if (player.name != null && player.name.Equals("playerLeftDrawPos"))
            {
                pos = transform.parent.FindChild("playerLeftDrawPos").transform.position;
                pos.x += player.transform.childCount * dealPlayerPokerSpace;
            }
            else if (player.name != null && player.name.Equals("playerDownDrawPos"))
            {
                pos = transform.parent.FindChild("playerDownDrawPos").transform.position;
                pos.x += player.transform.childCount * 88;
            }
            else
            {
                pos = transform.parent.FindChild("playerRightDrawPos").transform.position;
                pos.x += player.transform.childCount * dealPlayerPokerSpace;
            }

            pos.z = -0.1f * roundCount;

            LeanTween.move(poker, pos, dealInterval).setEase(LeanTweenType.easeInQuad);

            Vector3 posTrans = player.transform.position;
            if (player.name != null && player.name.Equals("playerTopDrawPos"))
            {
                posTrans.x -= dealSpace / 2;
            }
            else if (player.name != null && player.name.Equals("playerLeftDrawPos"))
            {
                posTrans.x -= dealSpace / 2;
            }
            else if (player.name != null && player.name.Equals("playerDownDrawPos"))
            {
                posTrans.x -= 70;
            }
            else if (player.name != null && player.name.Equals("playerRightDrawPos"))
            {
                posTrans.x -= dealSpace / 2;
            }
            
        }

        private void moveIcon()
        {
                //移动玩家的Icon
                var playerLeft = transform.parent.FindChild("playerLeftPanel").gameObject;
                var playerLeftPos = transform.parent.FindChild("playerLeftPos").gameObject;

                var playeRight = transform.parent.FindChild("playerRightPanel").gameObject;
                var playerRightPos = transform.parent.FindChild("playerRightPos").gameObject;

                var playerTop = transform.parent.FindChild("playerTopPanel").gameObject;
                var playerTopPos = transform.parent.FindChild("playerTopPos").gameObject;

                var playerDown = transform.parent.FindChild("playerDownPanel").gameObject;
                var playerDownPos = transform.parent.FindChild("playerDownPos").gameObject;

                FourBullGlobalTool.moveAction(playerLeft, playerLeftPos, FourBullGlobalConst.iconMoveTime);
                FourBullGlobalTool.moveAction(playeRight, playerRightPos, FourBullGlobalConst.iconMoveTime);
                FourBullGlobalTool.moveAction(playerTop, playerTopPos, FourBullGlobalConst.iconMoveTime);
                FourBullGlobalTool.moveAction(playerDown, playerDownPos, FourBullGlobalConst.iconMoveTime);

                FourBull.Messager.Broadcast(FourBullEvent.HidePlayerState);
           }
        private void ResetView()
        {
            //位置重置
            var playerLeft = transform.parent.FindChild("playerLeftPanel").gameObject;
            var playerLeftStartPos = transform.parent.FindChild("playerLeftStartPos").gameObject;

            var playeRight = transform.parent.FindChild("playerRightPanel").gameObject;
            var playerRightStartPos = transform.parent.FindChild("playerRightStartPos").gameObject;

            var playerTop = transform.parent.FindChild("playerTopPanel").gameObject;
            var playerTopStartPos = transform.parent.FindChild("playerTopStartPos").gameObject;

            var playerDown = transform.parent.FindChild("playerDownPanel").gameObject;
            var playerDownStartPos = transform.parent.FindChild("playerDownStartPos").gameObject;

            playerLeft.transform.position = playerLeftStartPos.transform.position;
            playeRight.transform.position = playerRightStartPos.transform.position;
            playerTop.transform.position = playerTopStartPos.transform.position;
            playerDown.transform.position = playerDownStartPos.transform.position;

            for (int i = 0; i < FourBullGlobalConst.pokerTotalCount; i++)
            {
                //隐藏剩余的牌
                pokerPool[i].SetActive(false);
                //pokerPool[i].GetComponent<Image>().sprite = FourBull.PrivateLoader.LoadCard("bm");
                pokerPool[i].transform.parent = transform.parent.FindChild("pokerDrawPos").transform;
                pokerPool[i].transform.localPosition = Vector3.zero;
            }

            FourBullPlayerData.getInstance().playerPokerSet = new PokerCard[4,5];

            transform.parent.FindChild("playerTopDrawPos").gameObject.SetActive(true);
            transform.parent.FindChild("playerLeftDrawPos").gameObject.SetActive(true);
            transform.parent.FindChild("playerRightDrawPos").gameObject.SetActive(true);
            transform.parent.FindChild("playerDownDrawPos").gameObject.SetActive(true);
            //initGameView();





        }
    }
 }
