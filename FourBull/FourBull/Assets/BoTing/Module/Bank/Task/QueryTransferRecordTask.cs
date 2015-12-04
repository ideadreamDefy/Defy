using BoTing.Framework;
using BoTing.GamePublic;
using System.Collections.Generic;
using TransferRecord = BoTing.Module.CMD_GP_UserTransferRecordResult;

namespace BoTing.Module
{
    public class QueryTransferRecordTask : BaseTask
    {
        private ISocketHander socketHdr = null;
        internal Dictionary<uint, TransferRecord> transferRecordDict = new Dictionary<uint, TransferRecord>();
        public QueryTransferRecordTask(ISocketHander socketHdr)
        {
            this.socketHdr = socketHdr;
            Description = "执行查询转帐信息任务.";
        }
        override public void OnExecute()
        {
            Init();
            CMD_GP_TransferRecord TransferRecord = new CMD_GP_TransferRecord();
            TransferRecord.cbAll = 1;
            TransferRecord.dwUserID = UserCenter.Instance.UserID;
            TransferRecord.dtTime = SystemTime.Now;
            socketHdr.SendPacket<CMD_GP_TransferRecord>((ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_TRANSFER_RECORD, TransferRecord);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserTransferRecordResult, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_RECORD_RESULT, typeof(CMD_GP_UserTransferRecordResult));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserTransferRecordFinish, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_RECORD_FINISH);

        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_SUCCESS);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_INSURE_FAILURE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_USER_SERVICE, (ushort)USER_SERVICE_SUB.SUB_GP_USER_TRANSFER_RECORD_RESULT);
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
        private void OnUserTransferRecordResult(System.Object Target)
        {
            CMD_GP_UserTransferRecordResult Obj = (CMD_GP_UserTransferRecordResult)Target;
            transferRecordDict.Add(Obj.dwRecordID, Obj);
        }
        private void OnUserTransferRecordFinish(System.Object Target)
        {
            this.Finish();
        }
    }
}
