using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class HallLoginTask : BaseTask
    {
        private string mAccount;
        private string mPwd;
        private ISocketHander socketHdr = null;
        public HallLoginTask(ISocketHander socketHdr, string Account, string Pwd)
        {
            this.socketHdr = socketHdr;
            mAccount = Account;
            mPwd = Pwd;
            Description = "正在验证用户的和密码.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_LogonAccounts LogonAccounts = new CMD_GP_LogonAccounts();
            LogonAccounts.dwPlazaVersion = 101318659;
            LogonAccounts.szMachineID = HardWare.GetMachineID();
            LogonAccounts.szPassword = Encrypt.MD5Encrypt32(mPwd); 
            LogonAccounts.szAccounts = mAccount;

            LogonAccounts.cbValidateFlags = 3;
            LogonAccounts.cbPassPortID = 0;
            LogonAccounts.szPassPortID = "";

            socketHdr.SendPacket<CMD_GP_LogonAccounts>((ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_ACCOUNTS, LogonAccounts);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE, OnCloseEvent);
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLoginSuccess, (ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_SUCCESS, typeof(CMD_GP_LogonSuccess));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLoginFail, (ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_FAILURE, typeof(CMD_GP_LogonFailure));

            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameType, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_TYPE, typeof(CMD_GP_GameType));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameKind, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_KIND, typeof(CMD_GP_GameKind));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameNode, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_NODE, typeof(CMD_GP_GameNode));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGamePage, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_PAGE, typeof(CMD_GP_GamePage));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameServer, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_SERVER, typeof(CMD_GP_GameServer));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnListFinish, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_FINISH);
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLogonFinishEvent, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_FINISH);

            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnValidatePassport, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_VALIDATE_PASSPORT_ID);
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GR_LOGON_FAILURE);

            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_TYPE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_KIND);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_NODE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_PAGE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_SERVER);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_FINISH);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_FINISH);

            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_VALIDATE_PASSPORT_ID);
            socketHdr = null;
        }
        public void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }

        public void OnLoginFail(System.Object Target)
        {
            CMD_GP_LogonFailure Obj = (CMD_GP_LogonFailure)Target;
            this.Message = Obj.szDescribeString;
            DebugKit.Log("登录失败!" + this.Message);
            this.Cancel();
        }
        private void OnLogonFinishEvent(System.Object Target)
        {
            DebugKit.Log("登录完成事件!");
            this.Finish();
        }
        public void OnLoginSuccess(System.Object Target)
        {
            DebugKit.Log("登录成功!");
            CMD_GP_LogonSuccess Obj = (CMD_GP_LogonSuccess)Target;
            UserCenter.Instance.UserID = Obj.dwUserID;
            UserCenter.Instance.FaceID = Obj.wFaceID;
            UserCenter.Instance.Account = Obj.szAccounts;
            UserCenter.Instance.Password = mPwd;
            UserCenter.Instance.NickName = Obj.szNickName;
            UserCenter.Instance.GameID = Obj.dwGameID;
        }
        public void OnGameType(System.Object Target)
        {
            DebugKit.Log("OnGameType!");
            CMD_GP_GameType Obj = (CMD_GP_GameType)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameTypes.Add(item);
            }
        }
        public void OnGameKind(System.Object Target)
        {
            DebugKit.Log("OnGameKind!");
            CMD_GP_GameKind Obj = (CMD_GP_GameKind)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameKinds.Add(item);
            }
        }
        public void OnGamePage(System.Object Target)
        {
            DebugKit.Log("OnGamePage!");
            CMD_GP_GamePage Obj = (CMD_GP_GamePage)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GamePages.Add(item);
            }
        }
        public void OnGameNode(System.Object Target)
        {
            DebugKit.Log("OnGameNode!");
            CMD_GP_GameNode Obj = (CMD_GP_GameNode)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameNodes.Add(item);
            }
        }
        public void OnGameServer(System.Object Target)
        {
            DebugKit.Log("OnGameServer!");
            CMD_GP_GameServer Obj = (CMD_GP_GameServer)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameServers.Add(item);
            }
        }
        public void OnListFinish(System.Object Target)
        {
            DebugKit.Log("OnListFinish!");
        }
        public void OnValidatePassport(System.Object Target)
        {
            DebugKit.Log("OnValidatePassport!");
            CMD_GP_LogonAccounts LogonAccounts = new CMD_GP_LogonAccounts();
            LogonAccounts.dwPlazaVersion = 101318659;
            LogonAccounts.szMachineID = HardWare.GetMachineID();
            LogonAccounts.szPassword = Encrypt.MD5Encrypt32(mPwd);
            LogonAccounts.szAccounts = mAccount;

            LogonAccounts.cbValidateFlags = 3;
            LogonAccounts.cbPassPortID = 1;
            LogonAccounts.szPassPortID = "432326197701215789";

            socketHdr.SendPacket<CMD_GP_LogonAccounts>((ushort)MAIN_CMD.MDM_GP_LOGON, (ushort)LOGON_SUB_CMD.SUB_GP_LOGON_ACCOUNTS, LogonAccounts);
        }

    }
}
