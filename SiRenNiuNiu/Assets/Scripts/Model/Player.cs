using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
//玩家类
public class Player {
	//玩家手牌
	public List<Poker> poker = new List<Poker>();
	public List<int> pokerValue = new List<int>();
	public List<GameObject> pokerGameObject = new List<GameObject>();
	public void AddPoker(Poker po){
		poker.Add(po);
	}

	public void AddPokerGameObject(GameObject poGame){
		pokerGameObject.Add(poGame);
	}

	public void AddPokerValue(int pokeValueNum){
		pokerValue.Add(pokeValueNum);
	}

	public string getPoints(){
		int []arrPokerValue = new int[global.roundMax];
		string []arrPokerColor = new string[global.roundMax];
		for(int i=0;i<poker.Count;i++){
			arrPokerValue[i] = poker[i].pokerPoints;
			arrPokerColor[i] = poker[i].pokerColor;
		}

		Judge.pokerValue = arrPokerValue;
		Judge.pokerColor = arrPokerColor;
		return Judge.getPokerPoint();
	}

}
