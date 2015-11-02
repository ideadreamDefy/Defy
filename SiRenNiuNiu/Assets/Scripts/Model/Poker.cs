using UnityEngine;
using System.Collections;

//扑克牌类
public class Poker{
    //扑克牌大小
    public int pokerPoints;
    //扑克牌花色
    public string pokerColor;
    //扑克位置
    public int pokerPos;
    //扑克类构造方法
    public Poker(string pokeImgName,int pokerPos){
        //扑克名称 "spade_2"
        char[] separator = { '_' };
        string temp = pokeImgName;
        string[] arr = temp.Split(separator);
        int tempInt = int.Parse(arr[1]);
        //当数字大于10时，取10
        
		this.pokerPos = pokerPos;
		this.pokerPoints = tempInt;
        this.pokerColor = arr[0];
    }
}
