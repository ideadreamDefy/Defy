using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

public class Judge{`
    private string [] paiZuRank = new string[]{
        "huazhaniu",//花顺牛
        "huazhaniu",//花炸牛
        "tonghuaniu",//同花牛
        "shunziniu",//顺子牛
        "niuniu",//牛牛
        "wuniu",//无牛
        "niu1",//牛一
        "niu2",//牛二
        "niu3",//牛三
        "niu4",//牛四
        "niu5",//牛五
        "niu6",//牛六
        "niu7",//牛七
        "niu8",//牛八
        "niu9",//牛九
    };
    private int[] pokerValue = new int[5];
    private string[] pokerColor = new string[5];
    private bool isHaveWang = false;
    
    //是拍组是否同一花色
    public bool isSameColor(string []pokerNameString){
        bool isRepeat = true;
        HashSet<string> pokerKind = new HashSet<string>();
        for(int i = 0;i<pokerColor.Length;i++){
			if (pokerColor.Equals("zhengWang")||pokerColor.Equals("fuWang")){
				isRepeat = false;
                isHaveWang = true;
				return isRepeat;
			}

            if (!pokerKind.Add(pokerColor[i])){
				isRepeat = false;
                return isRepeat;
            }
        }
		return isRepeat;
    } 
    //处理图片名称分解得到牌花色和大小
    public void getPaiZu(string[] pokerNameArray){
        char[] separator = { '_' };
        for(int i = 0 ;i<pokerNameArray.Length;i++){
            string temp = pokerNameArray[i];
			string[] arr = temp.Split(separator);
			int tempInt = Int32.Parse(arr[2]);
			if (tempInt>10){
                pokerValue[i] = 10;
            }
			pokerValue[i] = Int32.Parse(arr[2]);
            pokerColor[i] = arr[1];
        }
    }
    
    //牌组是否为炸弹
	public bool  isBoom(){
		int [] array = new int[5];
		Array.Copy(array,pokerValue,pokerValue.Length);
		Array.Sort(array);
		//有序数组,当arr[0] == arr[3] 或者 arr[1] == arr[4] 前四个数字或者后四个数字相等
		if ((array[0] == array[3] )||array[1] == array[4]){
			return true;
		}
		return false;
	}
    
    //牌组是否有大小王
    public bool isHaveWangPoker(){
		return isHaveWang;
    }

    //是否为顺子
    public bool isShunzi(){
		int [] array = new int[5];
		Array.Copy(array,pokerValue,pokerValue.Length);
        //排序
        Array.Sort(array);
        //去重
        HashSet<string> ht = new HashSet<string>();
        for(int i = 0 ;i< 5;i++){
            ht.Add(pokerValue[i].ToString());
        }
        
        if(Math.Abs(array[4]- array[0])== 4 && ht.Count == 5){
            return true;
        }
        return false;   
    }

	public int pokerPoints(){
        
	}
}
