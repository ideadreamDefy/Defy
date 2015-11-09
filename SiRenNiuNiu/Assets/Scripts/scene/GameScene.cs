using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameScene : MonoBehaviour {
    // Use this for initialization

    // public GameObject gameO;
     
    // private GameObject ruleBtn =  transform.Find("/backCanvas/easyRuleBtn").gameObject;
    
    // private GameObject startLookBtn =  transform.Find("/backCanvas/startlookBtn").gameObject;
    

	void Start () {
        initPlayerInfo();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startBtnClicked() {
        startBtnHide();
        hidePlayerState();
        movePlayerIcon();
        showBtnRuleAndStartLook();
        GameStart();
    }
	// 开始按钮隐藏
    private void startBtnHide() {
        //玩家开始按钮隐藏
        var startBtn = transform.Find("/backCanvas/playerDownPanel/StartButton").gameObject;
        globalTool.reclaimUnUsedObject(startBtn);
    }

    private void hideBtnRuleAndStartLook(){
        // var ruleBtn =  transform.Find("/backCanvas/easyRuleBtn").gameObject;
        // var startLookBtn =  transform.Find("/backCanvas/startlookBtn").gameObject;
        // ruleBtn.SetActive(false);
        // startLookBtn.SetActive(false);
    }
	// 隐藏玩家准备状态
    private void hidePlayerState() {
        //隐藏玩家准备状态
        var playerLeft = transform.Find("/backCanvas/playerLeftPanel/playerState").gameObject;
        var playerRight = transform.Find("/backCanvas/playerRightPanel/playerState").gameObject;
        var playerTop = transform.Find("/backCanvas/playerTopPanel/playerState").gameObject;
        globalTool.reclaimUnUsedObject(playerLeft);
        globalTool.reclaimUnUsedObject(playerRight);
        globalTool.reclaimUnUsedObject(playerTop);
    }
	//移动玩家头像
    public void movePlayerIcon() {
        //移动玩家的Icon
		var playerLeft = transform.Find("/backCanvas/playerLeftPanel").gameObject;
		var playerLeftPos = transform.Find("/backCanvas/playerLeftPos").gameObject;

		var playeRight =  transform.Find("/backCanvas/playerRightPanel").gameObject;
		var playerRightPos = transform.Find("/backCanvas/playerRightPos").gameObject;
        
		var playerTop = transform.Find("/backCanvas/playerTopPanel").gameObject;
		var playerTopPos = transform.Find("/backCanvas/playerTopPos").gameObject;

		var playerDown = transform.Find("/backCanvas/playerDownPanel").gameObject;
		var playerDownPos = transform.Find("/backCanvas/playerDownPos").gameObject;
		globalTool.moveAction(playerLeft, playerLeftPos, global.iconMoveTime);
		globalTool.moveAction(playeRight, playerRightPos, global.iconMoveTime);
		globalTool.moveAction(playerTop, playerTopPos, global.iconMoveTime);
		globalTool.moveAction(playerDown, playerDownPos, global.iconMoveTime);
    }
	//游戏开始
    public void GameStart() {
        //var pokerObject = transform.Find("/PokerObject").gameObject;
        initZhuang();
        GameObject.Find("PokerObject").SendMessage("Shuffle"); 
    }
    //初始化庄家框
    private void initZhuang(){
        int zhuangIndex = UnityEngine.Random.Range(0, global.playerCount-1);
        switch (zhuangIndex){
                case 0:
					var playerDown = transform.Find("/backCanvas/playerDownPanel/roleKuang").GetComponent<Image>();
              		playerDown.sprite = globalTool.GetSpriteByFilePath("UI/bankerKuang");
					transform.Find("/backCanvas/playerDownPanel/zhuangImg").gameObject.SetActive(true);
                    break;
                case 1:
					var playerLeft = transform.Find("/backCanvas/playerLeftPanel/roleKuang").GetComponent<Image>();
                    playerLeft.sprite = globalTool.GetSpriteByFilePath("UI/bankerKuang");
					transform.Find("/backCanvas/playerLeftPanel/zhuangImg").gameObject.SetActive(true);
                    break;
                case 2:
					var playerTop = transform.Find("/backCanvas/playerTopPanel/roleKuang").GetComponent<Image>();
                    playerTop.sprite = globalTool.GetSpriteByFilePath("UI/bankerKuang");
					transform.Find("/backCanvas/playerTopPanel/zhuangImg").gameObject.SetActive(true);
                    break;
                case 3:
					var playerRight = transform.Find("/backCanvas/playerRightPanel/roleKuang").GetComponent<Image>();;
                    playerRight.sprite = globalTool.GetSpriteByFilePath("UI/bankerKuang");
					transform.Find("/backCanvas/playerRightPanel/zhuangImg").gameObject.SetActive(true);
                    break;
				default:
					Console.WriteLine(zhuangIndex);
                    break;
            }
    }
    
    public void initPlayerInfo()
    {
        
        var playerLeft = transform.Find("/backCanvas/playerLeftPanel").gameObject;
        Image imageLeft = playerLeft.AddComponent<Image>();
        
        //各个位置的庄家图标
		transform.Find("/backCanvas/playerRightPanel/zhuangImg").gameObject.SetActive(false);
		transform.Find("/backCanvas/playerTopPanel/zhuangImg").gameObject.SetActive(false);
		transform.Find("/backCanvas/playerLeftPanel/zhuangImg").gameObject.SetActive(false);
		transform.Find("/backCanvas/playerDownPanel/zhuangImg").gameObject.SetActive(false);

        imageLeft.sprite = globalTool.GetSpriteByFilePath("Icon/icon");
        imageLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

        var playerRight = transform.Find("/backCanvas/playerRightPanel").gameObject;
        Image imageRight = playerRight.AddComponent<Image>();

        imageRight.sprite = globalTool.GetSpriteByFilePath("Icon/icon");
        imageRight.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

        var playerTop = transform.Find("/backCanvas/playerTopPanel").gameObject;
        Image imageTop = playerTop.AddComponent<Image>();

		imageTop.sprite = globalTool.GetSpriteByFilePath("Icon/icon");
        imageTop.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

        var playerDown = transform.Find("/backCanvas/playerDownPanel").gameObject;
        Image imageDown = playerDown.AddComponent<Image>();

        imageDown.sprite = globalTool.GetSpriteByFilePath("Icon/icon");
        imageDown.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

        hideBtnRuleAndStartLook();
    }
    //上手查看按钮
    public void startLookBtnClicked()
    {
        Console.WriteLine("startLookBtnClicked");
    }
    // 简易规则查看
    public void easyRuleBtnClicked()
    {
        Console.WriteLine("easyRuleBtnClicked");
    }

    private void showBtnRuleAndStartLook()
    {
        // var ruleBtn =  transform.Find("/backCanvas/easyRuleBtn").gameObject;
        // var startLookBtn =  transform.Find("/backCanvas/startlookBtn").gameObject;
        // ruleBtn.SetActive(true);
        // startLookBtn.SetActive(true);
    }
    
}
