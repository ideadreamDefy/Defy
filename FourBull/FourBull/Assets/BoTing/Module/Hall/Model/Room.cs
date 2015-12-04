using BoTing.Framework;
using System;

namespace BoTing.Module
{
    public class Room : IDisposable, IRoom
    {
        private tagGameServer mGameServer;
        private tagGameKind mKind;
        private ushort mChairCount = 0;
        private ushort mTableCount = 0;
        private ushort mServerType = 0;
        private bool mIsCaiJinRoom = false;
        private uint mServerRule=0;

        private ISocketHander mSocketHander = NetWorkManager.Instance.CreateSocket();
        private UserManager mUserManager = null;
        private TableManager mTableManager = null;
        public UserManager UserManager { get { return mUserManager; } }
        public TableManager TableManager { get { return mTableManager; } }
        #region 各种属性
        public ushort ChairCount
        {
            get { return mChairCount; } 
        }
        public ushort TableCount
        {
            get { return mTableCount; }
        }
        public bool IsCaiJinRoom
        {
            get { return mIsCaiJinRoom; }
        }
        public uint ServerRule
        {
            get { return mServerRule; }
        }
        public ushort KindID
        {
            get { return mGameServer.wKindID; }
        }
        public ushort ServerID
        {
            get { return mGameServer.wServerID; }
        }
        public string ServerAddr
        {
            get { return mGameServer.szServerAddr; }
        }
        public ushort ServerPort
        {
            get { return mGameServer.wServerPort; }
        }
        public ISocketHander SocketHander
        {
            get { return mSocketHander; }
        }
        public string ServerName
        {
            get { return mGameServer.szServerName; }
        }
        public string ProcessName
        {
            get { return mKind.szProcessName; }
        }
        public string KindName
        {
            get { return mKind.szKindName; }
        }
        #endregion
        public Room(tagGameServer Server, tagGameKind Kind)
        {
            mGameServer = Server;
            mKind = Kind;
            mUserManager = new UserManager(this);
            mTableManager = new TableManager(this);
           // mFrameManger = new GameClientManager(this);

            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnRequestFailureEvent, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_REQUEST_FAILURE, typeof(CMD_GR_RequestFailure));

            //游戏配置
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnCongigColumnEvent, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_COLUMN, typeof(CMD_GR_ConfigColumn));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnCongigServerEvent, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_SERVER, typeof(CMD_GR_ConfigServer));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnCongigPropertyEvent, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_PROPERTY, typeof(CMD_GR_ConfigProperty));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnCongigFinishEvent, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_FINISH);
        }
        //进入桌子
        public void PerformSitDown(ushort wTableID, ushort wChairID, string strPassword)
        {
            CMD_GR_UserSitDown UserSitDown = new CMD_GR_UserSitDown();
            UserSitDown.wTableID = wTableID;
            UserSitDown.wChairID = wChairID;
            UserSitDown.szPassword = strPassword;
            if (mSocketHander != null && mSocketHander.IsValid())
            {
                mSocketHander.SendPacket<CMD_GR_UserSitDown>((ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_SITDOWN, UserSitDown);
            }
        }

        public void SetOption(byte cbAllowLookon, uint dwClientVersion, uint dwFrameVersion)
        {
            if (mSocketHander != null && mSocketHander.IsValid())
            {
                CMD_GF_GameOption GameOption = new CMD_GF_GameOption();
                GameOption.dwClientVersion = dwClientVersion;
                GameOption.dwFrameVersion = dwFrameVersion;
                GameOption.cbAllowLookon = cbAllowLookon;
                mSocketHander.SendPacket<CMD_GF_GameOption>((ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_GAME_OPTION, GameOption);
            }
        }

        //离开桌子
        public void PrefromStandUp(byte cbForceLeave)
        {
            CMD_GR_UserStandUp UserStandUp = new CMD_GR_UserStandUp();
            UserStandUp.cbForceLeave = cbForceLeave;
            UserStandUp.wTableID = UserManager.Self.TableID;
            UserStandUp.wChairID = UserManager.Self.ChairID;
            //发送数据
            if (mSocketHander != null && mSocketHander.IsValid())
            {
                mSocketHander.SendPacket<CMD_GR_UserStandUp>((ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_STANDUP, UserStandUp);
            }
        }
        private void OnCongigColumnEvent(System.Object Target)
        {
            CMD_GR_ConfigColumn Obj = (CMD_GR_ConfigColumn)Target;
        }
        private void OnCongigServerEvent(System.Object Target)
        {
            CMD_GR_ConfigServer Obj = (CMD_GR_ConfigServer)Target;
            mChairCount = Obj.wChairCount;
            mTableCount = Obj.wTableCount;
            mServerType = Obj.wServerType;
            mIsCaiJinRoom = Obj.bCaiJinRoom != 0;
            mServerRule = Obj.dwServerRule;
        }
        private void OnCongigPropertyEvent(System.Object Target)
        {
            CMD_GR_ConfigProperty Obj = (CMD_GR_ConfigProperty)Target;
        }
        private void OnCongigFinishEvent(System.Object Target)
        {

        }
        private void OnRequestFailureEvent(System.Object Target)
        {
            CMD_GR_RequestFailure Obj = (CMD_GR_RequestFailure)Target;
            DebugKit.Log(Obj.szDescribeString);
        }

        public void Dispose()
        {
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_REQUEST_FAILURE);

            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_COLUMN);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_SERVER);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_PROPERTY);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_CONFIG, (ushort)CONFIG_SUB_CMD.SUB_GR_CONFIG_FINISH);

            mUserManager.Dispose();
            mTableManager.Dispose();
            if (mSocketHander.IsValid())
                mSocketHander.Shutdown();

            mTableManager = null;
            mSocketHander = null;
            mUserManager = null;
        }

        public void Close()
        {
            if (mSocketHander.IsValid())
                mSocketHander.Shutdown();
        }
    }
}
