using System;
using System.Collections.Generic;
using BoTing.Framework;

namespace BoTing.GamePublic
{
    public class TaskExecute
    {
        private Queue<BaseTask> mTaskQueue = new Queue<BaseTask>();
        private BaseTask mCurrentTask = null;
        private bool mIsRuning = false;
        private Action<TASK_EVENT, BaseTask> mCallBack = null;
        public bool Push(BaseTask task)
        {
            System.Diagnostics.Debug.Assert(!mIsRuning);
            if (mIsRuning)
                return false;
            mTaskQueue.Enqueue(task);
            return true;
        }

        public void Execute(Action<TASK_EVENT, BaseTask> CallBack)
        {
            System.Diagnostics.Debug.Assert(!mIsRuning);
            System.Diagnostics.Debug.Assert(mTaskQueue.Count > 0);
            if (mIsRuning)
                return;
            if (mTaskQueue.Count <= 0)
                return;
            mCallBack = CallBack;
            mIsRuning = true;
            TaskLoop();
        }

        private void TaskLoop()
        {
            if (mTaskQueue.Count == 0)
            {
                mIsRuning = false;
                DebugKit.Log("Task Complete");
                mCallBack(TASK_EVENT.TASK_COMPLETE, mCurrentTask);
                return;
            }
            mCurrentTask = mTaskQueue.Dequeue();
            mCurrentTask.HandleTaskEvent += HandleTaskEvent;
            mCurrentTask.Execute();
        }
        private void HandleTaskEvent(BaseTask Target, TASK_EVENT ev)
        {
            switch (ev)
            {
                case TASK_EVENT.TASK_EXECUTE:
                    mCallBack(ev, Target);
                    Target.OnExecute();
                    DebugKit.Log("Execute Task:" + Target.Description);
                    break;
                case TASK_EVENT.TASK_FINISH:
                    mCallBack(ev, Target);
                    Target.HandleTaskEvent -= HandleTaskEvent;
                    DebugKit.Log("Finish Task:" + Target.Description);
                    Target.OnFinish();
                    TaskLoop();
                    break;
                case TASK_EVENT.TASK_CANCEL:
                    mIsRuning = false;
                    Target.HandleTaskEvent -= HandleTaskEvent;
                    Target.OnCancel();
                    OnCancel();
                    DebugKit.Log("Cancel Task:" + Target.Description + " Message:" + Target.Message);
                    mCallBack(ev, Target);
                    break;
            }
        }

        private void OnCancel()
        {
            while (mTaskQueue.Count > 0)
                mTaskQueue.Dequeue();
        }
    }
}
