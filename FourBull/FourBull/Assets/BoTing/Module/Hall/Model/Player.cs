using BoTing.Framework;
using System;

namespace BoTing.Module
{
    public class Player : IDisposable, IPlayer
    {
        private tagUserInfo mUserInfo;
        #region 各种属性定义
        public uint UserID
        {
            get { return mUserInfo.dwUserID; }
        }
        public uint GameID
        {
            get { return mUserInfo.dwGameID; }
        }
        public string NickName
        {
            get { return mUserInfo.szNickName; }
        }
        public ushort TableID
        {
            get { return mUserInfo.wTableID; }
            set { mUserInfo.wTableID = value; }
        }
        public ushort ChairID
        {
            get { return mUserInfo.wChairID; }
            set { mUserInfo.wChairID = value; }
        }
        public byte UserStatus
        {
            get { return mUserInfo.cbUserStatus; }
            set { mUserInfo.cbUserStatus = value; }
        }
        public int LoveLiness
        {
            get { return mUserInfo.lLoveLiness; }
            set { mUserInfo.lLoveLiness = value; }
        }
        public uint Experience
        {
            get { return mUserInfo.dwExperience; }
            set { mUserInfo.dwExperience = value; }
        }
        public uint UserMedal
        {
            get { return mUserInfo.dwUserMedal; }
            set { mUserInfo.dwUserMedal = value; }
        }
        public uint FleeCount
        {
            get { return mUserInfo.dwFleeCount; }
            set { mUserInfo.dwFleeCount = value; }
        }
        public uint DrawCount
        {
            get { return mUserInfo.dwDrawCount; }
            set { mUserInfo.dwDrawCount = value; }
        }
        public uint LostCount
        {
            get { return mUserInfo.dwLostCount; }
            set { mUserInfo.dwLostCount = value; }
        }
        public uint WinCount
        {
            get { return mUserInfo.dwWinCount; }
            set { mUserInfo.dwWinCount = value; }
        }
        public long Insure
        {
            get { return mUserInfo.lInsure; }
            set { mUserInfo.lInsure = value; }
        }
        public long Grade
        {
            get { return mUserInfo.lGrade; }
            set { mUserInfo.lGrade = value; }
        }
        public long Score
        {
            get { return mUserInfo.lScore; }
            set { mUserInfo.lScore = value; }
        }

        public uint UserRight
        {
            get;
            set;
        }
        public uint MasterRight
        {
            get;
            set;
        }
        #endregion
        public Player(tagUserInfo UserInfo)
        {
            mUserInfo = UserInfo;
        }
        public void Dispose()
        {
            mUserInfo = null;
        }
    }
}
