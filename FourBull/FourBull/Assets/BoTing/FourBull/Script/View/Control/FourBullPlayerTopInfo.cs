/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullPlayerTopInfo
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== 这里写自己的名字 ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BoTing.Module;
using System;
using BoTing.Framework;

namespace BoTing.FourBull
{
    public class FourBullPlayerTopInfo : ViewBase
    {
        public override void InitView()
        {
            initPanelState();
            //房间初始化和有人加入的监听
            FourBull.Messager.AddListener<IPlayer>(FourBullEvent.TablePosTwoPlayerInfo, changePlayerInfo);
            //房间有人离开监听
            FourBull.Messager.AddListener<IPlayer>(FourBullEvent.TablePosTwoPlayerLeave, leavePlayer);
            //此人是否准备
            FourBull.Messager.AddListener(FourBullEvent.TablePosTwoIsReady, isReady);
            //隐藏准备图片
            FourBull.Messager.AddListener(FourBullEvent.HidePlayerState, hideReadyState);

            FourBull.Messager.AddListener(FourBullEvent.TablePosTwoPlayerIsBanker,isBanker);

            FourBull.Messager.AddListener<int>(FourBullEvent.BetPosTwo, showBetNumber);

            FourBull.Messager.AddListener(FourBullEvent.hidePlayerTwo, hidePlayerPos);

            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);

            FourBull.Messager.AddListener<bool>(FourBullEvent.isCallBanker2, changecallBankerTextState);
        }

        void OnDestroy()
        {
            //房间初始化和有人加入的监听
            FourBull.Messager.RemoveListener<IPlayer>(FourBullEvent.TablePosTwoPlayerInfo, changePlayerInfo);
            //房间有人离开监听
            FourBull.Messager.RemoveListener<IPlayer>(FourBullEvent.TablePosTwoPlayerLeave, leavePlayer);
            //此人是否准备
            FourBull.Messager.RemoveListener(FourBullEvent.TablePosTwoIsReady, isReady);
            //隐藏准备图片
            FourBull.Messager.RemoveListener(FourBullEvent.HidePlayerState, hideReadyState);

            FourBull.Messager.RemoveListener(FourBullEvent.TablePosTwoPlayerIsBanker, isBanker);

            FourBull.Messager.RemoveListener<int>(FourBullEvent.BetPosTwo, showBetNumber);

            FourBull.Messager.RemoveListener(FourBullEvent.hidePlayerTwo, hidePlayerPos);

            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);

            FourBull.Messager.RemoveListener<bool>(FourBullEvent.isCallBanker2, changecallBankerTextState);
        }

        //隐藏位置
        private void hidePlayerPos()
        {
            gameObject.SetActive(false);
        }


        void Update()
        {

        }
        /// <summary>
        /// changePlayerInfo 设置第0号位置玩家的ui显示信息
        /// </summary>
        /// <param name="playerInfo"></param>
        private void changePlayerInfo(IPlayer playerInfo)
        {
            gameObject.SetActive(true);

            //设置玩家Id
            var idLabel = transform.FindChild("idLabelText").GetComponent<Text>();
            idLabel.text = playerInfo.NickName;
            //设置玩家金币
            var goldLabel = transform.FindChild("goldLabelText").GetComponent<Text>();
            goldLabel.text = playerInfo.Score.ToString();

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(true);

            if (playerInfo.UserStatus == 0x03)
            {
                var stateImg = transform.FindChild("playerState").gameObject;
                stateImg.SetActive(true);

                var readingObjectText = transform.FindChild("readyingText").gameObject;
                readingObjectText.SetActive(false);
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

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);

        }


        private void isReady()
        {
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(true);

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);
        }

        private void initPanelState()
        {
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);

            //隐藏庄家标识
            var bankerImg = transform.FindChild("bankerImg").gameObject;
            bankerImg.SetActive(false);

            var bankerTagImg = transform.FindChild("bankerTag").gameObject;
            bankerTagImg.SetActive(false);

            var betNumObject = transform.FindChild("betNumObject").gameObject;
            betNumObject.SetActive(false);

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);

            var callBankerTextState = transform.FindChild("callBankerText").gameObject;
            callBankerTextState.SetActive(false);
        }

        private void hideReadyState()
        {
            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);
        }

        //是否为庄家
        private void isBanker()
        {
            //显示庄家标识
            var bankerImg = transform.FindChild("bankerImg").gameObject;
            bankerImg.SetActive(true);

            var bankerTagImg = transform.FindChild("bankerTag").gameObject;
            bankerTagImg.SetActive(true);

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);
        }

        private void showBetNumber(int number)
        {
            transform.FindChild("betNumObject").gameObject.SetActive(true);
            transform.FindChild("betNumObject/num_Bet").GetComponent<Text>().text = number.ToString();
            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);
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

            var readingObject = transform.FindChild("readyingText").gameObject;
            readingObject.SetActive(false);
        }

        private void changecallBankerTextState(bool isShow)
        {
            //叫庄中状态中
            var callBankerTextState = transform.FindChild("callBankerText").gameObject;
            callBankerTextState.SetActive(isShow);

            var stateImg = transform.FindChild("playerState").gameObject;
            stateImg.SetActive(false);
        }
    }
}

