using BoTing.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace BoTing.Module
{
    #region 定义各种枚举
    internal enum MAIN_CMD
    {
        MDM_GP_LOGON = 1,                                           //广场登录
        MDM_GP_SERVER_LIST = 2,									    //列表信息
        MDM_GP_USER_SERVICE = 3,									//用户服务
        MDM_GP_REMOTE_SERVICE = 4,									//远程服务

        MDM_GR_LOGON = 1,									        //游戏登录信息
        MDM_GR_CONFIG = 2,                                          //配置信息
        MDM_GR_USER = 3,									        //用户信息
        MDM_GR_STATUS = 4,									        //状态信息
        MDM_GR_INSURE = 5,                                          //用户信息
        MDM_GF_FRAME = 100,                                         //游戏框架命令
        MDM_GF_GAME = 200,                                          //游戏命令
        MDM_CM_SYSTEM = 1000,								        //系统命令
    }
    public enum GLOBAL_DEFINE
    {
        MAX_KIND = 256,								            //最大类型
        MAX_SERVER = 1024,							            //最大房间
        //人数定义
        MAX_CHAIR = 100,									    //最大椅子
        MAX_TABLE = 512,									    //最大桌子
        MAX_COLUMN = 32,									    //最大列表
        MAX_ANDROID = 256,									    //最大机器
        MAX_PROPERTY = 128,									    //最大道具
        MAX_WHISPER_USER = 16,									//最大私聊
        MAX_CHAIR_GENERAL = 8,									//最大椅子
    }
    public enum STR_LEN
    {
        LEN_ACCOUNTS = 32,
        LEN_NICKNAME = 32,							            //昵称长度
        LEN_GROUP_NAME = 32,
        LEN_MACHINE_ID = 33,
        LEN_SERVER = 32,
        LEN_MD5 = 33,
        LEN_UNDER_WRITE = 32,									//个性签名
        LEN_PASSWORD = 33,                                      //密码长度

        LEN_QQ = 16,									        //Q Q 号码
        LEN_EMAIL = 33,									        //电子邮件
        LEN_USER_NOTE = 256,									//用户备注
        LEN_SEAT_PHONE = 33,									//固定电话
        LEN_MOBILE_PHONE = 12,									//移动电话
        LEN_PASS_PORT_ID = 19,								    //证件号码
        LEN_COMPELLATION = 16,								    //真实名字
        LEN_DWELLING_PLACE = 128,								//联系地址
        LEN_COUNTRY = 20,								        //国家
        LEN_PROVINCE = 20,							            //省
        LEN_CITY = 20,							                //市
        LEN_POST_CODE = 10,                                     //邮编
        LEN_DEGREE = 14,                                        //学位
        LEN_MSN = 33,							                //MSN
        LEN_PHP = 33,							                //个人主页

        LEN_USER_CHAT = 128,								    //聊天长度
        TIME_USER_CHAT = 1,									    //聊天间隔
        TRUMPET_MAX_CHAR = 128,									//喇叭长度
        TIME_USER_CALL = 1,									    //呼叫间隔
    }

    #endregion
    #region 大厅登录相关协议

    internal enum LOGON_SUB_CMD
    {
        SUB_GP_LOGON_GAMEID = 1,									//I D 登录
        SUB_GP_LOGON_ACCOUNTS = 2,									//帐号登录
        SUB_GP_REGISTER_ACCOUNTS = 3,								//注册帐号
        SUB_GP_CHECK_ACCOUNTS = 4,									//检测账号

        //登录结果
        SUB_GP_LOGON_SUCCESS = 100,									//登录成功
        SUB_GP_LOGON_FAILURE = 101,									//登录失败
        SUB_GP_LOGON_FINISH = 102,									//登录完成
        SUB_GP_VALIDATE_MBCARD = 103,                               //登录失败
        SUB_GP_CHECK_SUCCESSS = 104,                                //验证成功
        SUB_GP_CHECK_FAILURE = 105,                                 //验证成功
        SUB_GP_VALIDATE_PASSPORT_ID = 106,							//登录失败
        SUB_GP_SENDMOBILEPASS = 107,								//手机接口


        //游戏登录模式
        SUB_GR_LOGON_USERID = 1,							    //I D 登录
        SUB_GR_LOGON_MOBILE = 2,							    //手机登录
        SUB_GR_LOGON_ACCOUNTS = 3,								//帐户登录

        //登录结果
        SUB_GR_LOGON_SUCCESS = 100,							    //登录成功
        SUB_GR_LOGON_FAILURE = 101,							    //登录失败
        SUB_GR_LOGON_FINISH = 102,								//登录完成
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //帐号登录
    internal struct CMD_GP_LogonAccounts
    {
        public uint dwPlazaVersion;						    //广场版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                    //机器序列
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szPassword;				            //登录密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szAccounts;			                //登录帐号
        public byte cbValidateFlags;			            //校验标识
        public byte cbPassPortID;                           //
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASS_PORT_ID)]
        public string szPassPortID;                         //
    };

    //I D 登录
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_LogonGameID
    {
        public uint dwPlazaVersion;						    //广场版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                    //机器序列
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public uint dwGameID;	                            //游戏 I D
        public string szPassword;				            //登录密码
        public byte cbValidateFlags;			            //校验标识
        public byte cbPassPortID;                           //
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASS_PORT_ID)]
        public string szPassPortID;                         //
    };

    //注册帐号
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_RegisterAccounts
    {
        //系统信息
        public uint dwPlazaVersion;						        //广场版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                        //机器序列

        //注册信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szAccounts;		                        //登录帐号
        public byte cbGender;							         //用户性别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szLogonPass;			                    //登录密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_COMPELLATION)]
        public string szCompellation;	                        //真实名字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASS_PORT_ID)]
        public string szPassPortID;	                            //证件号码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MOBILE_PHONE)]
        public string szMobilePhone;	                        //移动电话
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_QQ)]
        public string szQQ;	                                    //QQ
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_UNDER_WRITE)]
        public string szUnderWrite;	                            //个性签名

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_COUNTRY)]
        public string szCountry;	                            //国家
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PROVINCE)]
        public string szProvince;	                            //省
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_CITY)]
        public string szCity;	                                //市
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_POST_CODE)]
        public string szPostCode;	                            //邮编
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_DWELLING_PLACE)]
        public string szAddress;	                            //地址
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_DEGREE)]
        public string szDegree;	                                //学历
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_EMAIL)]
        public string szEmail;	                                 //邮箱
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PHP)]
        public string szPHP;                                    //个人主页
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_SEAT_PHONE)]
        public string szSeatPhone;	                            //固定电话
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MSN)]
        public string szMSN;	                                //MSN
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szInsurePass;				                //银行密码

        public byte cbValidateFlags;			                //校验标识
    };

    //检测账号可用
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_CheckAccounts
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szAccounts;			                //登录帐号
    };
    //登录成功
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_LogonSuccess
    {
        //属性资料
        public ushort wFaceID;							    //头像标识
        public uint dwUserID;							    //用户 I D
        public uint dwGameID;							    //游戏 I D

        public uint dwGroupID;							    //社团标识
        public uint dwCustomID;							    //自定标识
        public uint dwUserMedal;						    //用户奖牌
        public uint dwExperience;						    //经验数值
        public uint dwLoveLiness;						    //用户魅力
        public uint dwSpreaderID;						    //推荐人ID
        public byte cbWeiChatSet;						    //微信设置
        //用户成绩
        public long lUserScore;						    //用户金币
        public long lUserInsure;						    //用户银行

        //用户信息
        public byte cbGender;							    //用户性别
        public byte cbMoorMachine;						    //锁定机器

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szAccounts;			                //登录帐号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szNickName;			                //用户昵称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_GROUP_NAME)]
        public string szGroupName;		                    //社团名字

        public byte cbMoorWeiChat;						    //微信绑定
        public ushort wInSpreaderID;					    //是否绑定
        //配置信息
        public byte cbShowServerStatus;                     //显示服务器状态
    };

    //登录失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_LogonFailure
    {
        public Int32 lResultCode;						    //错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				        //描述消息
    };

    //登陆完成
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_LogonFinish
    {
        public ushort wIntermitTime;						//中断时间
        public ushort wOnLineCountTime;					    //更新时间
    };
    //验证成功
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_CheckSuccess
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				        //描述消息
    };
    //验证失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_CheckFailure
    {
        public Int32 lResultCode;						    //错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				        //描述消息
    };
    #endregion
    #region 大厅相关协议
    internal enum SERVER_LIST_SUB_CMD
    {
        //获取命令
        SUB_GP_GET_LIST = 1,									//获取列表
        SUB_GP_GET_SERVER = 2,								    //获取房间
        SUB_GP_GET_ONLINE = 3,									//获取在线
        SUB_GP_GET_COLLECTION = 4,								//获取收藏

        //列表信息
        SUB_GP_LIST_TYPE = 100,							        //类型列表
        SUB_GP_LIST_KIND = 101,							        //种类列表
        SUB_GP_LIST_NODE = 102,							        //节点列表
        SUB_GP_LIST_PAGE = 103,							        //定制列表
        SUB_GP_LIST_SERVER = 104,							    //房间列表
        SUB_GP_VIDEO_OPTION = 105,							    //视频配置

        //完成信息
        SUB_GP_LIST_FINISH = 200,							    //发送完成
        SUB_GP_SERVER_FINISH = 201,							    //房间完成

        //在线信息
        SUB_GR_KINE_ONLINE = 300,							    //类型在线
        SUB_GR_SERVER_ONLINE = 301,							    //房间在线

        //彩金信息
        SUB_GP_GETBONSESSCORE = 400,							//获取信息
        SUB_GP_GETRETURNSCORE = 401,							//获取返回
        SUB_GP_GETRETURNSCORE_FINSH = 402,						//完成获取
    }
    //游戏种类
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GP_GameType
    {
        public List<tagGameType> ItemArray = new List<tagGameType>();
        public byte[] Buffer
        {
            set
            {
                int nItemSize = Marshal.SizeOf(typeof(tagGameType));
                int nItemCount = value.Length / nItemSize;
                if (nItemCount == 0)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagGameType Item = (tagGameType)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagGameType));
                    ItemArray.Add(Item);
                }
            }
        }
    }
    //游戏类型
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GP_GameKind
    {
        public List<tagGameKind> ItemArray = new List<tagGameKind>();
        public byte[] Buffer
        {
            set
            {
                int nItemSize = Marshal.SizeOf(typeof(tagGameKind));
                int nItemCount = value.Length / nItemSize;
                if (nItemCount == 0)
                    return;

                MemoryStream stream = new MemoryStream(value);
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagGameKind Item = (tagGameKind)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagGameKind));
                    ItemArray.Add(Item);
                }
            }
        }
    }
    //游戏节点
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GP_GameNode
    {
        public List<tagGameNode> ItemArray = new List<tagGameNode>();
        public byte[] Buffer
        {
            set
            {
                int nItemSize = Marshal.SizeOf(typeof(tagGameNode));
                int nItemCount = value.Length / nItemSize;
                if (nItemCount == 0)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagGameNode Item = (tagGameNode)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagGameNode));
                    ItemArray.Add(Item);
                }
            }
        }
    }
    //定制类型
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GP_GamePage
    {
        public List<tagGamePage> ItemArray = new List<tagGamePage>();
        public byte[] Buffer
        {
            set
            {
                int nItemSize = Marshal.SizeOf(typeof(tagGamePage));
                int nItemCount = value.Length / nItemSize;
                if (nItemCount == 0)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagGamePage Item = (tagGamePage)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagGamePage));
                    ItemArray.Add(Item);
                }
            }
        }
    }
    //游戏房间
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GP_GameServer
    {
        public List<tagGameServer> ItemArray = new List<tagGameServer>();
        public byte[] Buffer
        {
            set
            {
                int nItemSize = Marshal.SizeOf(typeof(tagGameServer));
                int nItemCount = value.Length / nItemSize;
                if (nItemCount == 0)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagGameServer Item = (tagGameServer)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagGameServer));
                    ItemArray.Add(Item);
                }
            }
        }
    }
    //大厅彩金
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_AllGameScore
    {
        public long lAllGameScore;						//全部彩金
    };

    //获取返回
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_GetReturnScore
    {
        public uint dwServerID;							//房间 I D
        public long lNowScore;							//当前彩金
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szServerName;					    //房间名称
    };

    //获取信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_GetUserInfo
    {
        public uint dwUserID;							//用户 I D
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szPassWord;				//游戏密码
    };

    //获取在线
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_GetOnline
    {
        public ushort wServerCount;				//房间数目
        public ushort[] wOnLineServerID;		//房间标识
        public byte[] Buffer                    //获取缓冲内容
        {
            get
            {
                int nCount = sizeof(ushort) * (wServerCount + 1);
                byte[] buffer = new byte[nCount];
                buffer[0] = (byte)(wServerCount & 0x0f);
                buffer[1] = (byte)(wServerCount >> 8);
                for (int i = 0; i < wOnLineServerID.Length; i++)
                {
                    int nIndex = (i + 1) * 2;
                    buffer[nIndex] = (byte)(wOnLineServerID[i] & 0x0f);
                    buffer[nIndex + 1] = (byte)(wOnLineServerID[i] >> 8);
                }
                return buffer;
            }
        }
    };

    //类型在线
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_KindOnline
    {
        public ushort wKindCount;							            //类型数目
        public tagOnLineInfoKind[] OnLineInfoKind;			            //类型在线
    };

    //房间在线
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ServerOnline
    {
        public ushort wServerCount;						                //房间数目
        public tagOnLineInfoServer[] OnLineInfoServer;		            //房间在线
    };

    #endregion
    #region 游戏登录相关协议

    //帐号登录
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_LogonAccounts
    {
        //版本信息
        public uint dwPlazaVersion;						//广场版本
        public uint dwFrameVersion;						//框架版本
        public uint dwProcessVersion;					//进程版本
        //登录信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szPassword;					    //登录密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_ACCOUNTS)]
        public string szAccounts;				        //登录帐号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;			            //机器序列
    };

    //I D 登录
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_LogonUserID
    {
        //版本信息
        public uint dwPlazaVersion;						//广场版本
        public uint dwFrameVersion;						//框架版本
        public uint dwProcessVersion;					//进程版本

        //登录信息
        public uint dwUserID;							//用户 I D
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szPassword;					    //登录密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;			            //机器序列
        public ushort wKindID;							    //类型索引
    };


    //登录成功
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_LogonSuccess
    {
        public uint dwUserRight;						//用户权限
        public uint dwMasterRight;						//管理权限
    };

    //登录失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_LogonFailure
    {
        public int lErrorCode;						    //错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				    //描述消息
    };
    #endregion
    #region 列表配置
    internal enum CONFIG_SUB_CMD
    {
        SUB_GR_CONFIG_COLUMN = 100,								//列表配置
        SUB_GR_CONFIG_SERVER = 101,								//房间配置
        SUB_GR_CONFIG_PROPERTY = 102,							//道具配置
        SUB_GR_CONFIG_FINISH = 103,								//配置完成
        SUB_GR_CONFIG_USER_RIGHT = 104,							//玩家权限
    }
    //列表配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GR_ConfigColumn
    {
        public byte cbColumnCount;						                        //列表数目
        public List<tagColumnItem> ItemArray = new List<tagColumnItem>();			    //列表描述
        public byte[] Buffer
        {
            set
            {
                cbColumnCount = value[0];
                if (cbColumnCount <= 0)
                    return;
                int nItemSize = Marshal.SizeOf(typeof(tagColumnItem));
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(1, SeekOrigin.Begin);

                for (int i = 0; i < cbColumnCount; ++i)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagColumnItem Item = (tagColumnItem)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagColumnItem));
                    ItemArray.Add(Item);
                }
            }
        }
    };

    //房间配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_ConfigServer
    {
        //房间属性
        public ushort wTableCount;						//桌子数目
        public ushort wChairCount;						//椅子数目

        //房间配置
        public ushort wServerType;						//房间类型
        public uint dwServerRule;						//房间规则

        //底分设置
        public int bSetScore;							//能否设置
        public int iMaxTimes;							//最大倍数

        //彩金房间
        public int bCaiJinRoom;						    //彩金房间
    };

    //道具配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GR_ConfigProperty
    {
        public byte cbPropertyCount;					//道具数目
        public tagPropertyInfo[] PropertyInfo;			        //道具描述
        public byte[] Buffer
        {
            set
            {
                byte[] buffer = value;
                cbPropertyCount = buffer[0];
                if (cbPropertyCount <= 0)
                    return;
                Array.Copy(buffer, 1, buffer, 0, buffer.Length - 1);
                int len = buffer.Length - 1;
                PropertyInfo = new tagPropertyInfo[cbPropertyCount];
                for (int i = 0; i < cbPropertyCount; ++i)
                {
                    len -= Marshal.SizeOf(typeof(tagPropertyInfo));
                    PropertyInfo[i] = (tagPropertyInfo)PacketHelper.BytesToStruct(buffer, typeof(tagPropertyInfo));
                    Array.Copy(buffer, Marshal.SizeOf(typeof(tagPropertyInfo)), buffer, 0, len);
                }
            }
        }
    };

    //玩家权限
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_ConfigUserRight
    {
        public uint dwUserRight;						//玩家权限
    };
    #endregion
    #region 用户服务
    internal enum USER_SERVICE_SUB
    {
        //账号服务
        SUB_GP_MODIFY_MACHINE = 100,									    //修改机器
        SUB_GP_MODIFY_LOGON_PASS = 101,									    //修改密码
        SUB_GP_MODIFY_INSURE_PASS = 102,									//修改密码
        SUB_GP_MODIFY_UNDER_WRITE = 103,									//修改签名

        //修改头像
        SUB_GP_USER_FACE_INFO = 200,								        //头像信息
        SUB_GP_SYSTEM_FACE_INFO = 201,									    //系统头像
        SUB_GP_CUSTOM_FACE_INFO = 202,									    //自定头像

        //个人资料
        SUB_GP_USER_INDIVIDUAL = 301,								        //个人资料
        SUB_GP_QUERY_INDIVIDUAL = 302,									    //查询信息
        SUB_GP_MODIFY_INDIVIDUAL = 303,							            //修改资料

        //银行服务
        SUB_GP_USER_SAVE_SCORE = 400,							            //存款操作
        SUB_GP_USER_TAKE_SCORE = 401,							            //取款操作
        SUB_GP_USER_TRANSFER_SCORE = 402,								    //转账操作
        SUB_GP_USER_INSURE_INFO = 403,								        //银行资料
        SUB_GP_QUERY_INSURE_INFO = 404,							            //查询银行
        SUB_GP_USER_INSURE_SUCCESS = 405,								    //银行成功
        SUB_GP_USER_INSURE_FAILURE = 406,							        //银行失败
        SUB_GP_QUERY_USER_INFO_REQUEST = 407,							    //查询用户
        SUB_GP_QUERY_USER_INFO_RESULT = 408,						        //用户信息
        SUB_GP_TRANSFER_RECORD = 409,
        SUB_GP_USER_TRANSFER_RECORD_RESULT = 410,                           //用户转账记录
        SUB_GP_USER_TRANSFER_RECORD_FINISH = 411,                           //用户转账记录查询完成
        SUB_GP_USER_INSURE_LOGON = 412,                                     //用户登录
        SUB_GP_USER_INSURE_LOGON_SUCCESS = 413,
        SUB_GP_USER_INSURE_LOGON_FAILURE = 414,
        SUB_GP_QUERY_GAME_INSURE_INFO = 415,                                //用户游戏数据查询
        SUB_GP_USER_GAME_INSURE_INFO = 416,                                 //用户游戏数据查询
        SUB_GP_USER_TRANSFER_SUCCESS = 417,                                 //用户转账记录

        SUB_GR_USER_BIND_MOBILE = 418,						                //用户绑定手机
        SUB_GR_USER_BIND_MOBILE_RESULT = 419,						        //绑定手机返回
        SUB_GR_USER_ADD_FRIEND = 420,						                //添加好友
        SUB_GR_USER_ADD_FRIEND_RESULT = 421,						        //添加好友返回

        SUB_GR_QUERY_REBATE = 422,							                //查询返利
        SUB_GR_QUERY_REBATE_RESULT = 423,							        //查询返利

        SUB_GR_DRAW_REBATE = 430,						                    //领取返利
        SUB_GR_DRAW_REBATE_RESULT = 431,						            //领取返利

        //操作结果
        SUB_GR_DRAWREBATESCORE = 424,									    //操作成功
        SUB_GR_DRAWREBATESCORE_RESULE = 425,								//操作失败

        SUB_GP_OPERATE_SUCCESS = 426,									    //操作成功 
        SUB_GP_OPERATE_FAILURE = 427,									    //操作成功 

        //推荐人消息
        SUB_GR_TUIJIAN_INFO = 428,									        //推荐人信息
        SUB_GR_TUIJIAN_RETURN = 429,								        //推荐人信息

        //查询推荐人
        SUB_GR_FRISTQUERY = 440,								            //查询数据
        SUB_GR_TWOQUERY = 441,									            //查询数据
        SUB_GR_OTHERUSERSCORE = 442,								        //领取数据

        //查询返回
        SUB_GR_RETURNFRISTQUERY = 443,						                //查询数据
        SUB_GR_RETURNTWOQUERY = 444,						                //查询数据
        SUB_GR_RETURNOTHERUSERSCORE = 445,							        //领取数据
        SUB_GP_TAKE_WEICHAT_CODE = 446,						                //微信验证
        SUB_GP_TRANSFER_WEICHAT_CODE = 447,						            //微信验证
        SUB_GP_USER_WEICHAT_SET = 448,						                //微信设置
        SUB_GP_USER_WEICHAT_SET_RETURN = 449,						        //设置返回
    }
    //微信设置 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_UserWeiChatSet
    {
        public uint dwUserID;								//用户 I D 
        public byte cbWeiChatSet;							//用户设置 
    };
    //添加返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GetSScoreResult
    {
        public long lScore;									//剩余金币 
        public byte bAddOkorError;						    //添加状态 
    };
    //查询返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_QueryUserGxScore
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//玩家名字 
        public long lGxScore;							//当前贡献 
    };
    //查询返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_QueryReturnScore
    {
        public long lAllScore;							//总的金币 
        public long lNowScore;							//当前可领 
    };
    //初始查询 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_TuiJianRenScore
    {
        public uint dwGameID;								//自己 I D 
    };
    //添加返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_TuiJianInfoResult
    {
        public bool bAddOkorError;						   //添加状态 
    };
    //推荐 I D 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_tgaTuiJianInfo
    {
        public uint dwUserID;								//用户 I D 
        public uint dwGameID;								//推荐人ID 
    };
    //领取返利 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_DrawRebateScore
    {
        public uint dwUserID;									//用户I D 
        public int DrawScoreNum;								//领取数目 
    };
    //领取返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_DrawRebateScoreResule
    {
        public bool bDrawOk;									//领取成功 
        public long lRebateScore;								//剩余数目 
    };
    //查询返利 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_QueryRebateScore
    {
        public uint dwUserID;						//用户I D 
    };
    //返利返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_QueryRebateScoreResult
    {
        public long lRebateScore;					//返利返回 
    };
    //添加返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_AddFriendInfoResult
    {
        public uint dwUserID;						//用户 I D 
        public uint dwGameID;						//游戏 I D 
        public byte cbGender;						//用户性别 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;		//用户昵称 
        public bool bQueryIsOK;						//查询成功 
    };
    //添加好友 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_AddFriendInfo
    {
        public byte cbAddMode;							//选择类型 
        public uint dwGameID;							//添加 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFriendNam;					//添加名字				 
    };
    //绑定手机 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_BindMobileNum
    {
        public uint dwGameID;							//用户I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		//机器序列 
    };
    //绑定返回 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_BindMobileResule
    {
        public int nResultCode;								//绑定信息  
    };
    //修改密码 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ModifyLogonPass
    {
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szDesPassword;		//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szScrPassword;		//用户密码 
    };
    //修改密码 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ModifyInsurePass
    {
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szDesPassword;		//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szScrPassword;		//用户密码 
    };
    //修改签名 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ModifyUnderWrite
    {
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_UNDER_WRITE)]
        public string szUnderWrite;		//个性签名 
    };
    ////////////////////////////////////////////////////////////////////////////////// 
    //用户头像 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserFaceInfo
    {
        public ushort wFaceID;							//头像标识 
        public uint dwCustomID;							//自定标识 
    };
    //修改头像 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_SystemFaceInfo
    {
        public ushort wFaceID;							//头像标识 
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		//机器序列 
    };
    //修改头像 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_CustomFaceInfo
    {
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		//机器序列 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = /*FACE_CX * FACE_CY*/10)]
        public uint dwCustomFace;		//图片信息 
    };
    ////////////////////////////////////////////////////////////////////////////////// 
    //绑定机器 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ModifyMachine
    {
        public byte cbBind;								//绑定标志 
        public uint dwUserID;							//用户标识 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		//机器序列 
    };
    ////////////////////////////////////////////////////////////////////////////////// 
    //个人资料 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserIndividual
    {
        public uint dwUserID;							//用户 I D 
    };
    //查询信息 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_QueryIndividual
    {
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
    };
    //修改资料 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_ModifyIndividual
    {
        public byte cbGender;							//用户性别 
        public uint dwUserID;							//用户 I D 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			//用户密码 
    };

    //银行资料
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserInsureInfo
    {
        public ushort wRevenueTake;						//税收比例
        public ushort wRevenueTransfer;					//税收比例
        public ushort wServerID;						//房间标识
        public long lUserScore;							//用户金币
        public long lUserInsure;						//银行金币
        public long lTransferPrerequisite;				//转账条件
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //游戏银行数据查询
    internal struct CMD_GP_QueryGameInsureInfo
    {
        public uint dwUserID;							        //用户 I D
        public ushort wKindID;                                  //游戏KINDID
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserGameInsureInfo
    {
        public long lUserScore;							//用户金币
        public long lUserInsure;						//银行金币
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //登录银行
    internal struct CMD_GP_UserInsureLogon
    {
        public uint dwUserID;							//用户 I D
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;		    //用户密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		//机器序列
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //登录银行
    internal struct CMD_GP_UserInsureLogonFailure
    {
        public int lResultCode;						//错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				//描述消息
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //存入金币
    internal struct CMD_GP_UserSaveScore
    {
        public uint dwUserID;							//用户 I D
        public ushort wKindID;                          //游戏KINDID
        public long lSaveScore;							//存入金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                //机器序列
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //提取金币
    internal struct CMD_GP_UserTakeScore
    {
        public uint dwUserID;							//用户 I D
        public ushort wKindID;                            //游戏KINDID
        public long lTakeScore;							//提取金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                //机器序列
        public byte cbStaty;							//首次使用
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string szCodeID;						    //微信验证	
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //转账金币
    internal struct CMD_GP_UserTransferScore
    {
        public uint dwUserID;							//用户 I D
        public byte cbByNickName;                       //昵称赠送
        public long lTransferScore;						//转账金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MD5)]
        public string szPassword;				        //银行密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			            //目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_MACHINE_ID)]
        public string szMachineID;		                //机器序列
        public byte cbStaty;							//首次使用
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        public string szCodeID;						    //微信验证
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //银行成功
    internal struct CMD_GP_UserInsureSuccess
    {
        public uint dwUserID;							//用户 I D
        public long lUserScore;							//用户金币
        public long lUserInsure;						//银行金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				//描述消息
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //银行失败
    internal struct CMD_GP_UserInsureFailure
    {
        public int lResultCode;						//错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				//描述消息
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //提取结果
    internal struct CMD_GP_UserTakeResult
    {
        public uint dwUserID;							//用户 I D
        public long lUserScore;							//用户金币
        public long lUserInsure;						//银行金币
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //查询银行
    internal struct CMD_GP_QueryInsureInfo
    {
        public uint dwUserID;							//用户 I D
        //	TCHAR							szPassword[LEN_MD5];				//银行密码
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //查询用户
    internal struct CMD_GP_QueryUserInfoRequest
    {
        public byte cbByNickName;                       //昵称赠送
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//目标用户
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //用户信息
    internal struct CMD_GP_UserTransferUserInfo
    {
        public uint dwTargetGameID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//目标用户
    };
    //////////////////////////////////////////////////////////////////////////////////

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //操作失败
    internal struct CMD_GP_OperateFailure
    {
        public int lResultCode;						//错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				//描述消息
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    //操作成功
    internal struct CMD_GP_OperateSuccess
    {
        public int lResultCode;						//操作代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				//成功消息
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_TransferRecord
    {
        public uint dwUserID;							//用户 I D
        public byte cbAll;
        public SYSTEMTIME dtTime;
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserTransferRecordResult
    {
        public uint dwRecordID;                             //记录ID
        public uint dwSourceUserID;                         //赠送用户ID
        public uint dwTargetUserID;                          //获赠用户ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szSourceNickName;         //赠送用户昵称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szTargetNickName;          //收到赠送昵称
        public long lScore;								    //用户游戏币
        public SYSTEMTIME dtTime;							        //赠送时间
        public long lRevenue;								//转账税收
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_UserTransferResult
    {
        public long lUserScore;								//用户金币
        public long lUserInsure;
        public uint dwRecordID;                             //记录ID
        public uint dwSourceUserID;                         //赠送用户ID
        public uint dwTargetUserID;                           //获赠用户ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szSourceNickName;         //赠送用户昵称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szTargetNickName;           //赠送
        public long lScore;								    //用户游戏币
        public SYSTEMTIME dtTime;							        //赠送时间
        public long lRevenue;								//税收
    };
    #endregion
    #region 用户命令
    internal enum USER_SUB_CMD
    {
        //用户动作
        SUB_GR_USER_RULE = 1,								    //用户规则
        SUB_GR_USER_LOOKON = 2,							        //旁观请求
        SUB_GR_USER_SITDOWN = 3,								//坐下请求
        SUB_GR_USER_STANDUP = 4,								//起立请求
        SUB_GR_USER_INVITE = 5,							        //用户邀请
        SUB_GR_USER_INVITE_REQ = 6,							    //邀请请求
        SUB_GR_USER_REPULSE_SIT = 7,							//拒绝玩家坐下
        SUB_GR_USER_KICK_USER = 8,                              //踢出用户
        SUB_GR_USER_INFO_REQ = 9,                               //请求用户信息
        SUB_GR_USER_CHAIR_REQ = 10,                             //请求更换位置
        SUB_GR_USER_CHAIR_INFO_REQ = 11,                         //请求椅子用户信息
        SUB_GR_USER_WAIT_DISTRIBUTE = 12,						//等待分配
        SUB_GR_USER_CALL = 13,							        //呼叫用户
        SUB_GR_USER_OPERATE = 14,		                        //用户操作	
        //用户状态
        SUB_GR_USER_ENTER = 100,						        //用户进入
        SUB_GR_USER_SCORE = 101,						        //用户分数
        SUB_GR_USER_STATUS = 102,						        //用户状态
        SUB_GR_REQUEST_FAILURE = 103,						    //请求失败

        //聊天命令
        SUB_GR_USER_CHAT = 201,						            //聊天消息
        SUB_GR_USER_EXPRESSION = 202,						    //表情消息
        SUB_GR_WISPER_CHAT = 203,						        //私聊消息
        SUB_GR_WISPER_EXPRESSION = 204,						    //私聊表情
        SUB_GR_COLLOQUY_CHAT = 205,						        //会话消息
        SUB_GR_COLLOQUY_EXPRESSION = 206,						//会话表情

        //道具命令
        SUB_GR_PROPERTY_BUY = 300,							    //购买道具
        SUB_GR_PROPERTY_SUCCESS = 301,							//道具成功
        SUB_GR_PROPERTY_FAILURE = 302,							//道具失败
        SUB_GR_PROPERTY_MESSAGE = 303,                          //道具消息
        SUB_GR_PROPERTY_EFFECT = 304,                           //道具效应
        SUB_GR_PROPERTY_TRUMPET = 305,                          //喇叭消息

        //其他配置
        SUB_GR_AUSER_COFIG = 306,						        //其他信息
        SUB_GR_CONFIG_UPDATESCORE = 307,						//更新彩金
        SUB_GR_UPDATESCORE_RESULT = 308,						//彩金返回
        SUB_GR_UPDATEUSER_RESULT = 309,						    //玩家返回
    }

    //操作请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_UserManageOperate
    {
        public uint dwMeUserID;
        public uint dwTargetUserID;
        public bool bManageType;
        public int nCount;
        public int nTableID;
    };
    //玩家定义
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_LatelyUserInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;				            //玩家名字
        public long lUserScore;							    //获奖金币
        public long lAllScore;							    //全部彩金
    };

    //最近玩家
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_LatelyUserScore
    {
        public long lAllScore;							    //全部数目
    };

    //旁观请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserLookon
    {
        public ushort wTableID;							    //桌子位置
        public ushort wChairID;							    //椅子位置
    };

    //坐下请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserSitDown
    {
        public ushort wTableID;							    //桌子位置
        public ushort wChairID;							    //椅子位置
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szPassword;			                //桌子密码
    };

    //起立请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserStandUp
    {
        public ushort wTableID;							    //桌子位置
        public ushort wChairID;							    //椅子位置
        public byte cbForceLeave;						    //强行离开
    };

    //邀请用户 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserInvite
    {
        public ushort wTableID;							    //桌子号码
        public uint dwUserID;							    //用户 I D
    };

    //邀请用户请求 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserInviteReq
    {
        public ushort wTableID;							    //桌子号码
        public uint dwUserID;							    //用户 I D
    };
    //呼叫请求 
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserCall
    {
        public uint dwUserID;							    //用户 I D
    };
    //用户分数
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserScore
    {
        public uint dwUserID;							    //用户标识
        public tagUserScore UserScore;							    //积分信息
    };

    //用户分数
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_MobileUserScore
    {
        public uint dwUserID;							    //用户标识
        public tagMobileUserScore UserScore;						//积分信息
    };

    //用户状态
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserStatus
    {
        public uint dwUserID;							    //用户标识
        public tagUserStatus UserStatus;							//用户状态
    };

    //请求失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_RequestFailure
    {
        public int lErrorCode;							    //错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szDescribeString;				        //描述信息
    };


    //用户聊天
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_UserChat
    {
        public ushort wChatLength;						    //信息长度
        public uint dwChatColor;						    //信息颜色
        public uint dwTargetUserID;						    //目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;		                    //聊天信息
    };

    //用户聊天
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserChat
    {
        public ushort wChatLength;						//信息长度
        public uint dwChatColor;						//信息颜色
        public uint dwSendUserID;						//发送用户
        public uint dwTargetUserID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;		                //聊天信息
    };

    //用户表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_UserExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwTargetUserID;						    //目标用户
    };

    //用户表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwSendUserID;						    //发送用户
        public uint dwTargetUserID;						    //目标用户
    };

    //用户私聊
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_WisperChat
    {
        public ushort wChatLength;						//信息长度
        public uint dwChatColor;						//信息颜色
        public uint dwTargetUserID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;		                //聊天信息
    };

    //用户私聊
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_WisperChat
    {
        public ushort wChatLength;						//信息长度
        public uint dwChatColor;						//信息颜色
        public uint dwSendUserID;						//发送用户
        public uint dwTargetUserID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;		                //聊天信息
    };

    //私聊表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_WisperExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwTargetUserID;						    //目标用户
    };

    //私聊表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_WisperExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwSendUserID;						    //发送用户
        public uint dwTargetUserID;						    //目标用户
    };


    //邀请用户
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_InviteUser
    {
        public ushort wTableID;							    //桌子号码
        public uint dwSendUserID;						    //发送用户
    };

    //邀请用户
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_InviteUser
    {
        public uint dwTargetUserID;						    //目标用户
    };

    //购买道具
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_PropertyBuy
    {
        public byte cbRequestArea;						    //请求范围
        public byte cbConsumeScore;						    //积分消费
        public ushort wItemCount;							//购买数目
        public ushort wPropertyIndex;						//道具索引	
        public uint dwTargetUserID;						    //使用对象
    };

    //道具成功
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_PropertySuccess
    {
        public byte cbRequestArea;						    //使用环境
        public ushort wItemCount;							//购买数目
        public ushort wPropertyIndex;						//道具索引
        public uint dwSourceUserID;						    //目标对象
        public uint dwTargetUserID;						    //使用对象
    };

    //道具失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_PropertyFailure
    {
        public ushort wRequestArea;                         //请求区域
        public int lErrorCode;							    //错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szDescribeString;				        //描述信息
    };

    //道具消息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_PropertyMessage
    {
        //道具信息
        public ushort wPropertyIndex;                       //道具索引
        public ushort wPropertyCount;                       //道具数目
        public uint dwSourceUserID;                         //目标对象
        public uint dwTargerUserID;                         //使用对象
    };


    //道具效应
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_PropertyEffect
    {
        public uint wUserID;					            //用 户I D
        public byte cbMemberOrder;						    //会员等级
    };

    //发送喇叭
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_SendTrumpet
    {
        public byte cbRequestArea;                          //请求范围 
        public ushort wPropertyIndex;                       //道具索引 
        public uint TrumpetColor;                           //喇叭颜色
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.TRUMPET_MAX_CHAR)]
        public string szTrumpetContent;  //喇叭内容
    };

    //发送喇叭
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_SendTrumpet
    {
        public ushort wPropertyIndex;                       //道具索引 
        public uint dwSendUserID;                           //用户 I D
        public uint TrumpetColor;                           //喇叭颜色
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szSendNickName;				        //玩家昵称 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.TRUMPET_MAX_CHAR)]
        public string szTrumpetContent;                     //喇叭内容
    };


    //用户拒绝黑名单坐下
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_UserRepulseSit
    {
        public ushort wTableID;							    //桌子号码
        public ushort wChairID;							    //椅子位置
        public uint dwUserID;							    //用户 I D
        public uint dwRepulseUserID;					    //用户 I D
    };

    #endregion
    #region 状态命令
    internal enum STATUS_SUB_CMD
    {
        SUB_GR_TABLE_INFO = 100,								//桌子信息
        SUB_GR_TABLE_STATUS = 101,							    //桌子状态
    }
    //桌子信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GR_TableInfo
    {
        public ushort wTableCount;						                        //桌子数目
        public List<tagTableStatus> ItemArray = new List<tagTableStatus>();		//桌子状态
        public byte[] Buffer
        {
            set
            {
                wTableCount = (ushort)(value[0] + value[1] * 0xFF);
                int nItemSize = Marshal.SizeOf(typeof(tagTableStatus));
                int nItemCount = wTableCount;
                if (nItemCount == 0)
                    return;
                MemoryStream stream = new MemoryStream(value);
                stream.Seek(2, SeekOrigin.Begin);

                for (int i = 0; i < nItemCount; i++)
                {
                    byte[] buffer = new byte[nItemSize];
                    stream.Read(buffer, 0, nItemSize);
                    tagTableStatus Item = (tagTableStatus)PacketHelper.BytesToStruct(buffer, nItemSize, typeof(tagTableStatus));
                    ItemArray.Add(Item);
                }
            }
        }
    }

    //桌子状态
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_TableStatus
    {
        public ushort wTableID;							//桌子号码
        public tagTableStatus TableStatus;						//桌子状态
    };
    #endregion
    #region 银行命令
    internal enum INSURE_SUB_CMD
    {
        //银行命令
        SUB_GR_QUERY_INSURE_INFO = 1,								//查询银行
        SUB_GR_SAVE_SCORE_REQUEST = 2,								//存款操作
        SUB_GR_TAKE_SCORE_REQUEST = 3,								//取款操作
        SUB_GR_TRANSFER_SCORE_REQUEST = 4,							//取款操作
        SUB_GR_QUERY_USER_INFO_REQUEST = 5,							//查询用户
        SUB_GR_TAKE_GAME_SCORE_REQUEST = 6,

        SUB_GR_USER_INSURE_INFO = 100,								//银行资料
        SUB_GR_USER_INSURE_SUCCESS = 101,							//银行成功
        SUB_GR_USER_INSURE_FAILURE = 102,							//银行失败
        SUB_GR_USER_TRANSFER_USER_INFO = 103,						//用户资料
    }


    //查询银行
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_QueryInsureInfoRequest
    {
        public byte cbActivityGame;                     //游戏动作
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szInsurePass;			            //银行密码
    };

    //存款请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_SaveScoreRequest
    {
        public byte cbActivityGame;                     //游戏动作
        public long lSaveScore;							//存款数目
    };

    //取款请求
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_TakeScoreRequest
    {
        public byte cbActivityGame;                     //游戏动作
        public long lTakeScore;							//取款数目
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_TakeGameScoreRequest
    {
        public byte cbActivityGame;                     //游戏动作
        public long lTakeScore;							//取款数目
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_PASSWORD)]
        public string szInsurePass;			            //银行密码
    };
    //转账金币
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GP_C_TransferScoreRequest
    {
        public byte cbActivityGame;                     //游戏动作
        public byte cbByNickName;                       //昵称赠送
        public long lTransferScore;						//转账金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//目标用户
    };

    //查询用户
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_C_QueryUserInfoRequest
    {
        public byte cbActivityGame;                     //游戏动作
        public byte cbByNickName;                       //昵称赠送
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			//目标用户
    };

    //银行资料
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserInsureInfo
    {
        public byte cbActivityGame;                     //游戏动作
        public ushort wRevenueTake;						//税收比例
        public ushort wRevenueTransfer;					//税收比例
        public ushort wServerID;						//房间标识
        public long lUserScore;							//用户金币
        public long lUserInsure;						//银行金币
        public long lTransferPrerequisite;				//转账条件
    };

    //银行成功
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserInsureSuccess
    {
        public byte cbActivityGame;                     //游戏动作
        public long lUserScore;							//身上金币
        public long lUserInsure;						//银行金币
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				    //描述消息
    };

    //银行失败
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserInsureFailure
    {
        public byte cbActivityGame;                     //游戏动作
        public int lErrorCode;							//错误代码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szDescribeString;				    //描述消息
    };

    //用户信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_S_UserTransferUserInfo
    {
        public byte cbActivityGame;                     //游戏动作
        public uint dwTargetGameID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_NICKNAME)]
        public string szNickName;			            //目标用户
    };
    #endregion
    #region 框架命令
    internal enum FRAME_SUB_CMD
    {
        //用户命令
        SUB_GF_GAME_OPTION = 1,									    //游戏配置
        SUB_GF_USER_READY = 2,									    //用户准备
        SUB_GF_LOOKON_CONFIG = 3,									//旁观配置
        SUB_GF_SET_TABLESCORE = 4,									//底分设置

        //聊天命令
        SUB_GF_USER_CHAT = 10,									    //用户聊天
        SUB_GF_USER_EXPRESSION = 11,								//用户表情

        //设置命名
        SUB_GF_NOSET_SCORE = 50,								    //设置分数
        SUB_GF_YESSET_SCORE = 51,									//确认分数
        SUB_GF_SET_SHIBAI = 52,							            //设置失败
        SUB_ANDROID_STANDUP = 53,									//机器起立

        //游戏信息
        SUB_GF_GAME_STATUS = 100,								    //游戏状态
        SUB_GF_GAME_SCENE = 101,								    //游戏场景
        SUB_GF_LOOKON_STATUS = 102,								    //旁观状态

        //系统消息
        SUB_GF_SYSTEM_MESSAGE = 200,								//系统消息
        SUB_GF_ACTION_MESSAGE = 201,								//动作消息
    }

    //配置信息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GR_AUserCofigInfo
    {
        public uint dwUserID;								//用户ID
    };

    //等待消息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_SetShiBai
    {
        public int lMinSetScore;						//最小设置
        public int lMaxSetScore;						//最大设置
        int MaxTimes;							//游戏倍数
        public long lUserScore;							//用户分数
    };

    //设置完成
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_SetFinshTableScore
    {
        public long lCellScore;								//游戏分数
    };

    //设置底分
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_SetTableScore
    {
        public int MaxTimes;							//游戏倍数
        public long lUserScore;							//用户分数
    };

    //游戏配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_GameOption
    {
        public byte cbAllowLookon;						//旁观标志
        public uint dwFrameVersion;						//框架版本
        public uint dwClientVersion;					//游戏版本
        public long lSetScore;							//分数设置
    };

    //旁观配置
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_LookonConfig
    {
        public uint dwUserID;							//用户标识
        public byte cbAllowLookon;						//允许旁观
    };

    //旁观状态
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_LookonStatus
    {
        public byte cbAllowLookon;						//允许旁观
    };

    //游戏环境
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_GameStatus
    {
        public byte cbGameStatus;						//游戏状态
        public byte cbAllowLookon;						//旁观标志
    };

    //用户聊天
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_C_UserChat
    {
        public ushort wChatLength;						//信息长度
        public uint dwChatColor;						//信息颜色
        public uint dwTargetUserID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;                       		//聊天信息
    };

    //用户聊天
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_S_UserChat
    {
        public ushort wChatLength;						//信息长度
        public uint dwChatColor;						//信息颜色
        public uint dwSendUserID;						//发送用户
        public uint dwTargetUserID;						//目标用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)STR_LEN.LEN_USER_CHAT)]
        public string szChatString;                       		//聊天信息
    };

    //用户表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_C_UserExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwTargetUserID;						//目标用户
    };

    //用户表情
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_GF_S_UserExpression
    {
        public ushort wItemIndex;							//表情索引
        public uint dwSendUserID;						//发送用户
        public uint dwTargetUserID;						//目标用户
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    [Packet(Dynamic = true)]
    internal class CMD_GF_GameScene
    {
        public byte[] Buffer
        {
            set;
            get;
        }
    };
    #endregion
    #region 系统命令
    internal enum SYSTEM_SUB_CMD
    {
        SUB_CM_SYSTEM_MESSAGE = 1,									//系统消息
        SUB_CM_ACTION_MESSAGE = 2,									//动作消息
        SUB_CM_DOWN_LOAD_MODULE = 3,									//下载消息
    }
    //系统消息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_CM_SystemMessage
    {
        public ushort wType;								//消息类型
        public ushort wLength;							    //消息长度
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string szString;						        //消息内容
    };

    //动作消息
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    internal struct CMD_CM_ActionMessage
    {
        public ushort wType;								//消息类型
        public ushort wLength;							//消息长度
        public uint nButtonType;						//按钮类型
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string szString;						                                //消息内容
    };

    #endregion
}
