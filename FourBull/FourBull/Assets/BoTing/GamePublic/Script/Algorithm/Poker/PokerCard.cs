using System;
using UnityEngine;

namespace BoTing.GamePublic
{

    public class PokerCard: ICloneable, IComparable
    {
        /// <summary>
        /// 牌的正面所用图名
        /// </summary>
        public string FrontSpriteName { get; private set; }

        /// <summary>
        /// 牌的背面所用图名
        /// </summary>
        public string BackSpriteName { get; private set; }

        /// <summary>
        /// 花色
        /// </summary>
        public PokerStylor Stylor { get; private set; }

        /// <summary>
        /// 牌值
        /// <para>1-13 A-K</para>
        /// <para>14 小王</para>
        /// <para>15 大王</para>
        /// </summary>
        public int Mark { get; private set; }

        /// <summary>
        /// 是否明牌
        /// </summary>
        public bool ShowingFront { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 触发拖选
        /// </summary>
        public bool IsEnter { get; set; }

        /// <summary>
        /// 相同牌数，如果2222值为4; 222值为3;
        /// </summary>
        public int SameCardNum { get; set; }

        /// <summary>
        /// 大（小）王的替换牌值
        /// </summary>
        public int JokerReplaceWeight { get; set; }

        /// <summary>
        /// 扑克所有牌的 Sprite
        /// </summary>
        public Sprite CardSprite { get; set; }

        /// <summary>
        /// 服务端牌值
        /// </summary>
        public int ServerValue { get; private set; }

        public PokerCard()
        {
            ServerValue = 0;
            ShowingFront = false;
            BackSpriteName = PokerConst.Background;
            FrontSpriteName = PokerConst.Background;
        }

        /// <summary>
        /// 设置正面牌名
        /// <para>默认明牌</para>
        /// <para>服务端传来的值为0时，正面牌名为空, 牌值为0，花色为 ErrorStylor</para>
        /// </summary>
        /// <param name="cardValue">服务端传来的牌值</param>
        /// <param name="showCard">是否明牌</param>
        public void SetFront(int cardValue, bool showCard = true)
        {
            ServerValue = cardValue;
            ShowingFront = showCard;
            if (cardValue != 0)
            {
                Stylor = (PokerStylor)((cardValue & 0xF0) / 16);
                Mark = cardValue & 0x0F;

                if (Stylor == PokerStylor.King)
                {
                    if (Mark == 14)
                    {
                        FrontSpriteName = "52";
                    }
                    else if (Mark == 15)
                    {
                        FrontSpriteName = "53";
                    }
                }
                else
                {
                    FrontSpriteName = ((int)Stylor * 13 + Mark - 1).ToString();
                }
            }
            else
            {
                Stylor = PokerStylor.ErrorStylor;
                Mark = 0;
                FrontSpriteName = PokerConst.Background;
            }
        }

        /// <summary>
        /// 克隆自己的对象
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// 排序 List<PokerCard> pokerCardList.Sort();
        /// </summary>
        public virtual int CompareTo(object obj)
        {
            if (!(obj is PokerCard))
            {
                throw new ArgumentException("类型必须为 PokerCard 类型");
            }
            return Mark.CompareTo(((PokerCard)obj).Mark);
        }
    }
}