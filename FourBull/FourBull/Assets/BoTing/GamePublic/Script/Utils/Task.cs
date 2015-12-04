using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BoTing.GamePublic
{
    public enum TASK_EVENT
    {
        TASK_EXECUTE,
        TASK_FINISH,
        TASK_CANCEL,
        TASK_COMPLETE,
    }
    public class BaseTask
    {
        #region 委托方法相关定义
        public delegate void TaskEventHandler(BaseTask Target, TASK_EVENT ev);
        //任务事件
        public event TaskEventHandler HandleTaskEvent;
        #endregion

        //任务执行接口
        virtual public void OnExecute()
        {
           
        }

        virtual public void OnFinish()
        {

        }

        virtual public void OnCancel()
        {

        }

        public void Execute()
        {
            if (HandleTaskEvent != null)
                HandleTaskEvent(this, TASK_EVENT.TASK_EXECUTE);
        }
        //任务执行完成处理
        protected void Finish()
        {
            if (HandleTaskEvent != null)
                HandleTaskEvent(this, TASK_EVENT.TASK_FINISH);
        }
        //任务被取消
        protected void Cancel()
        {
            if (HandleTaskEvent != null)
                HandleTaskEvent(this, TASK_EVENT.TASK_CANCEL);
        }
        //消息字符串，主要用于存放错误信息等
        public string Message
        {
            get;
            protected set;
        }
        //任务的名称
        public string Name
        {
            get;
            protected set;
        }
        //任务描述
        public string Description
        {
            get;
            protected set;
        }
    }
}
