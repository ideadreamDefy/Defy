using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class HallConnectTask : BaseTask
    {
        private string mIPAddress;
        private ushort mPort;
        private ISocketHander socketHdr = null;
        public HallConnectTask(ISocketHander socketHdr, string IPAddress, ushort Port)
        {
            this.socketHdr = socketHdr;
            mIPAddress = IPAddress;
            mPort = Port;
            Description = "正在连接到登录服务器.";
        }
        override public void OnExecute()
        {
            Init();
            socketHdr.Connect(mIPAddress, mPort);
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CONNECT, OnConnectEvent);
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE, OnCloseEvent);
        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CONNECT);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr = null;
        }
        public void OnConnectEvent(System.Object Target)
        {
            if (socketHdr.IsValid())
            {
                this.Finish();
            }
            else
            {
                this.Message = "连接到服务器失败";
                this.Cancel();
            }

        }
        public void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接断开";
            this.Cancel();
        }
    }
}
