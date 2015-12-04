using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class ModifyInsurePassTask : BaseTask
    {
        private string newPassword;
        private string oldPassword;
        private ISocketHander socketHdr = null;
        public ModifyInsurePassTask(ISocketHander socketHdr, string oldPassword, string newPassword)
        {
            this.socketHdr = socketHdr;
            this.oldPassword = oldPassword;
            this.newPassword = newPassword;
            Description = "执行修改密码任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_ModifyInsurePass ModifyInsurePass = new CMD_GP_ModifyInsurePass();
            ModifyInsurePass.dwUserID = UserCenter.Instance.UserID;
            ModifyInsurePass.szDesPassword = Encrypt.MD5Encrypt32(newPassword);
            ModifyInsurePass.szScrPassword = Encrypt.MD5Encrypt32(oldPassword);
            socketHdr.SendPacket<CMD_GP_ModifyInsurePass>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_MODIFY_INSURE_PASS, ModifyInsurePass);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnOperateSuccess, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_OPERATE_SUCCESS, typeof(CMD_GP_OperateSuccess));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OperateFailure, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_OPERATE_FAILURE, typeof(CMD_GP_OperateFailure));  
            
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_OPERATE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_OPERATE_FAILURE);
            socketHdr = null;
        }
        private void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }

        private void OnOperateSuccess(System.Object Target)
        {
            CMD_GP_OperateSuccess Obj = (CMD_GP_OperateSuccess)Target;
            UserCenter.Instance.BankPwd = newPassword;
            this.Finish();
        }
        private void OperateFailure(System.Object Target)
        {
            CMD_GP_OperateFailure Obj = (CMD_GP_OperateFailure)Target;
            this.Message = Obj.szDescribeString;
            this.Cancel();
        }
    }
}
