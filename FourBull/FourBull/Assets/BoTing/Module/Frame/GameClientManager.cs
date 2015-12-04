using System.Collections.Generic;
using BoTing.Framework;

public class GameClientManager
 {

    #region 单例

    private volatile static GameClientManager instance = null;
    private static readonly object locker = new object();
    public static GameClientManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new GameClientManager();
                    }
                }
            }
            return instance;
        }
    }

    #endregion

    private Dictionary<string, GameClient> gameClientDic = new Dictionary<string, GameClient>();

    private GameClientManager()
    {
        Initial();
    }

    public void Initial() { }

    public void RegisterClient(string name, GameClient gameClient)
    {
        if (gameClientDic.ContainsKey(name) || string.IsNullOrEmpty(name))
        {
            DebugKit.LogError("GameClientManager RegisterClient Error!");
            return;
        }
        gameClientDic.Add(name, gameClient);
    }

    /// <summary>
    /// 取回 GameClient
    /// </summary>
    public GameClient GetGameClient(string name)
    {
        if (!gameClientDic.ContainsKey(name))
        {
             DebugKit.LogError(name + " GameClient is not Exist!");
            return null;
        }
        return gameClientDic[name];
    }
    /// <summary>
    /// 移除 GameClient (注意： GameClient 是否要做相应的回收处理？)
    /// </summary>
    public void Remove(string name)
    {
        if (gameClientDic.ContainsKey(name))
        {
            gameClientDic[name].Dispose();
            gameClientDic.Remove(name);
        }
    }
}