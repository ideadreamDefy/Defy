using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class TransScoreTask : BaseTask
    {
        private long score;
        private string nickName;
        private bool isNickName;
        private ISocketHander socketHdr = null;
        public long UserInsure;
        public long UserScore;
        public TransScoreTask(ISocketHander socketHdr, long score, string nickName, bool isNickName)
        {
            this.socketHdr = socketHdr;
            this.score = score;
            this.nickName = nickName;
            this.isNickName = isNickName;
            Description = "执行赠送任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_UserTransferScore UserTransferScore = new CMD_GP_UserTransferScore();
            UserTransferScore.dwUserID= UserCenter.Instance.UserID;
            UserTransferScore.lTransferScore = score;
            UserTransferScore.szMachineID = HardWare.GetMachineID();
            UserTransferScore.szNickName = nickName;
            UserTransferScore.cbByNickName = (byte)(isNickName ? 1 : 0);
            UserTransferScore.cbStaty = 1;
            UserTransferScore.szCodeID = "";
            UserTransferScore.szPassword = Encrypt.MD5Encrypt32(UserCenter.Instance.BankPwd);
            socketHdr.SendPacket<CMD_GP_UserTransferScore>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_SCORE, UserTransferScore);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserTransferResult, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_SUCCESS, typeof(CMD_GP_UserTransferResult));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserInsureFailure, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE, typeof(CMD_GP_UserInsureFailure));
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr = null;
        }
        private void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }


        private void OnUserTransferResult(System.Object Target)
        {
            CMD_GP_UserTransferResult Obj = (CMD_GP_UserTransferResult)Target;
            this.UserInsure = Obj.lUserInsure;
            this.UserScore = Obj.lUserScore;
            this.Finish();
        }
        private void OnUserInsureFailure(System.Object Target)
        {
            CMD_GP_UserInsureFailure Obj = (CMD_GP_UserInsureFailure)Target;
            this.Message = Obj.szDescribeString;
            this.Cancel();
        }
    }
}
