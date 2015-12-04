using BoTing.Framework;
using BoTing.GamePublic;
using System;

namespace BoTing.Module
{
    public enum ENUM_LOGIN_EVENT
    {
        LOGIN_FAIL,
        LOGIN_SUCCESS,
    }
    public class LoginManager : IDisposable
    {
        private ISocketHander skHall = NetWorkManager.Instance.CreateSocket();
        static private LoginManager gLoginManager;
        public delegate void LoginEventHandler(ENUM_LOGIN_EVENT ev, string message);
        public event LoginEventHandler OnLoginEvent;
        private class SingletoneEnter { };
        static public LoginManager Instance
        {
            get
            {
                if (gLoginManager == null)
                    gLoginManager = new LoginManager(new SingletoneEnter());
                return gLoginManager;
            }
        }
        private LoginManager(SingletoneEnter enter)
        {

        }
        public void Dispose()
        {
            if (skHall != null)
                skHall.Shutdown();
            skHall = null;
        }

        public void Login(string strAccount, string strPwd,ushort[] gameKinds = null)
        {
            TaskExecute execute = new TaskExecute();
            if (!skHall.IsValid())
                execute.Push(new HallConnectTask(skHall,"127.0.0.1", 7701));
            execute.Push(new HallLoginTask(skHall,strAccount, strPwd));
            execute.Push(new HallGetServerTask(skHall,gameKinds));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                if (TASK_EVENT.TASK_COMPLETE == ev)
                {
                    if (OnLoginEvent != null)
                        OnLoginEvent(ENUM_LOGIN_EVENT.LOGIN_SUCCESS,"OOXX上吧");
                    DebugKit.Log("Execute CallBack: " + ev.ToString());
                }
                if (TASK_EVENT.TASK_CANCEL == ev)
                {
                    if (OnLoginEvent != null)
                        OnLoginEvent(ENUM_LOGIN_EVENT.LOGIN_FAIL, task.Message);
                    DebugKit.Log("Execute CallBack: " + ev.ToString());
                }
            });
        }
       
    }
}
