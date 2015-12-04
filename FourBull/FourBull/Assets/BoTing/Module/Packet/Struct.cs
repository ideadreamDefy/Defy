using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using BoTing.Framework;

namespace BoTing.Module
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagGameType
    {
        public ushort wJoinID;							//挂接索引
        public ushort wSortID;							//排序索引
        public ushort wTypeID;							//类型索引
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szTypeName;				        //种类名字
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagOnLineCountInfo
    {
        public uint dwOnLineAllCount;					//在线人数
        public uint dwOnLineRealPlayerCount;			//在线真人玩家人数(无须发送给普通玩家)		
    }
    //游戏种类
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //游戏类型
    public class tagGameKind
    {
        public ushort wTypeID;							//类型索引
        public ushort wJoinID;							//挂接索引
        public ushort wSortID;							//排序索引
        public ushort wKindID;							//类型索引
        public ushort wGameID;							//模块索引
        public uint dwOnLineCount;					    //在线人数
        public uint dwFullCount;						//满员人数
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szKindName;				//游戏名字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szProcessName;			//进程名字
    };

    //游戏节点
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagGameNode
    {
        public ushort wKindID;							//名称索引
        public ushort wJoinID;							//挂接索引
        public ushort wSortID;							//排序索引
        public ushort wNodeID;							//节点索引
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szNodeName;				        //节点名称
    };

    //定制类型
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal class tagGamePage
    {
        public ushort wPageID;							//页面索引
        public ushort wKindID;							//名称索引
        public ushort wNodeID;							//节点索引
        public ushort wSortID;							//排序索引
        public ushort wOperateType;						//控制类型
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szDisplayName;			//显示名称
    };

    //游戏房间
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagGameServer
    {
        public ushort wKindID;							//名称索引
        public ushort wNodeID;							//节点索引
        public ushort wSortID;							//排序索引
        public ushort wServerID;						//房间索引
        public ushort wServerPort;						//房间端口
        public uint dwOnLineCount;					    //在线人数
        public uint dwFullCount;						//满员人数
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szServerAddr;					    //房间地址
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szServerName;			            //房间名称
        public long lMinEnterScore;					//最低进入
    };

    //视频配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagAVServerOption
    {
        public ushort wAVServerPort;					//视频端口
        public uint dwAVServerAddr;						//视频地址
    };

    //在线信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagOnLineInfoKind
    {
        public ushort wKindID;							//类型标识
        public uint dwOnLineCount;						//在线人数
    };

    //在线信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagOnLineInfoServer
    {
        public ushort wServerID;						//房间标识
        public uint dwOnLineCount;						//在线人数
    };

    //列表子项
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagColumnItem
    {
        public byte cbColumnWidth;						//列表宽度
        public byte cbDataDescribe;						//字段类型
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szColumnName;					    //列表名字
    };

    //桌子状态
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagTableStatus
    {
        public byte cbTableLock;						//锁定标志
        public byte cbPlayStatus;						//游戏标志
        public int lCellScore;							//底分信息
    };

    //用户状态
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagUserStatus
    {
        public ushort wTableID;							//桌子索引
        public ushort wChairID;							//椅子位置
        public byte cbUserStatus;						//用户状态
    };


    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagUserAttrib
    {
        public byte cbCompanion;						//用户关系
    };

    //用户积分
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagUserScore
    {
        //积分信息
        public long lScore;								//用户分数
        public long lGrade;								//用户成绩
        public long lInsure;							//用户银行

        //输赢信息
        public uint dwWinCount;							//胜利盘数
        public uint dwLostCount;						//失败盘数
        public uint dwDrawCount;						//和局盘数
        public uint dwFleeCount;						//逃跑盘数

        //全局信息
        public uint dwUserMedal;						//用户奖牌
        public uint dwExperience;						//用户经验
        public int lLoveLiness;						    //用户魅力
    };

    //用户积分
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagMobileUserScore
    {
        //积分信息
        public long lScore;								//用户分数

        //输赢信息
        public uint dwWinCount;							//胜利盘数
        public uint dwLostCount;						//失败盘数
        public uint dwDrawCount;						//和局盘数
        public uint dwFleeCount;						//逃跑盘数

        //全局信息
        public uint dwExperience;						//用户经验
    };

    //道具使用
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagUsePropertyInfo
    {
        public ushort wPropertyCount;                     //道具数目
        public ushort dwValidNum;						    //有效数字
        public uint dwEffectTime;                       //生效时间
    };


    //用户道具
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagUserProperty
    {
        public uint wPropertyUseMark;                   //道具标示
        tagUsePropertyInfo[] PropertyInfo;			//使用信息   
    };

    //道具包裹
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagPropertyPackage
    {
        public ushort wTrumpetCount;                     //小喇叭数
        public ushort wTyphonCount;                      //大喇叭数
    };

    //时间信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class tagTimeInfo
    {
        public uint dwEnterTableTimer;						//进出桌子时间
        public uint dwLeaveTableTimer;						//离开桌子时间
        public uint dwStartGameTimer;						//开始游戏时间
        public uint dwEndGameTimer;							//离开游戏时间
    };


    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal class tagDataDescribe
    {
        public ushort wDataSize;						//数据大小
        public ushort wDataDescribe;					//数据描述
    };
    //用户信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    public class tagUserInfo
    {
        public const int DTP_GR_TABLE_PASSWORD = 1;									//桌子密码
        //用户属性
        public const int DTP_GR_NICK_NAME = 10;									//用户昵称
        public const int DTP_GR_GROUP_NAME = 11;							//社团名字
        public const int DTP_GR_UNDER_WRITE = 12;								//个性签名

        //附加信息
        public const int DTP_GR_USER_NOTE = 20;								//用户备注
        public const int DTP_GR_CUSTOM_FACE = 21;								//自定头像

        public const int DTP_GR_ANDROID = 30;                                 //是否为机器人
        //基本属性
        public uint dwUserID;							//用户 I D
        public uint dwGameID;							//游戏 I D
        public uint dwGroupID;							//社团 I D
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			            //用户昵称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_GROUP_NAME)]
        public string szGroupName;		                //社团名字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_UNDER_WRITE)]
        public string szUnderWrite;		                //个性签名

        //头像信息
        public ushort wFaceID;							//头像索引
        public uint dwCustomID;							//自定标识

        //用户资料
        public byte cbGender;							//用户性别
        public byte cbMemberOrder;						//会员等级
        public byte cbMasterOrder;						//管理等级

        //用户状态
        public ushort wTableID;							//桌子索引
        public ushort wChairID;							//椅子索引
        public byte cbUserStatus;						//用户状态

        //积分信息
        public long lScore;								//用户分数
        public long lGrade;								//用户成绩
        public long lInsure;							//用户银行

        //游戏信息
        public uint dwWinCount;							//胜利盘数
        public uint dwLostCount;						//失败盘数
        public uint dwDrawCount;						//和局盘数
        public uint dwFleeCount;						//逃跑盘数
        public uint dwUserMedal;						//用户奖牌
        public uint dwExperience;						//用户经验
        public int lLoveLiness;						    //用户魅力

        //时间信息
        tagTimeInfo TimerInfo;							//时间信息

        //比赛信息
        public byte cbEnlistStatus;						//报名状态

        //扩展标识
        public int lExpand;
        public uint dwExpand;
        public byte[] Buffer
        {
            set
            {
                tagUserInfoHead UserInfoHead = (tagUserInfoHead)PacketHelper.BytesToStruct(value, typeof(tagUserInfoHead));
                //用户属性
                wFaceID = UserInfoHead.wFaceID;
                dwGameID = UserInfoHead.dwGameID;
                dwUserID = UserInfoHead.dwUserID;
                dwGroupID = UserInfoHead.dwGroupID;
                dwCustomID = UserInfoHead.dwCustomID;

                //用户状态
                wTableID = UserInfoHead.wTableID;
                wChairID = UserInfoHead.wChairID;
                cbUserStatus = UserInfoHead.cbUserStatus;

                //用户属性
                cbGender = UserInfoHead.cbGender;
                cbMemberOrder = UserInfoHead.cbMemberOrder;
                cbMasterOrder = UserInfoHead.cbMasterOrder;

                //用户积分
                lScore = UserInfoHead.lScore;
                lGrade = UserInfoHead.lGrade;
                lInsure = UserInfoHead.lInsure;
                dwWinCount = UserInfoHead.dwWinCount;
                dwLostCount = UserInfoHead.dwLostCount;
                dwDrawCount = UserInfoHead.dwDrawCount;
                dwFleeCount = UserInfoHead.dwFleeCount;
                dwUserMedal = UserInfoHead.dwUserMedal;
                dwExperience = UserInfoHead.dwExperience;
                lLoveLiness = UserInfoHead.lLoveLiness;

                long nHeadSize = Marshal.SizeOf(typeof(tagUserInfoHead));
                if (nHeadSize >= value.Length)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(nHeadSize, SeekOrigin.Begin);
                //没有读到最后
                while (stream.Position != stream.Length)
                {
                    byte[] buffer = new byte[Marshal.SizeOf(typeof(tagDataDescribe))];
                    stream.Read(buffer, 0, buffer.Length);
                    tagDataDescribe DataDescribe = (tagDataDescribe)PacketHelper.BytesToStruct(buffer, typeof(tagDataDescribe));
                    switch (DataDescribe.wDataDescribe)
                    {
                        case DTP_GR_NICK_NAME:
                            {
                                if (DataDescribe.wDataSize == 0)
                                    break;
                                buffer = new byte[DataDescribe.wDataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                szNickName = Encoding.Unicode.GetString(buffer);
                            }
                            break;
                        case DTP_GR_GROUP_NAME:
                            {
                                if (DataDescribe.wDataSize == 0)
                                    break;
                                buffer = new byte[DataDescribe.wDataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                szGroupName = Encoding.Unicode.GetString(buffer);
                            }
                            break;
                        case DTP_GR_UNDER_WRITE:
                            {
                                if (DataDescribe.wDataSize == 0)
                                    break;
                                buffer = new byte[DataDescribe.wDataSize];
                                stream.Read(buffer, 0, buffer.Length);
                                szUnderWrite = Encoding.Unicode.GetString(buffer);
                            }
                            break;
                    }
                }
            }
        }
    };

    //用户信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
   
    internal class tagUserInfoHead
    {
        //用户属性
        public uint dwGameID;							//游戏 I D
        public uint dwUserID;							//用户 I D
        public uint dwGroupID;							//社团 I D

        //头像信息
        public ushort wFaceID;							//头像索引
        public uint dwCustomID;							//自定标识

        //用户属性
        public byte cbGender;							//用户性别
        public byte cbMemberOrder;						//会员等级
        public byte cbMasterOrder;						//管理等级

        //用户状态
        public ushort wTableID;							//桌子索引
        public ushort wChairID;							//椅子索引
        public byte cbUserStatus;						//用户状态

        //积分信息
        public long lScore;								//用户分数
        public long lGrade;								//用户成绩
        public long lInsure;							//用户银行

        //游戏信息
        public uint dwWinCount;							//胜利盘数
        public uint dwLostCount;						//失败盘数
        public uint dwDrawCount;						//和局盘数
        public uint dwFleeCount;						//逃跑盘数
        public uint dwUserMedal;						//用户奖牌
        public uint dwExperience;						//用户经验
        public int lLoveLiness;						    //用户魅力
        
    };

    //头像信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal class tagCustomFaceInfo
    {
        public uint dwDataSize;							//数据大小
        //public uint							dwCustomFace[FACE_CX*FACE_CY];		//图片信息
    };

    //用户信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal class tagUserRemoteInfo
    {
        //用户信息
        public uint dwUserID;							//用户标识
        public uint dwGameID;							//游戏标识
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//用户昵称

        //等级信息
        public byte cbGender;							//用户性别
        public byte cbMemberOrder;						//会员等级
        public byte cbMasterOrder;						//管理等级

        //位置信息
        public ushort wKindID;							//类型标识
        public ushort wServerID;							//房间标识
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_SERVER)]
        public string szGameServer;			//房间位置
    };
}
