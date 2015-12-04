using BoTing.Framework;
using BoTing.GamePublic;

namespace BoTing.Module
{
    public class HallGetServerTask : BaseTask
    {
        private ushort[] mGameKinds;
        private ISocketHander socketHdr = null;
        public HallGetServerTask(ISocketHander socketHdr,ushort[] GameKinds)
        {
            this.socketHdr = socketHdr;
            Description = "正在获取游戏列表.";
            mGameKinds = GameKinds;
        }
        override public void OnExecute()
        {
            Init();
            byte[] Kind = new byte[GameList.GameKinds.Count * sizeof(ushort)];
            int nCount = 0;
            for (int i = 0; i < GameList.GameKinds.Count; ++i)
            {
                if (mGameKinds != null)
                {
                    bool bFind = false;
                    for (int j = 0; j < mGameKinds.Length; j++)
                    {
                        if (mGameKinds[j] == GameList.GameKinds[i].wKindID)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (!bFind)
                        continue;
                }
                Kind[nCount * 2] = (byte)(GameList.GameKinds[i].wKindID & 0x0ff);
                Kind[nCount * 2 + 1] = (byte)((GameList.GameKinds[i].wKindID >> 8) & 0x0ff);
                nCount++;
            }
            //
            ushort size = socketHdr.SendData((ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_GET_SERVER, Kind, (ushort)(sizeof(ushort) * nCount));
            DebugKit.Log("发送游戏列表请求 send:" + size.ToString() + " count:" + nCount.ToString()); 
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
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameNode, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_NODE, typeof(CMD_GP_GameNode));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGamePage, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_PAGE, typeof(CMD_GP_GamePage));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameServer, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_SERVER, typeof(CMD_GP_GameServer));
            socketHdr.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnServerFinish, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_SERVER_FINISH);

        }
        private void UnInit()
        {
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_CLOSE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_NODE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_PAGE);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_LIST_SERVER);
            socketHdr.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GP_SERVER_LIST, (ushort)SERVER_LIST_SUB_CMD.SUB_GP_SERVER_FINISH);
            socketHdr = null;
        }
        public void OnCloseEvent(System.Object Target)
        {
            this.Message = "与服务器的连接件断开";
            this.Cancel();
        }
        public void OnGamePage(System.Object Target)
        {
            DebugKit.Log("OnGamePage:" + Target.ToString());
            CMD_GP_GamePage Obj = (CMD_GP_GamePage)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GamePages.Add(item);
            }
        }
        public void OnGameNode(System.Object Target)
        {
            DebugKit.Log("OnGameNode:" + Target.ToString());
            CMD_GP_GameNode Obj = (CMD_GP_GameNode)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameNodes.Add(item);
            }
        }
        public void OnGameServer(System.Object Target)
        {
            DebugKit.Log("OnGameNode:" + Target.ToString());
            CMD_GP_GameServer Obj = (CMD_GP_GameServer)Target;
            foreach (var item in Obj.ItemArray)
            {
                GameList.GameServers.Add(item);
            }
        }
        public void OnServerFinish(System.Object Target)
        {
            DebugKit.Log("OnServerFinish:");
            this.Finish();
        }
    }
}
