/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：downPlayerObject
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BoTing.Module;
using BoTing.Framework;
using System;
using BoTing.GamePublic;
using UnityEngine.EventSystems;

namespace BoTing.FourBull
{
    public class FourBullDownPlayerObject : ViewBase
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
            FourBull.Messager.AddListener(FourBullEvent.showPoker, showBtn);
            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
            FourBull.Messager.AddListener(FourBullEvent.showMyPoker, showHandPoker);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener(FourBullEvent.showPoker, showBtn);
            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
            FourBull.Messager.RemoveListener(FourBullEvent.showMyPoker, showHandPoker);
        }

        private void Init()
        {
            addSureButtonEventListener();
            gameObject.SetActive(false);
        }
        //给确定(摊牌)按钮注册监听
        private void addSureButtonEventListener()
        {
            GameObject startBtnGameObject = transform.FindChild("btnBluff").gameObject;
            EventTriggerAssist.Get(startBtnGameObject).onClick(sureBtnClicked);
        }

        public void sureBtnClicked(PointerEventData pe)
        {
            showHandPoker();
        }

        public void showHandPoker()
        {
            //struct CMD_C_OxCard
            //{
            //    public byte bOX;                                //牛牛标志 
            //};
            PokerCard[,] pokerData = FourBullPlayerData.getInstance().playerPokerSet;
            PokerCard[] pokerArray = new PokerCard[5];
            for (int i = 0; i < 5; i++)
            {
                pokerArray[i] = pokerData[0, i];
            }
            PokerStructInfo result = FourBullLogic.getInstance().pokerPoints(pokerArray);
            CMD_C_OxCard cc = new CMD_C_OxCard();
            cc.bOX = (result.Points == -1) ? (byte)0 : (byte)1;
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 4);
            transform.FindChild("btnBluff").gameObject.SetActive(false);
            FourBullCommand.Instance.SendGamePacket<CMD_C_OxCard>((ushort)PROTOCOL_CLIENT.SUB_C_OPEN_CARD, cc);
        }

        private void showBtn()
        {
            gameObject.SetActive(true);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
            transform.FindChild("btnBluff").gameObject.SetActive(true);
        }
    }
}