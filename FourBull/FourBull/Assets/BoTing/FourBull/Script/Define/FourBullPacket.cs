using System.ComponentModel;
using System.Runtime.InteropServices;
enum PROTOCOL_PACKET
{
    [Description("游戏 I D ")]
    KIND_ID = 205,
    [Description("游戏人数 ")]
    GAME_PLAYER = 4,
    //[Description("游戏名字 ")]
    //GAME_NAME = "四人斗牛",
    [Description("最大数目 ")]
    MAX_COUNT = 5,
    [Description("下注区域 ")]
    MAX_JETTON_AREA = 4,
    [Description("最大赔率")]
    MAX_TIMES = 5,
    //结束原因
    [Description("没有玩家")]
    GER_NO_PLAYER = 0x10,

    //游戏状态
    [Description("等待开始")]
    GS_TK_FREE = 0,
    [Description("叫庄状态")]
    GS_TK_CALL = 100,
    [Description("下注状态")]
    GS_TK_SCORE = 101,
    [Description("游戏进行")]
    GS_TK_PLAYING = 102,

    SERVER_LEN = 32,
    //服务器命令结构 
    [Description("游戏开始")]
    SUB_S_GAME_START = 100,
    [Description("加注结果")]
    SUB_S_ADD_SCORE = 101,
    [Description("用户强退")]
    SUB_S_PLAYER_EXIT = 102,
    [Description("发牌消息")]
    SUB_S_SEND_CARD = 103,
    [Description("游戏结束")]
    SUB_S_GAME_END = 104,
    [Description("用户摊牌")]
    SUB_S_OPEN_CARD = 105,
    [Description("用户叫庄")]
    SUB_S_CALL_BANKER = 106,
    [Description("发牌消息")]
    SUB_S_ALL_CARD = 107,
    [Description("系统控制")]
    SUB_S_AMDIN_COMMAND = 108,
    [Description("存取款")]
    SUB_S_BANKER_OPERATE = 109,
    [Description("更新库存")]
    SUB_S_UPDATE_STORAGE = 110,
    [Description("控制成功")]
    SUB_S_WINLOST_RESULT = 111,
    [Description("锁定信息")]
    SUB_S_LOOKTABLEINFO = 112,
};

struct tagSGetBonsesInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
    public string[] szNickname;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
    public string[] szCardData;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public long[] lWinScore;
};
//游戏状态 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_StatusFree
{
    public long lCellScore;                         //基础积分 
    public long lWeekBonsesInfo;
    public long lMoneyScore;
    tagSGetBonsesInfo BonsesInfo;
    //历史积分 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTurnScore;            //积分信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lCollectScore;         //积分信息 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
    public string[] szGameRoomName;           //房间名称 
    public int bGameOption;                     //游戏配置 
                                                //锁桌信息 
    public bool bUserLookTable;						//锁桌信息 
};
//游戏状态 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_StatusCall
{
    public ushort wCallBanker;                      //叫庄用户 
    public byte cbDynamicJoin;                      //动态加入  
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] cbPlayStatus;          //用户状态 
    public long lWeekBonsesInfo;
    public long lMoneyScore;
    public tagSGetBonsesInfo BonsesInfo;
    //历史积分 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTurnScore;            //积分信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lCollectScore;         //积分信息 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
    public string[] szGameRoomName;           //房间名称 
    public int bGameOption;                     //游戏配置 
    public bool bUserLookTable;						//锁桌信息 
};
//游戏状态 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_StatusScore
{
    //下注信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] cbPlayStatus;          //用户状态 
    public byte cbDynamicJoin;                      //动态加入 
    public long lTurnMaxScore;                      //最大下注 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTableScore;           //下注数目 
    public ushort wBankerUser;                      //庄家用户 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
    public string[] szGameRoomName;           //房间名称 
    public long lWeekBonsesInfo;
    public long lMoneyScore;
    public tagSGetBonsesInfo BonsesInfo;
    //历史积分 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTurnScore;            //积分信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] CollectScore;         //积分信息 
    public int bGameOption;                     //游戏配置 
    public bool bUserLookTable;						//锁桌信息 
};
//游戏状态 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_StatusPlay
{
    //状态信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] cbPlayStatus;          //用户状态 
    public byte cbDynamicJoin;                      //动态加入 
    public long lTurnMaxScore;                      //最大下注 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTableScore;           //下注数目 
    public ushort wBankerUser;                      //庄家用户 
    public long lWeekBonsesInfo;
    public long lMoneyScore;
    public tagSGetBonsesInfo BonsesInfo;
    //扑克信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] cbHandCardData;//桌面扑克 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] bOxCard;               //牛牛数据 
                                         //历史积分 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lTurnScore;            //积分信息 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lCollectScore;         //积分信息 
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)PROTOCOL_PACKET.SERVER_LEN)]
    public string[] szGameRoomName;           //房间名称 
    public int bGameOption;                     //游戏配置 
    public bool bUserLookTable;						//锁桌信息
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] cbOxInfo;
};
//用户叫庄 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_CallBanker
{
    public ushort wCallBanker;                      //叫庄用户 
    public bool bFirstTimes;						//首次叫庄 
};
//游戏开始 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_GameStart
{
    //下注信息 
    public long lTurnMaxScore;                      //最大下注 
    public ushort wBankerUser;						//庄家用户 
};
//用户下注 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_AddScore
{
    public ushort wAddScoreUser;                        //加注用户 
    public long lAddScoreCount;						//加注数目 
};
//游戏结束 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_GameEnd
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lGameTax;              //游戏税收 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public long[] lGameScore;            //游戏得分 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[] cbCardData;            //用户扑克 
    public long lUserGold;                          //用户累计 
    public long lMoneyScore;                        //彩金数目 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public ushort[] wLookTable;			//无法加入 
};
//发牌数据包 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_SendCard
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst =20)]
    public byte[] cbCardData; //用户扑克 
    public ushort wPlayerCount;
};
//发牌数据包 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_AllCard
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public bool[] bAICount;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)PROTOCOL_PACKET.GAME_PLAYER)]
    public byte[][] cbCardData;	//用户扑克 
};
//用户退出 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_PlayerExit
{
    public ushort wPlayerID;							//退出用户 
};
//用户摊牌 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_Open_Card
{
    public ushort wPlayerID;                            //摊牌用户 
    public byte bOpen;								//摊牌标志 
};
//更新库存 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_UpdateStorage
{
    public long lStorage;                           //新库存值 
    public long lStorageDeduct;						//库存衰减 
};
//////////////////////////////////客户端发送信息//////////////////////////////////////// 
enum PROTOCOL_CLIENT
{
    [Description("用户叫庄 ")]
    SUB_C_CALL_BANKER = 1,
    [Description("用户加注 ")]
    SUB_C_ADD_SCORE = 2,
    [Description("用户摊牌 ")]
    SUB_C_OPEN_CARD = 3,
    [Description("特殊终端 ")]
    SUB_C_SPECIAL_CLIENT_REPORT = 5,
    [Description("更新库存 ")]
    SUB_C_AMDIN_COMMAND = 6,

    SUB_C_LEAVE = 7,
    [Description("输赢控制 ")]
    SUB_C_WIN_LOST = 8,
    [Description("锁定桌子 ")]
    SUB_C_LOOKTABLE = 9,
};

//用户锁桌 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_LookTable
{
    public bool bIsLookTable;						//锁定桌子 
};
//用户叫庄 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_CallBanker
{
    public byte bBanker;							//做庄标志 
};
//终端类型 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_SPECIAL_CLIENT_REPORT
{
    public ushort wUserChairID;                       //用户方位 
};
//用户加注 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_AddScore
{
    public long lScore;								//加注数目 
};
//用户摊牌 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_OxCard
{
    public byte bOX;								//牛牛标志 
};
//更新库存 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_UpdateStorage
{
    public byte cbReqType;                      //请求类型 
    public long lStorage;                       //新库存值 bAICount
    public long lStorageDeduct;					//库存衰减 
};
//输赢控制 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_WinLost
{
    public int nGameID;
    public int nCount;
    public bool bWinlost;
};

//RQ_REFRESH_STORAGE		1 
// RQ_SET_STORAGE			2 
//////////////////////////////////////////////////////////////////////////
enum CB_REQ_TYPE
{
    RQ_SET_WIN_AREA = 1,
    RQ_RESET_CONTROL = 2,
    RQ_PRINT_SYN = 3,
};

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_AdminReq
{
    public byte cbReqType;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] cbExtendData;			//附加数据 
};
//请求回复 
enum CB_ACK_TYPE
{
    ACK_SET_WIN_AREA = 1,
    ACK_PRINT_SYN = 2,
    ACK_RESET_CONTROL = 3,
};

enum CB_RESULT
{
    CR_ACCEPT = 2,//接受
    CR_REFUSAL = 3,//拒绝
};



[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_CommandResult
{
    public byte cbAckType;                  //回复类型 
    public byte cbResult;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] cbExtendData;			//附加数据 
};
//IDM_ADMIN_COMMDN WM_USER+1000 
//控制区域信息 
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct tagControlInfo
{
    public int nAreaWin;		//控制区域 
};
//服务器控制返回 
//回复类型
enum CB_RETURNS_TYPE
{
    S_CR_FAILURE = 0,		//失败 
    S_CR_UPDATE_SUCCES = 1,		//更新成功 
    S_CR_SET_SUCCESS = 2,		//设置成功 
    S_CR_CANCEL_SUCCESS = 3,		//取消成功 
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_S_ControlReturns
{
    public byte cbReturnsType;              //回复类型 
    public byte cbControlArea;  //控制区域 
    public byte cbControlTimes;			//控制次数 
};
//客户端控制申请
enum CB_CONTROL_APP_TYPE
{
    C_CA_UPDATE = 1,		//更新 
    C_CA_SET = 2,//设置 
    C_CA_CANCELS = 3,	//取消 
};
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
struct CMD_C_ControlApplication
{
    public byte cbControlAppType;           //申请类型 
    public byte cbControlArea;  //控制区域 
    public byte cbControlTimes;			//控制次数 
};
