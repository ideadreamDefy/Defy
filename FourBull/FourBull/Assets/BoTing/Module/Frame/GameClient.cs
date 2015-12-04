using System;
using BoTing.Module;
using BoTing.Framework;
using System.Collections.Generic;

/// <summary>
/// 与服务端交互实体
/// <para>(向服务器发送消息，接收服务器消息，并做相应处理或触发调度界面信息)</para>
/// </summary>
public abstract class GameClient : ViewCommand, IDisposable
{
    #region vars

    struct GameStatusObserver
    {
        public byte GameStatus;
        public Object Target;
        public Type ObjectType;
        public Action<Object> CallBack;
    }

    public const ushort InvalidChairID = 0xFFFF;
    protected byte mGameStatus = 0;
    protected byte mAllowLookon = 0;
    protected ISocketHander mSocketHander = null;
    private List<Player> allUserList = new List<Player>();
    private List<GameStatusObserver> mObserverList = new List<GameStatusObserver>();
    private List<UserEvent> mUserEvent = new List<UserEvent>();
    private Dictionary<ushort, Player> playerDic = new Dictionary<ushort, Player>();
    private Room room = null;
    private bool isViewAttached = false;
    /// <summary>
    /// 房间控制器
    /// </summary>
    public Room Room { get { return room; } }

    private IPlayer self;
    /// <summary>
    /// 玩家信息
    /// </summary>
    public IPlayer Self
    {
        get
        {
            if (self == null)
            {
                self = room.UserManager.Find(UserCenter.Instance.UserID);
            }
            return self;
        }
    }

    public string ProcessName { get { return room.ProcessName; } }
    public string ServerName { get { return room.ServerName; } }
    public string KindName { get { return room.KindName; } }
    public bool AllowLookon { get { return mAllowLookon == 1; } }
    public byte GameStatus { get { return mGameStatus; } }

    #endregion

    /// <summary>
    /// 仅在进入房间调用并初始数据
    /// </summary>
    /// <param name="room"></param>
    /// <param name="gameName"></param>
    public void OnInitRoom(IRoom iRoom)
    {
        room = (Room)iRoom;
        mSocketHander = room.SocketHander;
        room.UserManager.OnUserEvent += OnUserEvent;

        //用户命令
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameOptionEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_GAME_OPTION, typeof(CMD_GF_GameOption));

        // ==== TODO: 比赛服用消息，暂不处理 ====
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserReadyEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_USER_READY);
        //聊天命令
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserChatEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_USER_CHAT, typeof(CMD_GF_S_UserChat));
        // 用户异常
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserExpressiontEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_USER_EXPRESSION, typeof(CMD_GF_S_UserExpression));
        //游戏信息
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnGameStatusEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_GAME_STATUS, typeof(CMD_GF_GameStatus));
        // 游戏场景消息
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnSceneEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_GAME_SCENE);
        // 观牌状态
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnLookonStatusEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_LOOKON_STATUS, typeof(CMD_GF_LookonStatus));
        //系统消息
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnSystemMessageEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_SYSTEM_MESSAGE, typeof(CMD_CM_SystemMessage));
        // Action Message
        mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnActionMessageEvent,
                                        (ushort)MAIN_CMD.MDM_GF_FRAME,
                                        (ushort)FRAME_SUB_CMD.SUB_GF_ACTION_MESSAGE, typeof(CMD_CM_ActionMessage));

        //用户自己掉线回来
        if (Self.UserStatus == (byte)ENUM_USER_STATUS.US_PLAYING)
        {
            this.CreateGameClient(true);
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        room.UserManager.OnUserEvent -= OnUserEvent;

        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_GAME_OPTION);
        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_READY);

        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_CHAT);
        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_EXPRESSION);

        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_GAME_STATUS);
        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_GAME_SCENE);
        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_LOOKON_STATUS);

        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_SYSTEM_MESSAGE);
        mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_ACTION_MESSAGE);
        room = null;
        mSocketHander = null;

        mObserverList.Clear();
        mObserverList = null;

        mUserEvent.Clear();
        mUserEvent = null;
    }

    /// <summary>
    /// 监听消息,网络消息
    /// </summary>
    public bool AddGameListener(object target, Action<object> CallBack, ushort wSubCmdID, Type type = null)
    {
        if (mSocketHander == null)
            return false;
        return mSocketHander.AddListener(target, ENUM_SOCKET_EVENT.EVENT_MESSAGE, CallBack, (ushort)MAIN_CMD.MDM_GF_GAME, wSubCmdID, type);
    }
    /// <summary>
    /// 移除监听的消息
    /// </summary>
    public bool RemoveGameListener(object target, ushort wSubCmdID)
    {
        if (mSocketHander == null)
            return false;
        return mSocketHander.RemoveListener(target, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GF_GAME, wSubCmdID);
    }
    /// <summary>
    /// 添加场景消息监听
    /// </summary>
    /// <param name="target"></param>
    /// <param name="callBack"></param>
    /// <param name="gameStatus"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool AddSceneListener(object target, Action<object> callBack, byte gameStatus, Type type)
    {
        for (int i = 0; i < mObserverList.Count; ++i)
        {
            if (mObserverList[i].Target == target && mObserverList[i].GameStatus == gameStatus)
                throw new Exception("重复添消息观察者,确认代码没有问题？");
        }
        GameStatusObserver Observer = new GameStatusObserver();
        Observer.Target = target;
        Observer.CallBack = callBack;
        Observer.GameStatus = gameStatus;
        Observer.ObjectType = type;
        mObserverList.Add(Observer);
        return true;
    }

    public override void OnAttachedView()
    {
        base.OnAttachedView();
        this.isViewAttached = true;
        room.SetOption(0, 16777217, 16777217);
        //初始化完成了,必须先补上未处理的玩家事件
        for(int i=0; i < mUserEvent.Count; i++)
            this.OnUserEvent(mUserEvent[i]);
        mUserEvent.Clear();
    }

    public override void OnReleasedView()
    {
        base.OnReleasedView();
        this.isViewAttached = false;
        mUserEvent.Clear();
    }
    /// <summary>
    /// 移除场景监听
    /// </summary>
    /// <param name="target"></param>
    /// <param name="gameStatus"></param>
    /// <returns></returns>
    public bool RemoveSceneListener(object target, byte gameStatus)
    {
        for (int i = 0; i < mObserverList.Count; ++i)
        {
            if (mObserverList[i].Target == target && mObserverList[i].GameStatus == gameStatus)
            {
                mObserverList.RemoveAt(i);
                break;
            }
        }
        return true;
    }
    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    public void SendGamePacket<T>(ushort wSubCmdID, T t)
    {
        if (mSocketHander != null && mSocketHander.IsValid())
        {
            if (t != null)
                mSocketHander.SendPacket<T>((ushort)MAIN_CMD.MDM_GF_GAME, wSubCmdID, t);
            else
                mSocketHander.SendData((ushort)MAIN_CMD.MDM_GF_GAME, wSubCmdID);
        }
    }
    /// <summary>
    /// 向服务器发送准备开始
    /// </summary>
    public void SendUserReady()
    {
        if (mSocketHander != null && mSocketHander.IsValid())
        {
            mSocketHander.SendData((ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_READY);
        }
    }
    /// <summary>
    /// 向服务器发送离开房间消息
    /// </summary>
    /// <param name="forceLeave">是否强退</param>
    public void SendUserLeave(bool forceLeave)
    {
        System.Diagnostics.Debug.Assert(room != null);
        if (room != null)
            room.PrefromStandUp(forceLeave ? (byte)1 : (byte)0);
    }
    /// <summary>
    /// 向服务器发送表情
    /// </summary>
    public void SendUserExpression(uint TargetUserID, ushort ItemIndex)
    {
        if (mSocketHander != null && mSocketHander.IsValid())
        {
            CMD_GF_C_UserExpression UserExpression = new CMD_GF_C_UserExpression();
            UserExpression.wItemIndex = ItemIndex;
            UserExpression.dwTargetUserID = TargetUserID;
            mSocketHander.SendPacket<CMD_GF_C_UserExpression>((ushort)MAIN_CMD.MDM_GF_GAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_EXPRESSION, UserExpression);
        }
    }
    /// <summary>
    /// 向服务器发送聊天信息
    /// </summary>
    public void SendUserChat(uint TargetUserID, string chatText, uint color)
    {
        if (mSocketHander != null && mSocketHander.IsValid())
        {
            //构造信息
            CMD_GF_C_UserChat UserChat = new CMD_GF_C_UserChat();
            UserChat.szChatString = chatText;
            //构造数据
            UserChat.dwChatColor = color;
            UserChat.dwTargetUserID = TargetUserID;
            mSocketHander.SendPacket<CMD_GF_C_UserChat>((ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_USER_CHAT, UserChat);
        }
    }
    /// <summary>
    /// 向服务器是否允许某个旁观用户看牌？
    /// </summary>
    public bool SendUserLookon(uint UserID, bool bAllowLookon)
    {
        if (mSocketHander == null || !mSocketHander.IsValid())
            return false;
        //发送消息
        if (IsLookonMode() == false)
        {
            //变量定义
            CMD_GF_LookonConfig LookonConfig = new CMD_GF_LookonConfig();

            //设置变量
            LookonConfig.dwUserID = UserID;
            LookonConfig.cbAllowLookon = (byte)((bAllowLookon == true) ? 1 : 0);

            //发送数据
            mSocketHander.SendPacket<CMD_GF_LookonConfig>((ushort)MAIN_CMD.MDM_GF_FRAME, (ushort)FRAME_SUB_CMD.SUB_GF_LOOKON_CONFIG, LookonConfig);
        }
        return true;
    }
    /// <summary>
    /// 查询指定类型玩家 eg. 同桌玩家、旁观者 etc.
    /// <para>默认包括旁观的玩家都会取出</para>
    /// <para>只取出 旁观者 ENUM_USER_STATUS.US_LOOKON</para>
    /// <para>只取出 同桌玩家 ENUM_USER_STATUS.US_SIT</para>
    /// </summary>
    public void GetPlayerList(Action<IPlayer> callback, ENUM_USER_STATUS status = ENUM_USER_STATUS.US_NULL)
    {
        System.Diagnostics.Debug.Assert(callback != null);
        for (int i = 0; i < allUserList.Count; ++i)
        {
            if (allUserList[i].UserStatus != (byte)status && status != ENUM_USER_STATUS.US_NULL)
                continue;
            if (callback != null)
                callback(allUserList[i]);
        }
    }
    /// <summary>
    /// 通过 UserID 查询游戏玩家
    /// </summary>
    /// <param name="dwUserID">UserID</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetPlayerByUserID(uint dwUserID)
    {
        return allUserList.Find(f => { return f.UserID == dwUserID; });
    }
    /// <summary>
    /// 通过客户端座位编号查询游戏玩家
    /// <para>返回值可能为 null </para>
    /// </summary>
    /// <param name="wChairID">玩家在桌位编号，（客户端编号）</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetPlayerByClientChairID(ushort wChairID)
    {
        if (playerDic.ContainsKey(wChairID))
        {
            return playerDic[wChairID];
        }
        return null;
    }
    /// <summary>
    /// 通过服务端座位编号查询游戏玩家
    /// <para>返回值可能为 null </para>
    /// </summary>
    /// <param name="wChairID">玩家在桌位编号，（服务端编号）</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetPlayerByServerChairID(ushort wChairID)
    {
        ushort ClientChairID = SwitchViewChairID(wChairID);
        if (ClientChairID == InvalidChairID)
            return null;
        if (playerDic.ContainsKey(ClientChairID))
            return playerDic[ClientChairID];
        return null;
    }

    /// <summary>
    /// 通过客户端座位编号查询旁观玩家
    /// <para>返回值可能为 null </para>
    /// </summary>
    /// <param name="wChairID">玩家在桌位编号，（客户端编号）</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetLookOnByClientChairID(ushort wChairID)
    {
        return null;
    }
    /// <summary>
    /// 通过 GameID 查询游戏玩家
    /// </summary>
    /// <param name="gameID">GameID</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetPlayerByGameID(uint gameID)
    {
        return allUserList.Find(f => { return f.GameID == gameID; });
    }

    /// <summary>
    /// 通过 NickName 查询游戏玩家
    /// </summary>
    /// <param name="NickName">昵称</param>
    /// <returns>IPlayer</returns>
    public IPlayer GetPlayerByNickName(string nickName)
    {
        return allUserList.Find(f => { return f.NickName == nickName; });
    }
    /// <summary>
    /// 转换客户端座位号到服务端坐位号
    /// </summary>
    public ushort SwitchViewChairID(ushort serverChairID)
    {
        //参数判断
        if (serverChairID == InvalidChairID)
            return InvalidChairID;
        if (Self == null)
            return InvalidChairID;
        if (Self.ChairID == InvalidChairID)
            return InvalidChairID;

        //转换椅子
        ushort wChairCount = room.ChairCount;
        ushort wViewChairID = (ushort)((serverChairID - Self.ChairID + wChairCount) % wChairCount);
        return wViewChairID;
    }
    /// <summary>
    /// 向服务器是否是旁观模式
    /// </summary>
    public bool IsLookonMode()
    {
        if (Self == null)
            return true;
        return (Self.UserStatus == (byte)ENUM_USER_STATUS.US_LOOKON);
    }


    #region S -> C abstract

    /// <summary>
    /// 初始化游戏客户端，此时所有的基础数据已经初始化完成，开发者必须在此方法中创建并显示View
    /// </summary>
    abstract protected void OnGameClientReady();
    /// <summary>
    /// 匹配OnGameClientReady，此时游戏玩家本人已经离开，游戏界面View必须关闭
    /// </summary>
    abstract protected void OnGameClientClose();

    /// <summary>
    /// 玩家进入游戏场景
    /// </summary>
    /// <param name="player"></param>
    abstract protected void OnPlayerEnter(IPlayer player, ushort chairID, bool isLookOn);
    /// <summary>
    /// 玩家离开游戏场景
    /// </summary>
    /// <param name="player"></param>
    abstract protected void OnPlayerLeave(IPlayer player, ushort chairID, bool isLookOn);

    /// <summary>
    /// 玩家财富信息变更
    /// </summary>
    /// <param name="player"></param>
    abstract protected void OnScoreChange(IPlayer player, ushort chairID);

    /// <summary>
    /// 玩家准备事件
    /// </summary>
    abstract protected void OnPlayerReady(IPlayer target, ushort chairID);

    /// <summary>
    /// 用户开始游戏 (不一定是玩家触发游戏开始，有游戏准备的小游戏就不要实现该接口内容)
    /// </summary>
    abstract protected void OnPlayerGameStart(IPlayer player);

    /// <summary>
    /// 用户结束游戏 (同 OnPlayerGameStart，可能玩家放弃游戏但未离开房间)
    /// </summary>
    abstract protected void OnPlayerGameOver(IPlayer player);

    /// <summary>
    /// 用户掉线、掉线重链
    /// </summary>
    abstract protected void OnPlayerOffline(IPlayer player, ushort chairID, bool isBack);



    #endregion

    #region S -> C virtual

    /// <summary>
    /// 游戏选项事件
    /// </summary>
    protected virtual void OnGameOptionEvent(object target) { }

    /// <summary>
    /// ================ TODO: 比赛服用消息，暂不处理 ================
    /// </summary>
    protected virtual void OnUserReadyEvent(object obj) { }

    /// <summary>
    /// 聊天消息
    /// </summary>
    protected virtual void OnUserChatEvent(object target) { }

    /// <summary>
    /// ???
    /// </summary>
    protected virtual void OnUserExpressiontEvent(object target) { }

    /// <summary>
    /// 旁观变更事件
    /// </summary>
    protected virtual void OnLookonStatusEvent(object target) { }

    /// <summary>
    /// 系统消息
    /// </summary>
    protected virtual void OnSystemMessageEvent(object target) { }

    /// <summary>
    /// ???
    /// </summary>
    protected virtual void OnActionMessageEvent(object target) { }

    #endregion


    /// <summary>
    /// 游戏状态变更
    /// </summary>
    private void OnGameStatusEvent(object target)
    {
        CMD_GF_GameStatus gs = (CMD_GF_GameStatus)target;
        mGameStatus = gs.cbGameStatus;
        mAllowLookon = gs.cbAllowLookon;
    }

    private void CreateGameClient(bool bCutBack = false)
    {
        DebugKit.Todo("GameClient.OnuserEvent: 根据配表初始游戏是否可以旁观及游戏版本");
        List<Player> playerList = room.UserManager.FindByTableID(Self.TableID);
        for (int i = 0; i < playerList.Count; i++)
        {

            if (Self.UserID == playerList[i].UserID && !bCutBack)
                continue;
            allUserList.Add(playerList[i]);
            if (playerList[i].UserStatus != (byte)ENUM_USER_STATUS.US_LOOKON)
                playerDic[SwitchViewChairID(playerList[i].ChairID)] = playerList[i];

        }
        OnGameClientReady();//开始创建界面
    }
    /// <summary>
    /// S -> C
    /// </summary>
    private void OnUserEvent(UserEvent ev)
    {
        //事情没有发生在本人所在的房间内
        if (ev.dwUserID != Self.UserID && ev.statusAfter.wTableID != Self.TableID && ev.statusBefore.wTableID != Self.TableID)
        {
            return;
        }
        //缓存未初始化view状态下的用户事件
        if (!this.isViewAttached)
        {
            mUserEvent.Add(ev);
            //非进入消息情况下直接pass
            if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_FREE 
                && ev.statusAfter.cbUserStatus >= (byte)ENUM_USER_STATUS.US_SIT
                && ev.dwUserID == Self.UserID)
            {
                CreateGameClient();
            }
            return;
        }

        if (!ev.bScoreChange)
        {
            Player player = room.UserManager.Find(ev.dwUserID);
            //用户进入房间
            if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_FREE && ev.statusAfter.cbUserStatus >= (byte)ENUM_USER_STATUS.US_SIT)
            {
                //压玩家信息入列表
                if (player.UserStatus != (byte)ENUM_USER_STATUS.US_LOOKON && this.isViewAttached)
                {
                    playerDic[SwitchViewChairID(player.ChairID)] = player;
                }
                allUserList.Add(player);
                OnPlayerEnter(player, SwitchViewChairID(ev.statusAfter.wChairID), ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_LOOKON);
            }

            //用户离开房间
            if (ev.statusBefore.cbUserStatus >= (byte)ENUM_USER_STATUS.US_SIT && ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_FREE)
            {
                OnPlayerLeave(player, SwitchViewChairID(ev.statusBefore.wChairID), ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_LOOKON);
                if (ev.dwUserID == Self.UserID)
                {
                    OnGameClientClose();
                }
                //删除玩家信息
                allUserList.RemoveAll(f => { return f.UserID == ev.dwUserID; });
                //删除玩家列表
                if (ev.statusBefore.cbUserStatus != (byte)ENUM_USER_STATUS.US_LOOKON)
                    playerDic[SwitchViewChairID(ev.statusBefore.wChairID)] = null;
            }

            if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_SIT && ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_READY)
            {
                OnPlayerReady(player, SwitchViewChairID(ev.statusBefore.wChairID));
            }
            ////用户开始游戏
            //if (ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_PLAYING)
            //{
            //    OnPlayerGameStart(player);
            //}
            ////用户结束游戏
            //if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_PLAYING && ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_SIT)
            //{
            //    OnPlayerGameOver(player);
            //}
            // 掉线
            if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_PLAYING && ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_OFFLINE)
            {
                OnPlayerOffline(player, SwitchViewChairID(ev.statusBefore.wChairID), false);
            }
            // 掉线重链
            if (ev.statusBefore.cbUserStatus == (byte)ENUM_USER_STATUS.US_OFFLINE && ev.statusAfter.cbUserStatus == (byte)ENUM_USER_STATUS.US_PLAYING)
            {
                OnPlayerOffline(player, SwitchViewChairID(ev.statusBefore.wChairID), true);
            }
        }
        else //用户积分信息变更
        {
            Player player = allUserList.Find(f => { return f.UserID == ev.dwUserID; });
            if (player != null)
                OnScoreChange(player, SwitchViewChairID(player.ChairID));
        }
    }

    /// <summary>
    /// 游戏场景事件(根据游戏当前状态做相应处理)
    /// </summary>
    private void OnSceneEvent(object target)
    {
        byte[] buffer = (byte[])target;
        bool bInvoke = false;
        for (int i = 0; i < mObserverList.Count; ++i)
        {
            if (mObserverList[i].GameStatus == this.GameStatus)
            {
                Object Packet = PacketHelper.BytesToStruct(buffer, buffer.Length, mObserverList[i].ObjectType);
                mObserverList[i].CallBack.Invoke(Packet);
                bInvoke = true;
            }
        }
        if (!bInvoke)
            DebugKit.LogWarning("未处理的场景状态:" + this.GameStatus.ToString());
    }
}