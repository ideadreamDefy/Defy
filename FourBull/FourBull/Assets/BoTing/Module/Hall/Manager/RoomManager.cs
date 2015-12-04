using BoTing.Framework;
using BoTing.GamePublic;
using System;
using System.Collections.Generic;

namespace BoTing.Module
{
    public enum ENUM_ROOM_EVENT
    {
        SHOW_ROOM,
        SHOW_ERROR,
    };

    internal class RoomEvent
    {
        public IRoom Target;
        public ENUM_ROOM_EVENT ev;
        public string Message;
        public RoomEvent(IRoom Target,ENUM_ROOM_EVENT ev,string Message="")
        {
            this.Target = Target;
            this.ev = ev;
            this.Message = Message;
        }
    }
    public class RoomManager : IDisposable
    {
        static private RoomManager gRoomManager;
        private List<Room> mRoomList = new List<Room>();
        internal delegate void RoomEventHandler(RoomEvent ev);
        internal event RoomEventHandler OnRoomEvent;
        private class SingletoneEnter { };
        static public RoomManager Instance
        {
            get
            {
                if (gRoomManager == null)
                    gRoomManager = new RoomManager(new SingletoneEnter());
                return gRoomManager;
            }
        }
        private RoomManager(SingletoneEnter enter)
        {

        }

        internal bool EnterRoom(tagGameServer Server, tagGameKind Kind)
        {
            if (FindByServerID(Server.wServerID) != null)
                return false;
            Room room = new Room(Server, Kind);
            mRoomList.Add(room);
            TaskExecute execute = new TaskExecute();
            execute.Push(new GameLoginTask(room));
            execute.Push(new WaitRoomCloseTask(room));
            execute.Execute(delegate(TASK_EVENT ev, BaseTask task)
            {
                DebugKit.Log("KL", "RoomManager.EnterRoom: " + ev.ToString());
                switch (ev)
                {
                    case TASK_EVENT.TASK_COMPLETE:
                        {
                            if (OnRoomEvent != null)
                            {
                                OnRoomEvent(new RoomEvent(room,ENUM_ROOM_EVENT.SHOW_ERROR));
                                room.Dispose();
                            }
                        }
                        break;
                    case TASK_EVENT.TASK_FINISH:
                        {
                            if(task.Name == typeof(GameLoginTask).ToString())
                            {
                                if (OnRoomEvent != null)
                                    OnRoomEvent(new RoomEvent(room, ENUM_ROOM_EVENT.SHOW_ROOM));
                            }
                        }
                        break;
                    case TASK_EVENT.TASK_EXECUTE:
                        break;
                    case TASK_EVENT.TASK_CANCEL:
                        {
                            OnRoomEvent(new RoomEvent(room, ENUM_ROOM_EVENT.SHOW_ERROR, task.Message));
                            room.Dispose();
                        }
                        break;
                }
            });
            return true;
        }
        public bool LeaveRoom(IRoom room)
        {
            return false;
        }
        public IRoom FindByServerID(ushort wServerID)
        {
            IRoom model = mRoomList.Find(f =>
            {
                return (f.ServerID == wServerID);
            });
            return model;
        }
        public IRoom FindByProcessName(string processName)
        {
            IRoom model = mRoomList.Find(f =>
            {
                return (f.ProcessName == processName);
            });
            return model;
        }

        public IRoom FindByKindID(ushort kindID)
        {
            IRoom model = mRoomList.Find(f =>
            {
                return (f.KindID == kindID);
            });
            return model;
        }
        public bool Remove(ushort wServerID)
        {
            Room model = (Room)FindByServerID(wServerID);
            if (model != null)
            {
                model.Close();
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            for (int i = 0; i < mRoomList.Count; ++i)
            {
                mRoomList[i].Close();
            }
        }
    }
}
