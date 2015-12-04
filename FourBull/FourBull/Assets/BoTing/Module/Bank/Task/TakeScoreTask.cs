using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class TakeScoreTask : BaseTask
    {
        private long score;
        private ushort kindID;
        private ISocketHander socketHdr = null;
        public long UserInsure;
        public long UserScore;
        public TakeScoreTask(ISocketHander socketHdr, long score, ushort kindID)
        {
            this.socketHdr = socketHdr;
            this.score = score;
            this.kindID = kindID;
            Description = "执行取款任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_UserTakeScore UserTakeScore = new CMD_GP_UserTakeScore();
            UserTakeScore.dwUserID = UserCenter.Instance.UserID;
            UserTakeScore.lTakeScore = score;
            UserTakeScore.szMachineID = HardWare.GetMachineID();
            UserTakeScore.wKindID = kindID;
            UserTakeScore.cbStaty = 1;
            UserTakeScore.szCodeID = "";
            socketHdr.SendPacket<CMD_GP_UserTakeScore>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TAKE_SCORE, UserTakeScore);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserInsureSuccess, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS, typeof(CMD_GP_UserInsureSuccess));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserInsureFailure, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE, typeof(CMD_GP_UserInsureFailure));
       
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr = null;
        }
        private void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }

        private void OnUserInsureSuccess(System.Object Target)
        {
            CMD_GP_UserInsureSuccess Obj = (CMD_GP_UserInsureSuccess)Target;
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
