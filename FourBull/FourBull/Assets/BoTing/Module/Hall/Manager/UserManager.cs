using BoTing.Framework;
using System;
using System.Collections.Generic;

namespace BoTing.Module
{
    internal struct UserEvent
    {
        public uint dwUserID;
        public bool bScoreChange;
        internal tagUserStatus statusBefore;
        public tagUserStatus statusAfter;
        public tagUserScore scoreBefore;
        public tagUserScore scoreAfter;
    }
    public class UserManager : IDisposable
    {
        private Dictionary<uint, Player> mPlayerDictionary = new Dictionary<uint, Player>();
        private Room mRoom = null;
        private ISocketHander mSocketHander = null;
        internal delegate void UserEventHandler(UserEvent ev);
        internal event UserEventHandler OnUserEvent;
        public IPlayer Self
        {
            get { return Find(UserCenter.Instance.UserID); }
        }

        public Dictionary<uint, Player> Players
        {
            get { return mPlayerDictionary;}
        }

        public UserManager(IRoom room)
        {
            mRoom = (Room)room;
            mSocketHander = mRoom.SocketHander;
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserEnterEvent, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_ENTER, typeof(tagUserInfo));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserStatusEvent, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_STATUS, typeof(CMD_GR_UserStatus));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnUserScoreEvent, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_SCORE, typeof(CMD_GR_UserScore));
        }
        public void Dispose()
        {
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_ENTER);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_REQUEST_FAILURE);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_USER, (ushort)USER_SUB_CMD.SUB_GR_USER_STATUS);
            foreach(var pair in mPlayerDictionary)
            {
                pair.Value.Dispose();
            }
            mPlayerDictionary.Clear();
            mPlayerDictionary = null;
            mSocketHander = null;
            mRoom = null;
        }
        public Player Find(uint dwUserID)
        {
            if (!mPlayerDictionary.ContainsKey(dwUserID))
                return null;
            return mPlayerDictionary[dwUserID];
        }
        public List<Player> FindByTableID(ushort tableID)
        {
            List<Player> list = new List<Player>();
            foreach (var v in mPlayerDictionary.Values)
            {
                if (v.TableID == tableID)
                {
                    list.Add(v);
                }
            }
            return list;
        }
        private void OnUserEnterEvent(System.Object Target)
        {
            tagUserInfo UserInfo = (tagUserInfo)Target;
            //判断进入房间的是不是本人
            bool bSelf = UserInfo.dwUserID == UserCenter.Instance.UserID;
            if (!mPlayerDictionary.ContainsKey(UserInfo.dwUserID))
            {
                mPlayerDictionary[UserInfo.dwUserID] = new Player(UserInfo);
                tagUserStatus statusBefore = new tagUserStatus();
                statusBefore.cbUserStatus = (byte)ENUM_USER_STATUS.US_NULL;
                tagUserStatus statusAfrer = new tagUserStatus();
                statusAfrer.cbUserStatus = UserInfo.cbUserStatus;
                if (OnUserEvent != null)
                {
                    UserEvent Event = new UserEvent();
                    Event.dwUserID = UserInfo.dwUserID;
                    Event.bScoreChange = false;
                    Event.statusBefore = statusBefore;
                    Event.statusAfter = statusAfrer;
                    OnUserEvent(Event);
                }
            }
        }
        private void OnUserLeaveEvent(uint dwUserID)
        {
            if (!mPlayerDictionary.ContainsKey(dwUserID))
                return;
            mPlayerDictionary.Remove(dwUserID);
        }
        private void OnUserStatusEvent(System.Object Target)
        {
            CMD_GR_UserStatus UserStatus = (CMD_GR_UserStatus)Target;
            if (!mPlayerDictionary.ContainsKey(UserStatus.dwUserID))
                return;
            tagUserStatus statusBefore = new tagUserStatus();
            statusBefore.cbUserStatus = mPlayerDictionary[UserStatus.dwUserID].UserStatus;
            statusBefore.wChairID = mPlayerDictionary[UserStatus.dwUserID].ChairID;
            statusBefore.wTableID = mPlayerDictionary[UserStatus.dwUserID].TableID;

            mPlayerDictionary[UserStatus.dwUserID].UserStatus = UserStatus.UserStatus.cbUserStatus;
            mPlayerDictionary[UserStatus.dwUserID].TableID = UserStatus.UserStatus.wTableID;
            mPlayerDictionary[UserStatus.dwUserID].ChairID = UserStatus.UserStatus.wChairID;

            if (OnUserEvent != null) {
                UserEvent Event = new UserEvent();
                Event.dwUserID = UserStatus.dwUserID;
                Event.bScoreChange = false;
                Event.statusBefore = statusBefore;
                Event.statusAfter = UserStatus.UserStatus;
                OnUserEvent(Event);
            }
            //用户已经离开，删除用户列表中的对象
            if ((ENUM_USER_STATUS)UserStatus.UserStatus.cbUserStatus == ENUM_USER_STATUS.US_NULL)
            {
                OnUserLeaveEvent(UserStatus.dwUserID);
            }
        }
        private void OnUserScoreEvent(System.Object Target)
        {
            CMD_GR_UserScore UserScore = (CMD_GR_UserScore)Target;
            if (!mPlayerDictionary.ContainsKey(UserScore.dwUserID))
                return;

            tagUserScore scoreBefore = new tagUserScore();
            Player player = mPlayerDictionary[UserScore.dwUserID];
            //以往数据
            scoreBefore.lScore = player.Score;
            scoreBefore.lGrade = player.Grade;
            scoreBefore.lInsure = player.Insure;
            scoreBefore.dwWinCount = player.WinCount;
            scoreBefore.dwLostCount = player.LostCount;
            scoreBefore.dwDrawCount = player.DrawCount;
            scoreBefore.dwFleeCount = player.FleeCount;
            scoreBefore.dwUserMedal = player.UserMedal;
            scoreBefore.dwExperience = player.Experience;
            scoreBefore.lLoveLiness = player.LoveLiness;

            //新数据
            player.Score = UserScore.UserScore.lScore;
            player.Grade = UserScore.UserScore.lGrade;
            player.Insure = UserScore.UserScore.lInsure;
            player.WinCount = UserScore.UserScore.dwWinCount;
            player.LostCount = UserScore.UserScore.dwLostCount;
            player.DrawCount = UserScore.UserScore.dwDrawCount;
            player.FleeCount = UserScore.UserScore.dwFleeCount;
            player.UserMedal = UserScore.UserScore.dwUserMedal;
            player.Experience = UserScore.UserScore.dwExperience;
            player.LoveLiness = UserScore.UserScore.lLoveLiness;

            if (OnUserEvent != null)
            {
                UserEvent Event = new UserEvent();

                Event.statusBefore = new tagUserStatus();
                Event.statusAfter = new tagUserStatus();

                Event.dwUserID = UserScore.dwUserID;
                Event.bScoreChange = true;
                Event.scoreBefore = scoreBefore;
                Event.scoreAfter = UserScore.UserScore;

                Event.statusBefore.cbUserStatus = player.UserStatus;
                Event.statusBefore.wChairID = player.ChairID;
                Event.statusBefore.wTableID = player.TableID;

                Event.statusAfter.cbUserStatus = player.UserStatus;
                Event.statusAfter.wChairID = player.ChairID;
                Event.statusAfter.wTableID = player.TableID;

                OnUserEvent(Event);
            }
        }
    }
}
