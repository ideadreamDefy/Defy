
namespace BoTing.FourBull
{
	//用于定义commander向view发送消息
	public class FourBullEvent
	{
        //四人座
        public const string TablePosZeroPlayerInfo = "FourBull_TablePosZeroInfo_Message"; //第一个位置

        public const string TablePosOnePlayerInfo = "FourBull_TablePosOneInfo_Message"; //第二个位置

        public const string TablePosTwoPlayerInfo = "FourBull_TablePosTwoInfo_Message"; //第三个位置

        public const string TablePosThreePlayerInfo = "FourBull_TablePosThreeInfo_Message"; //第四个位置

        public const string TablePosZeroPlayerLeave = "FourBull_TablePosZeroLeave_Message"; //第一个位置的人离开

        public const string TablePosOnePlayerLeave = "FourBull_TablePosOneLeave_Message"; //第二个位置的人离开

        public const string TablePosTwoPlayerLeave = "FourBull_TablePosTwoLeave_Message"; //第三个位置的人离开

        public const string TablePosThreePlayerLeave = "FourBull_TablePosThreeLeave_Message"; //第四个位置的人离开

        public const string TablePosZeroIsReady = "FourBull_TablePosZeroIsReady_Message";//第一个位置的人准备

        public const string TablePosOneIsReady = "FourBull_TablePosOneIsReady_Message";//第二个位置的人准备

        public const string TablePosTwoIsReady = "FourBull_TablePosTwoIsReady_Message";//第三个位置的人准备

        public const string TablePosThreeIsReady = "FourBull_TablePosThreeIsReady_Message";//第四个位置的人准备

        public const string BtnStartMoveIcon = "FourBull_BtnStartMoveIcon_Message";//移动Icon

        public const string InRoomClockStart = "FourBull_InRoomClockStart_Message"; //进房间倒计时

        public const string StopClock = "FourBull_StopClock_Message"; //进房间倒计时

        public const string StartCallZhuang = "FourBull_StartCallZhuang_Message";//开始叫庄

        public const string StartCallByClock = "FourBull_StartCallByClock_Message";//叫庄开始倒计时

        public const string HidePlayerState = "FourBull_HidePlayerState_Message";//隐藏玩家状态

        public const string TablePosZeroPlayerIsBanker = "FourBull_TablePosZeroPlayerIsBanker_Message";//第一个为庄家（down）

        public const string TablePosOnePlayerIsBanker = "FourBull_TablePosOnePlayerIsBanker_Message";//第一个为庄家（Right）

        public const string TablePosTwoPlayerIsBanker = "FourBull_TablePosTwoPlayerIsBanker_Message";//第一个为庄家（Top）

        public const string TablePosThreePlayerIsBanker = "FourBull_TablePosThreePlayerIsBanker_Message";//第一个为庄家（Left）

        public const string ShowBetComponet = "FourBull_ShowBetComponet_Message"; //显示下注组件

        public const string StartBetClock = "FourBull_StartBetClock_Message";//开始下注定时器

        public const string BetPosZero = "FourBull_BetPosZero_Message";//第一个玩家的下注

        public const string BetPosOne = "FourBull_BetPosOne_Message";//第一个玩家的下注 

        public const string BetPosTwo = "FourBull_BetPosTwo_Message";//第一个玩家的下注 

        public const string BetPosThree = "FourBull_BetPosThree_Message";//第一个玩家的下注 

        public const string StartdealPoker = "FourBull_StartdealPoker_Message";//开始发牌

        public const string hidePlayerOne = "FourBull_hidePlayerOne_Message";//隐藏位置右的人

        public const string hidePlayerTwo = "FourBull_hidePlayerTwo_Message";//隐藏位置上的人

        public const string hidePlayerThree = "FourBull_hidePlayerThree_Message";//隐藏位置左的人 

        public const string bluffPokerClock = "FourBull_bluffPokerClock_Message";//摊牌倒计时

        public const string showPoker = "FourBull_showPoker_Message";//显示确定按钮（摊牌）

        public const string showCow = "FourBull_showCow_Message";//显示牛

        public const string showTopCow = "FourBull_showTopCow_Message";

        public const string showDownCow = "FourBull_showDownCow_Message";

        public const string showLeftCow = "FourBull_showLeftCow_Message";

        public const string showRightCow = "FourBull_showRightCow_Message";

        public const string showResult = "FourBull_showResult_Message"; //显示结算面板

        public const string resetGame = "FourBull_resetGame_Message";//重置游戏

        //public const string readyPos1 = "FourBull_readyPos1_Message"; //右边玩家准备中

        //public const string readyPos2 = "FourBull_readyPos2_Message";//顶上玩家准备中

        //public const string readyPos3 = "FourBull_readyPos3_Message";//左边玩家准备中

        public const string isCallBanker1 = "FourBull_isCallBanker1_Message";

        public const string isCallBanker2 = "FourBull_isCallBanker2_Message";

        public const string isCallBanker3= "FourBull_isCallBanker3_Message";

        public const string showMyPoker = "FourBull_showMyPoker_Message";



    }

}
