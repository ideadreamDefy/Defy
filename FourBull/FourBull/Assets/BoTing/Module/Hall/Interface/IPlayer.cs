
namespace BoTing.Module
{
    public enum ENUM_USER_STATUS
    {
        US_NULL = 0x00,								//没有状态
        US_FREE = 0x01,								//站立状态
        US_SIT = 0x02,							    //坐下状态
        US_READY = 0x03,							//同意状态
        US_LOOKON = 0x04,							//旁观状态
        US_PLAYING = 0x05,							//游戏状态
        US_OFFLINE = 0x06,							//断线状态
    }
    public interface IPlayer
    {
        uint UserID
        {
            get;
        }
        uint GameID
        {
            get;
        }
        string NickName
        {
            get;
        }
        ushort TableID
        {
            get;
        }
        ushort ChairID
        {
            get;
        }
        byte UserStatus
        {
            get;
        }
        int LoveLiness
        {
            get;
        }
        uint Experience
        {
            get;
        }
        uint UserMedal
        {
            get;
        }
        uint FleeCount
        {
            get;
        }
        uint DrawCount
        {
            get;
        }
        uint LostCount
        {
            get;
        }
        uint WinCount
        {
            get;
        }
        long Insure
        {
            get;
        }
        long Grade
        {
            get;
        }
        //积分,
        long Score
        {
            get;
        }
        uint UserRight
        {
            get;
        }
        uint MasterRight
        {
            get;
        }
    }
}
