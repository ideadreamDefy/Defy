using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//全局变量
public class global : MonoBehaviour {
    //头像准备移动时间
    public static float iconMoveTime = 0.5f;
    //总扑克数量
    public static int pokerCount = 54;
    //每位玩家的最大手牌数
    public static int roundMax = 5;
    //玩家数量
    public static int playerCount = 4;
    //押注等待时间
	public static int waitTime = 2;
    //花色图片资源名称路径
    public static string [] colorResPath = {"poker/hearts_","poker/plum_","poker/spade_","poker/Squal_"};
    //牌组游戏点数种类数量
    public static int PointsCount = 15;

    
}





