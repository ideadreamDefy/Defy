using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class QueryInsureTask : BaseTask
    {
        private ISocketHander socketHdr = null;
        public long UserInsure;
        public long UserScore;
        public QueryInsureTask(ISocketHander socketHdr)
        {
            this.socketHdr = socketHdr;
            Description = "查询保险箱存款任务.";
        }
        override public void OnExecute()
        {
            Init();
            ActionQueryInsureInfo();
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserInsureInfo, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_INFO, typeof(CMD_GP_UserInsureInfo));  

        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_INFO);
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
        }
        private void OnUserInsureFailure(System.Object Target)
        {
            CMD_GP_UserInsureFailure Obj = (CMD_GP_UserInsureFailure)Target;
            this.Message = Obj.szDescribeString;
            this.Cancel();
        }
        private void OnUserInsureInfo(System.Object Target)
        {
            CMD_GP_UserInsureInfo Obj = (CMD_GP_UserInsureInfo)Target;
            this.UserInsure = Obj.lUserInsure;
            DebugKit.Log("Insure:" + Obj.lUserInsure.ToString() + " score:" + Obj.lUserScore.ToString());
            this.Finish();
        }
        private void ActionQueryInsureInfo()
        {
            //变量定义
            CMD_GP_QueryInsureInfo QueryInsureInfo = new CMD_GP_QueryInsureInfo();
            QueryInsureInfo.dwUserID = UserCenter.Instance.UserID;
            //QueryInsureInfo.szPassword = Encrypt.MD5Encrypt32(mPwd);
            socketHdr.SendPacket<CMD_GP_QueryInsureInfo>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_QUERY_INSURE_INFO, QueryInsureInfo);
        }
    }
}
