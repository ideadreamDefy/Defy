using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Judge{
	public static int[] pokerValue = new int[5];
	public static string[] pokerColor = new string[5];

    //是拍组是否同一花色
	public static bool isSameColor(){
        bool isRepeat = true;
        HashSet<string> pokerKind = new HashSet<string>();
        for(int i = 0;i<pokerColor.Length;i++){
			if (pokerColor.Equals("zhengWang")||pokerColor.Equals("fuWang")){
				isRepeat = false;
				return isRepeat;
			}

            if (pokerKind.Add(pokerColor[i])){
				isRepeat = false;
                return isRepeat;
            }
        }
		return isRepeat;
    } 

	//牌组是否有大小王
	public static bool isHaveWangPoker(){
		for (int i = 0 ;i<5;i++){
			if (pokerColor.Equals("zhengWang")||pokerColor.Equals("fuWang")){
				return true;
			}
		}
		return false;
	}

    //牌组是否为炸弹
	public static bool  isBoom(){
		int [] array = new int[5];
		Array.Copy(pokerValue,array,pokerValue.Length);
		Array.Sort(array);
		//有序数组,当arr[0] == arr[3] 或者 arr[1] == arr[4] 前四个数字或者后四个数字相等
		if (array[0] == array[3] ||array[1] == array[4]){
			return true;
		}
		return false;
	}
  	
    //是否为顺子
	public static bool isShunzi(){
		int [] array = new int[5];
		Array.Copy(pokerValue,array,pokerValue.Length);
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

    //是否有牛
	public static int pokerPoints(){
        int [] array = new int[5];
		Array.Copy(pokerValue,array,pokerValue.Length);
        int totalPoints = 0;
        for(int i =0 ;i<5;i++){
			//---------------------------------------------//
			if (array[i]>10){
				array[i] = 10;
			}
			//---------------------------------------------//
            totalPoints = totalPoints + array[i];
        }

        for(int first = 0;first< 5;first++){
            for(int second = 1;second<5;second++){
                if (first!=second){
                    int  temp = totalPoints - array[first]-array[second]; 
                    if (temp%10 == 0){
                        int points = array[first]+array[second];
                        if (points%10 == 0){
                            return 100;
                        }else{
                            return (array[first]+array[second])%10;
                        }
                    }
                }
            }
        }
       	
		return -1;
	}

    public static string getPokerPoint(){
        int points = pokerPoints();
        // 当牌组中无大小王时
		if(!isHaveWangPoker()){
            if(isBoom()&&points != -1){
                return "niu_HuaZha"; // 花炸牛
            }else if(isSameColor() && points!= -1){
                return "niu_Tonghua";//同花牛
            }else if(isShunzi() && points!= -1){
                return "niu_Shunzi";//顺子牛
            }else{
                if ( points== -1){
                    return "niu_wu";//无牛
                }else{
                    if (points == 100){
                        return "niu_niu";//牛牛
                    }else{
                        return "niu_"+points.ToString();
                    }
                }
            }
        }else{
			if(isBoom()&&points != -1){
				return "niu_HuaZha"; // 花炸牛
			}else{
				if ( points== -1){
					return "niu_wu";//无牛
				}else{
					if (points == 100){
						return "niu_niu";//牛牛
					}else{
						return "niu_"+points.ToString();
					}
				}
			}
        }
    }
}
