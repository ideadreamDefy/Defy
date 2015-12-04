using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class QueryGameInsureTask : BaseTask
    {
        private ushort kindID;
        private ISocketHander socketHdr = null;
        public long UserInsure;
        public long UserScore;
        public QueryGameInsureTask(ISocketHander socketHdr, ushort kindID)
        {
            this.socketHdr = socketHdr;
            this.kindID = kindID;
            Description = "查询游戏存款任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_QueryGameInsureInfo QueryGameInsureInfo = new CMD_GP_QueryGameInsureInfo();
            QueryGameInsureInfo.dwUserID = UserCenter.Instance.UserID;
            QueryGameInsureInfo.wKindID = kindID;
            socketHdr.SendPacket<CMD_GP_QueryGameInsureInfo>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_QUERY_GAME_INSURE_INFO, QueryGameInsureInfo);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserGameInsureInfo, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_GAME_INSURE_INFO, typeof(CMD_GP_UserGameInsureInfo));
            
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_GAME_INSURE_INFO);
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
            this.Finish();
        }
        private void OnUserInsureFailure(System.Object Target)
        {
            CMD_GP_UserInsureFailure Obj = (CMD_GP_UserInsureFailure)Target;
            this.Message = Obj.szDescribeString;
            this.Cancel();
        }
        private void OnUserGameInsureInfo(System.Object Target)
        {
            CMD_GP_UserGameInsureInfo Obj = (CMD_GP_UserGameInsureInfo)Target;
            this.UserInsure = Obj.lUserInsure;
            this.UserScore = Obj.lUserScore;
            DebugKit.Log("UserInsure:" + Obj.lUserInsure.ToString() + " UserScore:" +Obj.lUserScore.ToString());
            this.Finish();
        }
    }
}
