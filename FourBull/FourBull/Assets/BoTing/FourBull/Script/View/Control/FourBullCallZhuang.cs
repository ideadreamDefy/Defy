/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullCallZhuang.cs
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.Framework;
using UnityEngine.EventSystems;
using BoTing.GamePublic;

namespace BoTing.FourBull {
    public class FourBullCallZhuang : ViewBase
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
            FourBull.Messager.AddListener(FourBullEvent.StartCallZhuang, ShowCallZhuangObject);
            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener(FourBullEvent.StartCallZhuang, ShowCallZhuangObject);
            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        private void initObjectView()
        {
            gameObject.SetActive(false);
        }

        private void ShowCallZhuangObject()
        {
            
            //抢庄按钮
            GameObject CallBtnObject = transform.FindChild("btnCall").gameObject;
            EventTriggerAssist.Get(CallBtnObject).onClick(OnCall);
            //不抢庄按钮
            GameObject noCallBtnObject = transform.FindChild("btnNoCall").gameObject;
            EventTriggerAssist.Get(noCallBtnObject).onClick(OnNoCall);
            gameObject.SetActive(true);
            //叫庄定时开启
            FourBull.Messager.Broadcast(FourBullEvent.StartCallByClock);
        }

        //叫庄请求
        private void OnCall(PointerEventData data)
        {
            CMD_C_CallBanker callBanker = new CMD_C_CallBanker();
            callBanker.bBanker = 1;
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_CALL_BANKER, callBanker);
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 2);
            gameObject.SetActive(false);
            
        }

        //不叫请求
        private void OnNoCall(PointerEventData data)
        {
            CMD_C_CallBanker callBanker = new CMD_C_CallBanker();
            callBanker.bBanker = 0;
            FourBullCommand.Instance.SendGamePacket((ushort)PROTOCOL_CLIENT.SUB_C_CALL_BANKER, callBanker);
            //关闭叫庄定时器
            FourBull.Messager.Broadcast(FourBullEvent.StopClock, 2);
            gameObject.SetActive(false);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }
    }
}