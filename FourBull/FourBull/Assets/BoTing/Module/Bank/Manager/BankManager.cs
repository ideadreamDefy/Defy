using BoTing.Framework;
using BoTing.GamePublic;
using System.Collections.Generic;
using TransferRecord = BoTing.Module.CMD_GP_UserTransferRecordResult;

namespace BoTing.Module
{

    public enum ENUM_BANK_EVENT
    {
        BANK_LOGIN_EVENT,
        QUERY_GAME_INSURE_EVENT,
        QUERY_USER_INFO_EVENT,
        QUERY_TRANSFER_RECORD_EVENT,
        QUERY_INSURE_EVENT,
        MODIFY_INSUREPASS_EVENT,
        SAFE_SCORE_EVENT,
        TRANSFER_SCORE_EVENT,
        TAKE_SCORE_EVENT,
    }
    public class BankEvent
    {
        public bool success;
        public string message;
        public ENUM_BANK_EVENT ev;
        public BankEvent(bool success, ENUM_BANK_EVENT ev, string message = "")
        {
            this.success = success;
            this.message = message;
            this.ev = ev;
        }
    }

    public class BankManager
    {
        public delegate void BankEventHandler(BankEvent ev);
        public event BankEventHandler OnBankEvent;
        private static BankManager gBankManager;
        private ISocketHander skSocketHdr = NetWorkManager.Instance.CreateSocket();
        private Dictionary<uint, TransferRecord> transferRecordDict = new Dictionary<uint, TransferRecord>();
        private long mUserInsure = 0;
        private long mUserScore = 0;
        internal long UserInsure { get { return mUserInsure; } }
        internal long UserScore { get { return mUserScore; } }
        internal TransferRecord[] TransferRecords 
        { 
            get 
            {                
                TransferRecord[] array = new TransferRecord[transferRecordDict.Count];
                transferRecordDict.Values.CopyTo(array,0);
                return array;
            } 
        }
        static public BankManager Instance
        {
            get
            {
                if (gBankManager == null)
                    gBankManager = new BankManager();
                return gBankManager;
            }
        }

        public void Update()
        {
            if (skSocketHdr != null)
                skSocketHdr.Update();
        }
        public void Login(string pwdBank)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new BankLoginTask(skSocketHdr, pwdBank));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.BANK_LOGIN_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.BANK_LOGIN_EVENT, task.Message));
                }
            });
        }

        public void QueryUserInfo(string nickName)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new QueryUserInfoTask(skSocketHdr, nickName, false));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.QUERY_USER_INFO_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.QUERY_USER_INFO_EVENT, task.Message));
                }
            });
        }

        public void QueryInsure()
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new QueryInsureTask(skSocketHdr));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    this.mUserInsure = ((QueryInsureTask)task).UserInsure;
                    this.mUserScore = ((QueryInsureTask)task).UserScore;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.QUERY_INSURE_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.QUERY_INSURE_EVENT, task.Message));
                }
            });
        }
        public void QueryGameInsure(ushort gameKindID)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new QueryGameInsureTask(skSocketHdr, gameKindID));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    this.mUserInsure = ((QueryGameInsureTask)task).UserInsure;
                    this.mUserScore = ((QueryGameInsureTask)task).UserScore;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.QUERY_GAME_INSURE_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.QUERY_GAME_INSURE_EVENT, task.Message));
                }
            });
        }
        public void QueryTransferRecord()
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new QueryTransferRecordTask(skSocketHdr));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    foreach (var item in ((QueryTransferRecordTask)task).transferRecordDict)
                        this.transferRecordDict[item.Key] = item.Value;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.QUERY_TRANSFER_RECORD_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.QUERY_TRANSFER_RECORD_EVENT, task.Message));
                }
            });
        }
        public void SafeScore(long score, ushort gameKindID)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new SafeScoreTask(skSocketHdr, score, gameKindID));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    this.mUserInsure = ((SafeScoreTask)task).UserInsure;
                    this.mUserScore = ((SafeScoreTask)task).UserScore;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.SAFE_SCORE_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.SAFE_SCORE_EVENT, task.Message));
                }
            });
        }
        public void TakeScore(long score, ushort gameKindID)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new TakeScoreTask(skSocketHdr, score, gameKindID));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    this.mUserInsure = ((TakeScoreTask)task).UserInsure;
                    this.mUserScore = ((TakeScoreTask)task).UserScore;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.TAKE_SCORE_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.TAKE_SCORE_EVENT, task.Message));
                }
            });
        }
        public void TransferScore(long score, string nickName, bool isNickname)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new TransScoreTask(skSocketHdr, score, nickName, isNickname));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    this.mUserInsure = ((TransScoreTask)task).UserInsure;
                    this.mUserScore = ((TransScoreTask)task).UserScore;
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.TRANSFER_SCORE_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.TRANSFER_SCORE_EVENT, task.Message));
                }
            });
        }
        public void ModifyInsurePass(string oldPassword, string newPassword)
        {
            TaskExecute execute = new TaskExecute();
            if (!skSocketHdr.IsValid())
                execute.Push(new HallConnectTask(skSocketHdr, UserCenter.Instance.LoginAddress, UserCenter.Instance.LoginPort));
            execute.Push(new ModifyInsurePassTask(skSocketHdr, oldPassword, newPassword));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(true, ENUM_BANK_EVENT.MODIFY_INSUREPASS_EVENT));
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnBankEvent != null)
                        OnBankEvent(new BankEvent(false, ENUM_BANK_EVENT.MODIFY_INSUREPASS_EVENT, task.Message));
                }
            });
        }
    }
}
