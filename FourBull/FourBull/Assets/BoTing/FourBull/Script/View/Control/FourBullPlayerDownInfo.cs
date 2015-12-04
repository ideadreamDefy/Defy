/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：playerDownInfo
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
using System;
using BoTing.Framework;
using BoTing.GamePublic;
using UnityEngine.EventSystems;

namespace BoTing.FourBull
{
    public class FourBullPlayerDownInfo : ViewBase
    {
        public override void InitView()
        {
            //初始化panel的准备状态
            initPanelState();
            //注册按钮事件
            addStartButtonEventListener();
            //房间初始化和有人加入的监听
            FourBull.Messager.AddListener<IPlayer>(FourBullEvent.TablePosZeroPlayerInfo, changePlayerInfo);
            //房间有人离开监听
            FourBull.Messager.AddListener<IPlayer>(FourBullEvent.TablePosZeroPlayerLeave, leavePlayer);
            //此人是否准备
            FourBull.Messager.AddListener(FourBullEvent.TablePosZeroIsReady, isReady);
            //隐藏准备图片
            FourBull.Messager.AddListener(FourBullEvent.HidePlayerState, hideReadyState);
            //是否为庄家
            FourBull.Messager.AddListener(FourBullEvent.TablePosZeroPlayerIsBanker, isBanker);
            //显示下注数目
            FourBull.Messager.AddListener<int>(FourBullEvent.BetPosZero, showBetNumber);

            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);


        }

        void Update()
        {

        }

        void OnDestroy()
        {
            //房间初始化和有人加入的监听
            FourBull.Messager.RemoveListener<IPlayer>(FourBullEvent.TablePosZeroPlayerInfo, changePlayerInfo);
            //房间有人离开监听
            FourBull.Messager.RemoveListener<IPlayer>(FourBullEvent.TablePosZeroPlayerLeave, leavePlayer);
            //此人是否准备
            FourBull.Messager.RemoveListener(FourBullEvent.TablePosZeroIsReady, isReady);
            //隐藏准备图片
            FourBull.Messager.RemoveListener(FourBullEvent.HidePlayerState, hideReadyState);

            FourBull.Messager.RemoveListener(FourBullEvent.TablePosZeroPlayerIsBanker, isBanker);

            FourBull.Messager.RemoveListener<int>(FourBullEvent.BetPosZero, showBetNumber);

            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        private void showBetNumber(int number)
        {
            transform.FindChild("betNumObject").gameObject.SetActive(true);
            transform.FindChild("betNumObject/num_Bet").GetComponent<Text>().text = number.ToString();
        }

        //开始按钮点击注册
        private void addStartButtonEventListener()
        {
            GameObject startBtnGameObject = transform.FindChild("StartButton").gameObject;
            EventTriggerAssist.Get(startBtnGameObject).onClick(startBtnOnClick);
        }
        //开始按钮点击
        private void startBtnOnClick(PointerEventData data)
        {
            FourBullCommand.Instance.SendUserReady();
        }

        //初始化Panel
        private void initPanelState()
        {
            //隐藏准备状态
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);

            //隐藏庄家标识
            var bankerImg = transform.FindChild("bankerImg").gameObject;
            bankerImg.SetActive(false);

            var bankerTagImg = transform.FindChild("bankerTag").gameObject;
            bankerTagImg.SetActive(false);

            var betNumObject = transform.FindChild("betNumObject").gameObject;
            betNumObject.SetActive(false);
            //叫庄中状态中
            var callBankerTextState = transform.FindChild("betNumObject").gameObject;
            callBankerTextState.SetActive(false);
        }
        /// <summary>
        /// changePlayerInfo 设置第0号位置玩家的ui显示信息
        /// </summary>
        /// <param name="playerInfo"></param>
        private void changePlayerInfo(IPlayer playerInfo) {
            gameObject.SetActive(true);
            //设置玩家Id
            var idLabel = transform.FindChild("idLabelText").GetComponent<Text>();
            idLabel.text = playerInfo.NickName;
            //设置玩家金币
            var goldLabel = transform.FindChild("goldLabelText").GetComponent<Text>();
            goldLabel.text = playerInfo.Score.ToString();

            if (playerInfo.UserStatus == 0x03) {
                var stateImg = transform.FindChild("playerState").gameObject;
                stateImg.SetActive(true);
            }
        }

        private void leavePlayer(IPlayer playerInfo)
        {
            //清空玩家Id
            var idLabel = transform.FindChild("idLabelText").GetComponent<Text>();
            idLabel.text = "";
            //清空玩家金币
            var goldLabel = transform.FindChild("goldLabelText").GetComponent<Text>();
            goldLabel.text = "";
            //隐藏panel
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);
        }

        private void hideReadyState()
        {
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);
        }


        private void isReady()
        {
            //隐藏开始按钮
            GameObject startBtnGameObject = transform.FindChild("StartButton").gameObject;
            startBtnGameObject.SetActive(false);
            //显示准备状态
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(true);
            //关闭定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 1);
        }

        //是否为庄家
        private void isBanker()
        {
            //显示庄家标识
            var bankerImg = transform.FindChild("bankerImg").gameObject;
            bankerImg.SetActive(true);

            var bankerTagImg = transform.FindChild("bankerTag").gameObject;
            bankerTagImg.SetActive(true);
        }

        public void ResetView()
        {
            //gameObject.SetActive(false);
            //隐藏准备状态
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);

            //隐藏庄家标识
            var bankerImg = transform.FindChild("bankerImg").gameObject;
            bankerImg.SetActive(false);

            var bankerTagImg = transform.FindChild("bankerTag").gameObject;
            bankerTagImg.SetActive(false);

            var betNumObject = transform.FindChild("betNumObject").gameObject;
            betNumObject.SetActive(false);
        }
    }
}