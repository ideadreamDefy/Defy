using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    internal class WaitRoomCloseTask : BaseTask
    {
        private Room mRoom;
        private ISocketHander mSocketHander = null;
        public WaitRoomCloseTask(Room room)
        {
            mRoom = room;
            Description = "等待游戏房间关闭.";
        }
        override public void OnExecute()
        {
            Init();
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
            mSocketHander = mRoom.SocketHander;
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE, OnCloseEvent);
        }
        private void UnInit()
        {
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
        }
        public void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接断开";
            this.Finish();
        }
    }
}
