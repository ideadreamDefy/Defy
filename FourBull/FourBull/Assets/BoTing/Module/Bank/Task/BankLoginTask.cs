using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class BankLoginTask : BaseTask
    {
        private string mPwd;
        private ISocketHander socketHdr = null;
        public BankLoginTask(ISocketHander socketHdr, string Pwd)
        {
            this.socketHdr = socketHdr;
            mPwd = Pwd;
            Description = "正在验证保险箱密码.";
        }
        override public void OnExecute()
        {
            Init();
            ActionLogin();
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLoginSuccess, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_LOGON_SUCCESS);
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLoginFail, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_LOGON_FAILURE, typeof(CMD_GP_UserInsureLogonFailure));
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_LOGON_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_LOGON_FAILURE);
            socketHdr = null;
        }
        private void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }

        private void OnLoginFail(System.Object Target)
        {
            CMD_GP_UserInsureLogonFailure Obj = (CMD_GP_UserInsureLogonFailure)Target;
            this.Message = Obj.szDescribeString;
            this.Cancel();
        }
        private void OnLoginSuccess(System.Object Target)
        {
            UserCenter.Instance.BankPwd = mPwd;
            this.Finish();
        }

        private void ActionLogin()
        {
            CMD_GP_UserInsureLogon UserInsureLogon = new CMD_GP_UserInsureLogon();
            UserInsureLogon.szMachineID = HardWare.GetMachineID();
            UserInsureLogon.szPassword = Encrypt.MD5Encrypt32(mPwd);
            UserInsureLogon.dwUserID = UserCenter.Instance.UserID;
            socketHdr.SendPacket<CMD_GP_UserInsureLogon>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_LOGON, UserInsureLogon);
        }
    }
}
