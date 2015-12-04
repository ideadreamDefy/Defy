/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullClock
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.Framework;
using UnityEngine.UI;

namespace BoTing.FourBull
{
    public class FourBullClock : ViewBase
    {
        /// <summary>
        /// 正在倒计时中
        /// </summary>
        private bool mCountting;
        /// <summary>
        /// 初始化倒计时时间
        /// </summary>
        private int mCount;

        private Text mText;

        void Start()
        {

        }
        void Update()
        {

        }
        public override void InitView()
        {
            initObjectView();
            //注册进房间是否倒计时监听（未坐下前）
            FourBull.Messager.AddListener(FourBullEvent.InRoomClockStart, InRoomStartTime);
            FourBull.Messager.AddListener(FourBullEvent.StartCallByClock,CallZhuangStartTime);
            FourBull.Messager.AddListener(FourBullEvent.StartBetClock,BetStartTime);
            FourBull.Messager.AddListener<int>(FourBullEvent.StopClock, ResetClock);
            FourBull.Messager.AddListener(FourBullEvent.bluffPokerClock, startBluffPokerClock);
            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener(FourBullEvent.InRoomClockStart, InRoomStartTime);
            FourBull.Messager.RemoveListener(FourBullEvent.StartCallByClock,CallZhuangStartTime);
            FourBull.Messager.RemoveListener<int>(FourBullEvent.StopClock, ResetClock);
            FourBull.Messager.RemoveListener(FourBullEvent.StartBetClock, BetStartTime);
            FourBull.Messager.RemoveListener(FourBullEvent.bluffPokerClock, startBluffPokerClock);
            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        

        private void initObjectView()
        {
            gameObject.SetActive(false);
        }


        private void startBluffPokerClock()
        {
            gameObject.SetActive(true);
            mText = transform.FindChild("text_clock").GetComponent<Text>();
            if (!mCountting)
            {
                mCount = 0;
                mCountting = true;
                StartCoroutine(startBluffPokerClockIEnumerator());
            }
        }

        IEnumerator startBluffPokerClockIEnumerator()
        {
            while (mCount <= FourBullGlobalConst.bluffPokerWaitTime)
            {
                int curTime = FourBullGlobalConst.bluffPokerWaitTime - mCount;
                mText.text = curTime > 9 ? curTime.ToString() : "0" + curTime.ToString();
                yield return new WaitForSeconds(1);
                mCount++;
            }
            yield return null;
            mCountting = false;
            //叫庄倒计时结束
            gameObject.SetActive(false);
            //主动发送摊牌请求
            FourBull.Messager.Broadcast(FourBullEvent.showMyPoker);
        }
        
        private void CallZhuangStartTime()
        {
            gameObject.SetActive(true);
            mText = transform.FindChild("text_clock").GetComponent<Text>();
            if (!mCountting)
            {
                mCount = 0;
                mCountting = true;
                StartCoroutine(CallZhuangStartTimeIEnumerator());
            }
        }

        //叫庄倒计时
        IEnumerator CallZhuangStartTimeIEnumerator()
        {
            while (mCount <= FourBullGlobalConst.callWaitTime)
            {
                int curTime = FourBullGlobalConst.callWaitTime - mCount;
                mText.text = curTime > 9 ? curTime.ToString() : "0" + curTime.ToString();
                yield return new WaitForSeconds(1);
                mCount++;
            }
            yield return null;
            mCountting = false;
            //叫庄倒计时结束
            gameObject.SetActive(false);
        }



        private void InRoomStartTime()
        {
            gameObject.SetActive(true);
            mText = transform.FindChild("text_clock").GetComponent<Text>();
            if (!mCountting)
            {
                mCount = 0;
                mCountting = true;
                StartCoroutine(InRoomStartTimeIEnumerator());
            }
        }

        public void ResetClock(int type)
        {
            gameObject.SetActive(false);
            mCountting = false;
            switch (type) {
                case 1: StopCoroutine(InRoomStartTimeIEnumerator()); break;
                case 2: StopCoroutine(CallZhuangStartTimeIEnumerator()); break;
                case 3: StopCoroutine(BetStartTimeIEnumerator()); break;
                case 4: StopCoroutine(startBluffPokerClockIEnumerator()); break;
                default:
                    break;
            }
        }

        IEnumerator InRoomStartTimeIEnumerator()
        {
            while (mCount <= FourBullGlobalConst.sitWaitTime)
            {
                int curTime = FourBullGlobalConst.sitWaitTime - mCount;
                mText.text = curTime > 9 ? curTime.ToString() : "0" + curTime.ToString();
                yield return new WaitForSeconds(1);
                mCount++;
            }
            yield return null;
            mCountting = false;
            //退出房间代码
            gameObject.SetActive(false);
        }

        private void BetStartTime()
        {
            gameObject.SetActive(true);
            mText = transform.FindChild("text_clock").GetComponent<Text>();
            if (!mCountting)
            {
                mCount = 0;
                mCountting = true;
                StartCoroutine(BetStartTimeIEnumerator());
            }
        }

        IEnumerator BetStartTimeIEnumerator()
        {
            while (mCount <= FourBullGlobalConst.betWaitTime)
            {
                int curTime = FourBullGlobalConst.betWaitTime - mCount;
                mText.text = curTime > 9 ? curTime.ToString() : "0" + curTime.ToString();
                yield return new WaitForSeconds(1);
                mCount++;
            }
            yield return null;
            mCountting = false;
            //退出房间代码
            gameObject.SetActive(false);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }
    }
}