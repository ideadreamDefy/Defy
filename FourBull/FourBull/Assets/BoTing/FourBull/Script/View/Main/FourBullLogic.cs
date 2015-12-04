using UnityEngine;
using BoTing.GamePublic;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BoTing.FourBull
{
    public class FourBullLogic
    {
        private static FourBullLogic _instance = null;
        private FourBullLogic()
        {


        }

        public static FourBullLogic getInstance()
        {
            if (_instance == null)
            {
                _instance = new FourBullLogic();
            }
            return _instance;

        }

        //是拍组是否同一花色
        public static bool isSameColor(PokerCard[] playerArray)
        {
            bool isRepeat = true;
            int[] array = new int[playerArray.Length];
            for (int i = 0; i < playerArray.Length; i++)
            {
                array[i] = int.Parse(playerArray[i].FrontSpriteName);
                if (array[i] == 52 || array[i] == 53)
                {
                    isRepeat = false;
                    return isRepeat;
                }
            }

            Array.Sort(array);

            if ((array[4] - array[0] <12) && (array[4] - (int)(array[4] % 13))== (array[1] - (int)(array[1] % 13)))
            {
                isRepeat = true;
                return isRepeat;
            }
            else
            {
                isRepeat = false;
                return isRepeat;
            }
        }

        ////牌组是否有大小王
        public bool isHaveWangPoker(PokerCard []playerArray)
        {
            for (int i = 0; i < playerArray.Length; i++)
            {
                if (playerArray[i].FrontSpriteName.Equals("52")|| playerArray[i].FrontSpriteName.Equals("53"))
                {
                    return true;
                }
            }
            return false;
        }

        //牌组是否为炸弹
        public bool isBoom(PokerCard[] playerArray)
        {
            int[] array = new int[playerArray.Length];
            for (int i = 0; i < playerArray.Length; i++)
            {
                int num = int.Parse(playerArray[i].FrontSpriteName);
                if (num != 52 && num != 53)
                {
                    array[i] = (int)(num % 13) + 1;
                }
                else {
                    array[i] = num;
                }
            }

            Array.Sort(array);

            //有序数组,当arr[0] == arr[3] 或者 arr[1] == arr[4] 前四个数字或者后四个数字相等
            if (array[0] == array[3] || array[1] == array[4])
            {
                return true;
            }
            return false;
        }

        //是否为顺子
        public  bool isShunzi(PokerCard [] playerArray)
        {
            int[] array = new int[playerArray.Length];
            for (int i = 0; i < playerArray.Length; i++)
            {
                int num = int.Parse(playerArray[i].FrontSpriteName);
                if (num != 52 && num != 53)
                    array[i] = (int)(num % 13) + 1;
                else {
                    array[i] = num;
                }
            }
            //排序
            Array.Sort(array);
            //去重
            HashSet<string> ht = new HashSet<string>();
            for (int i = 0; i < 5; i++)
            {
                ht.Add(playerArray[i].FrontSpriteName);
            }

            if (Math.Abs(array[4] - array[0]) == 4 && ht.Count == 5)
            {
                return true;
            }
            return false;
        }

        //是否有牛
        internal PokerStructInfo pokerPoints(PokerCard [] playerArray)
        {
            PokerStructInfo ps = new PokerStructInfo();
            ps.Points = -1;
            ps.first = -1;
            ps.second = -1;
            //int Points;//算牛的点数
            //int first;//相应凑成牛点数的牌
            //int second;//
            int[] array = new int[playerArray.Length];
            for (int i = 0; i < playerArray.Length; i++)
            {
                int num = int.Parse(playerArray[i].FrontSpriteName);
                array[i] = (int)(num% 13)+1;
            }
            int totalPoints = 0;
            for (int i = 0; i < 5; i++)
            {
                //---------------------------------------------//
                if (array[i] > 10)
                {
                    array[i] = 10;
                }
                //---------------------------------------------//
                totalPoints = totalPoints + array[i];
            }

            for (int first = 0; first < 5; first++)
            {
                for (int second = 1; second < 5; second++)
                {
                    if (first != second)
                    {
                        int temp = totalPoints - array[first] - array[second];
                        if (temp % 10 == 0)
                        {
                            int points = array[first] + array[second];
                            if (points % 10 == 0)
                            {
                                ps.Points = 100;
                                ps.first = first;
                                ps.second = second;

                                return ps;
                            }
                            else
                            {
                                ps.Points = (array[first] + array[second]) % 10;
                                ps.first = first;
                                ps.second = second;
                                return ps;
                            }
                        }
                    }
                }
            }
            return ps;
        }
        //得到拍组颜色
        public  string getPokerPoint(PokerCard[] playerArray)
        {
            int points = pokerPoints(playerArray).Points;
            // 当牌组中无大小王时
            if (!isHaveWangPoker(playerArray))
            {
                if (isBoom(playerArray) && points != -1)
                {
                    return "niu_HuaZha"; // 花炸牛
                }
                else if (isSameColor(playerArray) && points != -1)
                {
                    return "niu_Tonghua";//同花牛
                }
                else if (isShunzi(playerArray) && points != -1)
                {
                    return "niu_Shunzi";//顺子牛
                }
                else
                {
                    if (points == -1)
                    {
                        return "niu_wu";//无牛
                    }
                    else
                    {
                        if (points == 100)
                        {
                            return "niu_niu";//牛牛
                        }
                        else
                        {
                            return "niu_" + points.ToString();
                        }
                    }
                }
            }
            else
            {
                if (isBoom(playerArray) && points != -1)
                {
                    return "niu_HuaZha"; // 花炸牛
                }
                else
                {
                    if (points == -1)
                    {
                        return "niu_wu";//无牛
                    }
                    else
                    {
                        if (points == 100)
                        {
                            return "niu_niu";//牛牛
                        }
                        else
                        {
                            return "niu_" + points.ToString();
                        }
                    }
                }
            }
        }
    }
}

