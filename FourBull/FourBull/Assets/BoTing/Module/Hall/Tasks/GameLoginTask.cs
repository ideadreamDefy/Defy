using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    internal class GameLoginTask : BaseTask
    {
        private Room mRoom;
        private ISocketHander mSocketHander = null;
        public GameLoginTask(Room room)
        {
            Name = typeof(GameLoginTask).ToString();
            mRoom = room;
            Description = "正在连接到游戏服务器.";
        }
        override public void OnExecute()
        {
            Init();
            mSocketHander.Connect(mRoom.ServerAddr, mRoom.ServerPort);
        }

        public override void OnFinish()
        {
            UnInit();
        }

        public override void OnCancel()
        {
            UnInit();
        }

        private void Init()
        {
            mSocketHander = mRoom.SocketHander;

            //登录部分
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CONNECT, OnConnectEvent);
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE, OnCloseEvent);
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLogonSuccessEvent, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_SUCCESS, typeof(CMD_GR_LogonSuccess));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLogonFailEvent, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_FAILURE, typeof(CMD_GR_LogonFailure));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLogonFinishEvent, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_FINISH);
        }

        private void OnLogonSuccessEvent(System.Object Target)
        {
            CMD_GR_LogonSuccess LogonSuccess = (CMD_GR_LogonSuccess)Target;
        }
        private void OnLogonFailEvent(System.Object Target)
        {
            CMD_GR_LogonFailure LogonFailure = (CMD_GR_LogonFailure)Target;
            this.Message = LogonFailure.szDescribeString;
            this.Cancel();
        }
        private void OnLogonFinishEvent(System.Object Target)
        {
            if (mRoom.UserManager.Self.TableID == 65535)
                mRoom.PerformSitDown(1, 65535, "");
            this.Finish();
        }
        private void UnInit()
        {
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CONNECT);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_SUCCESS);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_FAILURE);
        }
        public void OnConnectEvent(System.Object Target)
        {
            if (mSocketHander.IsValid())
            {
                SendLoginPakcet();
            }
            else
            {
                this.Message = "连接到服务器失败";
                this.Cancel();
            }

        }
        public void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接断开";
            this.Cancel();
        }
        private void SendLoginPakcet()
        {
            CMD_GR_LogonUserID LogonUserID = new CMD_GR_LogonUserID();
            LogonUserID.dwFrameVersion = 16777217;
            LogonUserID.dwProcessVersion = 16777217;
            LogonUserID.dwPlazaVersion = 101318659;

            LogonUserID.szMachineID = HardWare.GetMachineID();
            LogonUserID.szPassword = Encrypt.MD5Encrypt32(UserCenter.Instance.Password);
            LogonUserID.dwUserID = UserCenter.Instance.UserID;
            LogonUserID.wKindID = mRoom.KindID;

            mSocketHander.SendPacket<CMD_GR_LogonUserID>((ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_USERID, LogonUserID);
        }
    }

}
