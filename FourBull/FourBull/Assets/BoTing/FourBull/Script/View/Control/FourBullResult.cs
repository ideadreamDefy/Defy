/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullResult
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.Framework;
using System;
using UnityEngine.UI;
using BoTing.GamePublic;
using UnityEngine.EventSystems;

namespace BoTing.FourBull
{
    public class FourBullResult : ViewBase
    {
        void Start()
        {

        }
        void Update()
        {

        }

        public override void InitView()
        {
            Init();
            
            FourBull.Messager.AddListener<CMD_S_GameEnd>(FourBullEvent.showResult,showResultPanel);

            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener<CMD_S_GameEnd>(FourBullEvent.showResult, showResultPanel);

            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        private void showResultPanel(CMD_S_GameEnd ge)
        {
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
            //玩家名称
            //玩家积分
            int count = 1;
            for (int i = 0; i < 4; i++)
            {
                var player = FourBullCommand.Instance.GetPlayerByClientChairID((ushort)i);
                if (player != null) {
                    int target = (int)player.ChairID;
                    if (target != 0) {
                        var nameText = transform.FindChild("player" + count + "Text").GetComponent<Text>();
                        var scoreText = transform.FindChild("player" + count + "Score").GetComponent<Text>();
                        nameText.text = player.NickName;
                        scoreText.text = ge.lGameScore[target].ToString();
                        count++;
                    }
                }
            }
            //离开按钮
            GameObject leaveBtnGameObject = transform.FindChild("btnLeave").gameObject;
            EventTriggerAssist.Get(leaveBtnGameObject).onClick(btnLeaveClicked);
            //继续按钮
            GameObject continueBtnGameObject = transform.FindChild("btnContinue").gameObject;
            EventTriggerAssist.Get(continueBtnGameObject).onClick(btnContinueClicked);
            gameObject.SetActive(true);

        }

        private void btnLeaveClicked(PointerEventData pe)
        {

        }

        private void btnContinueClicked(PointerEventData pe)
        {
            //数据初始化

            //界面初始化
            FourBull.Messager.Broadcast(FourBullEvent.resetGame);

            FourBullCommand.Instance.SendUserReady();
        }


        private void Init()
        {
            gameObject.SetActive(false);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }
    }
}