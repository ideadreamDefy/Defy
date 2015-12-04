using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class QueryUserInfoTask : BaseTask
    {
        private string nickName;
        private bool isNickName;
        private ISocketHander socketHdr = null;
        public QueryUserInfoTask(ISocketHander socketHdr, string nickName, bool isNickName)
        {
            this.socketHdr = socketHdr;
            this.nickName = nickName;
            this.isNickName = isNickName;
            Description = "执行查询用户信息任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_QueryUserInfoRequest QueryUserInfoRequest = new CMD_GP_QueryUserInfoRequest();
            QueryUserInfoRequest.szNickName = nickName;
            QueryUserInfoRequest.cbByNickName = (byte)(isNickName ? 1 : 0);
            socketHdr.SendPacket<CMD_GP_QueryUserInfoRequest>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_QUERY_USER_INFO_REQUEST, QueryUserInfoRequest);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserTransferUserInfo, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_QUERY_USER_INFO_RESULT, typeof(CMD_GP_UserTransferUserInfo));
            
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_QUERY_USER_INFO_RESULT);
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
        private void OnUserTransferUserInfo(System.Object Target)
        {
            CMD_GP_UserTransferUserInfo Obj = (CMD_GP_UserTransferUserInfo)Target;
            this.Finish();
        }
    }
}
