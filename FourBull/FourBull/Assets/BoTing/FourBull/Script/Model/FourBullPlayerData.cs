/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：NewBehaviourScript
 * 简    述：
 * 创建时间：#TIME#
 * 创 建 人：=== Coco ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using BoTing.GamePublic;
namespace BoTing.FourBull
{
    public class FourBullPlayerData
    {
        public PokerCard [,] playerPokerSet = new PokerCard[4,5];
        private static FourBullPlayerData _instance = null;
        private FourBullPlayerData()
        {
                
        }

        public static FourBullPlayerData getInstance()
        {
            if (_instance == null)
            {
                _instance = new FourBullPlayerData();
            }
            return _instance;
        }
        //初始化数据
        public void initData(PokerCard[,]  playerData)
        {
            playerPokerSet = playerData;
        }


    }
}