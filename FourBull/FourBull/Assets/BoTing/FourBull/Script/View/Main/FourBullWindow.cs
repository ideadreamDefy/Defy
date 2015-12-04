using BoTing.Framework;
using System.Collections.Generic;
using BoTing.GamePublic;

namespace BoTing.FourBull
{
	public class FourBullWindow : View
	{
        private FourBullCommand command;
        private GameLayout layout;

        //设置界面标识ViewName
        protected override string ViewName
        {
            get { return "FourBull"; }
        }


        //显示UI并开始干活！
        void Start()
        {
            command = (FourBullCommand)viewCommand;
            layout = transform.GetComponent<GameLayout>();
            InitCommponetView();
            //do your work!!
            command.OnAttachedView();

        }

        //当界面销毁时候调用
        void OnDestroy()
        {
            command.ViewRoot = null;
            command.OnReleasedView();

        }

        //界面ui已经创建好，准备初始化。后于OnCreateView执行
        void InitCommponetView()
    	{

            //playerLeftPanel捆绑脚本 initView初始化
            ViewBase playerLeftPanelScript = layout.BottomCenterNode.FindChild("ControlPanel/playerLeftPanel").GetComponent<ViewBase>();
            playerLeftPanelScript.InitView();
            //playerDownPanel捆绑脚本 initView初始化
            ViewBase playerDownPanelScript = layout.BottomCenterNode.FindChild("ControlPanel/playerDownPanel").GetComponent<ViewBase>();
            playerDownPanelScript.InitView();
            //playerRightPanel捆绑脚本 initView初始化
            ViewBase playerRightPanelScript = layout.BottomCenterNode.FindChild("ControlPanel/playerRightPanel").GetComponent<ViewBase>();
            playerRightPanelScript.InitView();
            //playerTopPanel捆绑脚本 initView初始化
            ViewBase playerTopPanelScript = layout.BottomCenterNode.FindChild("ControlPanel/playerTopPanel").GetComponent<ViewBase>();
            playerTopPanelScript.InitView();
            //controlNode initView初始化-----> FourBullControlView.cs
            ViewBase controlNodeScript = layout.BottomCenterNode.FindChild("ControlPanel/controlNode").GetComponent<ViewBase>();
            controlNodeScript.InitView();
            //倒计时组件 initView初始化-----> FourBullClock.cs
            ViewBase clockScript = layout.BottomCenterNode.FindChild("ControlPanel/timeObject").GetComponent<ViewBase>();
            clockScript.InitView();
            //叫庄组件initView ----->FourBullCallZhuang.cs
            ViewBase callZhuangScript = layout.BottomCenterNode.FindChild("ControlPanel/callZhuangObject").GetComponent<ViewBase>();
            callZhuangScript.InitView();
            //下注组件initView ------>FourBullBet.cs
            ViewBase betScript = layout.BottomCenterNode.FindChild("ControlPanel/betObject").GetComponent<ViewBase>();
            betScript.InitView();
            //摊牌
            ViewBase downPlayerObjectScript = layout.BottomCenterNode.FindChild("ControlPanel/downPlayerObject").GetComponent<ViewBase>();
            downPlayerObjectScript.InitView();
            //显示牛牛和牌
            ViewBase downPlayerCowScript = layout.BottomCenterNode.FindChild("ControlPanel/playerDownCowObject").GetComponent<ViewBase>();
            downPlayerCowScript.InitView();
            //显示牛牛和牌
            ViewBase topPlayerCowScript = layout.BottomCenterNode.FindChild("ControlPanel/playerTopCowObject").GetComponent<ViewBase>();
            topPlayerCowScript.InitView();
            //显示牛牛和牌
            ViewBase rightPlayerCowScript = layout.BottomCenterNode.FindChild("ControlPanel/playerRightCowObject").GetComponent<ViewBase>();
            rightPlayerCowScript.InitView();
            //显示牛牛和牌
            ViewBase leftPlayerCowScript = layout.BottomCenterNode.FindChild("ControlPanel/playerLeftCowObject").GetComponent<ViewBase>();
            leftPlayerCowScript.InitView();
            //结算面板
            ViewBase resultObjectScript = layout.ForegroundNode.FindChild("resultObject").GetComponent<ViewBase>();
            resultObjectScript.InitView();
        }
    }
}


