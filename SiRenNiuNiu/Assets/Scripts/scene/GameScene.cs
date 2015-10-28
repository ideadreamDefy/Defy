using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameScene : MonoBehaviour {
    // Use this for initialization

    public GameObject gameO;

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
        GameStart();
    }

    public void startBtnHide() {
        //玩家开始按钮隐藏
        var startBtn = transform.Find("/backCanvas/playerDownPanel/StartButton").gameObject;
        globalTool.reclaimUnUsedObject(startBtn);
    }
    
    public void hidePlayerState() {
        //隐藏玩家准备状态
        var playerLeft = transform.Find("/backCanvas/playerLeftPanel/playerState").gameObject;
        var playerRight = transform.Find("/backCanvas/playerRightPanel/playerState").gameObject;
        var playerTop = transform.Find("/backCanvas/playerTopPanel/playerState").gameObject;
        globalTool.reclaimUnUsedObject(playerLeft);
        globalTool.reclaimUnUsedObject(playerRight);
        globalTool.reclaimUnUsedObject(playerTop);
    }

    public void movePlayerIcon() {
        //移动玩家的Icon
       var playerLeft = transform.Find("/backCanvas/playerLeftPanel").gameObject;
       var playerLeftPos = transform.Find("/backCanvas/playerLeftPos").gameObject;

       var playeRight = transform.Find("/backCanvas/playerRightPanel").gameObject;
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

    public void GameStart() {
        //var pokerObject = transform.Find("/PokerObject").gameObject;
        GameObject.Find("PokerObject").SendMessage("Shuffle"); 
    }

    public void initPlayerInfo()
    {
        var playerLeft = transform.Find("/backCanvas/playerLeftPanel").gameObject;
        Image imageLeft = playerLeft.AddComponent<Image>();

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
    }
}
