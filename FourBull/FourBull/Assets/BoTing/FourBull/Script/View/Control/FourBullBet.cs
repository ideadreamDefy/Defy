/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullBet
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

namespace BoTing.FourBull {
    public class FourBullBet : ViewBase
    {
        void Start()
        {

        }
        void Update()
        {

        }

        public override void InitView()
        {
            initObjectView();
            FourBull.Messager.AddListener<int>(FourBullEvent.ShowBetComponet, showCommponet);
            FourBull.Messager.AddListener(FourBullEvent.resetGame,ResetView);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener<int>(FourBullEvent.ShowBetComponet, showCommponet);
            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        private void initObjectView()
        {
            gameObject.SetActive(false);
        }
        //初始化下注按钮
        private void showCommponet(int lTurnMaxScore)
        {
            //m_lAddMaxScore,m_lAddMaxScore / 2,m_lAddMaxScore / 5,m_lAddMaxScore / 10
            var btn1Text = transform.FindChild("BtnBet1/Text").GetComponent<Text>();
            btn1Text.text = lTurnMaxScore.ToString();

            var btn2Text = transform.FindChild("BtnBet2/Text").GetComponent<Text>();
            btn2Text.text =((int)(lTurnMaxScore / 2)).ToString();

            var btn3Text = transform.FindChild("BtnBet3/Text").GetComponent<Text>();
            btn3Text.text = ((int)(lTurnMaxScore / 5)).ToString();

            var btn4Text = transform.FindChild("BtnBet4/Text").GetComponent<Text>();
            btn4Text.text =((int) (lTurnMaxScore / 10)).ToString();

            GameObject BtnBet1GameObject = transform.FindChild("BtnBet1").gameObject;
            EventTriggerAssist.Get(BtnBet1GameObject).onClick(BtnBet1Clicked);

            GameObject BtnBet2GameObject = transform.FindChild("BtnBet2").gameObject;
            EventTriggerAssist.Get(BtnBet2GameObject).onClick(BtnBet2OnClick);

            GameObject BtnBet3GameObject = transform.FindChild("BtnBet3").gameObject;
            EventTriggerAssist.Get(BtnBet3GameObject).onClick(BtnBet3OnClick);

            GameObject BtnBet4GameObject = transform.FindChild("BtnBet4").gameObject;
            EventTriggerAssist.Get(BtnBet4GameObject).onClick(BtnBet4OnClick);

            gameObject.SetActive(true);

            FourBull.Messager.Broadcast(FourBullEvent.StartBetClock);
        }

        private void BtnBet1Clicked(PointerEventData pd)
        {
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 3);
            var btn1Text = transform.FindChild("BtnBet1").GetComponentInChildren<Text>();
            CMD_C_AddScore callBanker = new CMD_C_AddScore();
            callBanker.lScore = long.Parse(btn1Text.text);
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_ADD_SCORE, callBanker);
            gameObject.SetActive(false);
        }

        private void BtnBet2OnClick(PointerEventData pd)
        {
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 3);
            var btn2Text = transform.FindChild("BtnBet2").GetComponentInChildren<Text>();
            CMD_C_AddScore callBanker = new CMD_C_AddScore();
            callBanker.lScore = long.Parse(btn2Text.text);
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_ADD_SCORE, callBanker);
            gameObject.SetActive(false);
            
        }

        private void BtnBet3OnClick(PointerEventData pd)
        {
            var btn3Text = transform.FindChild("BtnBet3").GetComponentInChildren<Text>();
            CMD_C_AddScore callBanker = new CMD_C_AddScore();
            callBanker.lScore = long.Parse(btn3Text.text);
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_ADD_SCORE, callBanker);
            gameObject.SetActive(false);
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock,3);
        }

        private void BtnBet4OnClick(PointerEventData pd)
        {
            var btn4Text = transform.FindChild("BtnBet4").GetComponentInChildren<Text>();
            CMD_C_AddScore callBanker = new CMD_C_AddScore();
            callBanker.lScore = long.Parse(btn4Text.text);
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_ADD_SCORE, callBanker);
            gameObject.SetActive(false);
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock,3);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }


    }
}