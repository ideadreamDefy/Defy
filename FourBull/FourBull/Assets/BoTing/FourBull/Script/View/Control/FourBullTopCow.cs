/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：FourBullTopCow
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== 这里写自己的名字 ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.Framework;
using System;
using BoTing.GamePublic;
using UnityEngine.UI;

namespace BoTing.FourBull
{
    public class FourBullTopCow : ViewBase
    {
        void Start()
        {

        }
        void Update()
        {

        }

        public override void InitView()
        {
            initCowView();

            FourBull.Messager.AddListener<string>(FourBullEvent.showTopCow, showPokerList);

            FourBull.Messager.AddListener(FourBullEvent.resetGame, ResetView);

            gameObject.SetActive(false);
        }

        private void initCowView()
        {
            Transform[] prefabSet = transform.FindChild("cowObject").GetComponentsInChildren<Transform>();
            for (int index = 0; index < prefabSet.Length; index++)
            {
                prefabSet[index].gameObject.SetActive(false);
            }
        }

        void OnDestory()
        {
            FourBull.Messager.RemoveListener<string>(FourBullEvent.showTopCow, showPokerList);

            FourBull.Messager.RemoveListener(FourBullEvent.resetGame, ResetView);
        }

        private void showPokerList(string cowPoints)
        {
            
            PokerCard[] p = new PokerCard[5];
            for (int i = 0; i < 5; i++)
            {
                p[i] = FourBullPlayerData.getInstance().playerPokerSet[2, i];
            }

            PokerStructInfo ps = FourBullLogic.getInstance().pokerPoints(p);
            if (ps.Points > 0)
            {
                int target = 1;
                for (int index = 0; index < 5; index++)
                {
                    if (index != ps.first && index != ps.second)
                    {
                        transform.FindChild("poker" + target).GetComponent<Image>().sprite = FourBull.PublicLoader.LoadCard(FourBullPlayerData.getInstance().playerPokerSet[2, index].FrontSpriteName);
                        target++;
                    }
                }
                transform.FindChild("poker" + 4).GetComponent<Image>().sprite = FourBull.PublicLoader.LoadCard(FourBullPlayerData.getInstance().playerPokerSet[2, ps.first].FrontSpriteName);
                transform.FindChild("poker" + 5).GetComponent<Image>().sprite = FourBull.PublicLoader.LoadCard(FourBullPlayerData.getInstance().playerPokerSet[2, ps.second].FrontSpriteName);
            }
            else {
                for (int index = 0; index < 5; index++)
                {
                    transform.FindChild("poker" + (index+1)).GetComponent<Image>().sprite = FourBull.PublicLoader.LoadCard(FourBullPlayerData.getInstance().playerPokerSet[2, index].FrontSpriteName);
                }

            }

            //显示牛数
            Transform[] prefabSet = transform.FindChild("cowObject").GetComponentsInChildren<Transform>();
            for (int index = 0; index < prefabSet.Length; index++)
            {
                if (prefabSet[index].name.Equals(cowPoints))
                {
                    prefabSet[index].gameObject.SetActive(true);
                }
                else
                {
                    prefabSet[index].gameObject.SetActive(false);
                }
            }
            gameObject.SetActive(true);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }
    }
}