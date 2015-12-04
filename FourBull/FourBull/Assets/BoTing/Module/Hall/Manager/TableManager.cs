using BoTing.Framework;
using System;
using System.Collections.Generic;

namespace BoTing.Module
{
    public enum ENUM_TABLE_EVENT
    {
        TABLE_INIT_EVENT,
        TABLE_UPDATE_EVENT,
    }
    internal class TableEvent
    {
        internal ushort wTableID;
        internal ENUM_TABLE_EVENT EventID;
        internal tagTableStatus TableStatus;
    }
    public class TableManager : IDisposable
    {
        internal delegate void TableEventHandler(TableEvent ev);
        internal event TableEventHandler OnTableEvent;

        private Room mRoom = null;
        private ISocketHander mSocketHander = null;
        private List<tagTableStatus> mTableList = new List<tagTableStatus>();
        public TableManager(IRoom room)
        {
            mRoom = (Room)room;
            mSocketHander = mRoom.SocketHander;

            //桌子信息
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnTableStatusEvent, (ushort)MAIN_CMD.MDM_GR_STATUS, (ushort)STATUS_SUB_CMD.SUB_GR_TABLE_STATUS, typeof(CMD_GR_TableStatus));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnTableInfoEvent, (ushort)MAIN_CMD.MDM_GR_STATUS, (ushort)STATUS_SUB_CMD.SUB_GR_TABLE_INFO, typeof(CMD_GR_TableInfo));
            mSocketHander.AddListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, OnSystemMessageEvent, (ushort)MAIN_CMD.MDM_CM_SYSTEM, (ushort)SYSTEM_SUB_CMD.SUB_CM_SYSTEM_MESSAGE, typeof(CMD_CM_SystemMessage));
        }
        public void Dispose()
        {
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_STATUS, (ushort)STATUS_SUB_CMD.SUB_GR_TABLE_STATUS);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_GR_STATUS, (ushort)STATUS_SUB_CMD.SUB_GR_TABLE_INFO);
            mSocketHander.RemoveListener(this, ENUM_SOCKET_EVENT.EVENT_MESSAGE, (ushort)MAIN_CMD.MDM_CM_SYSTEM, (ushort)SYSTEM_SUB_CMD.SUB_CM_SYSTEM_MESSAGE);

            mSocketHander = null;
            mRoom = null;
            mTableList.Clear();
            mTableList = null;
        }

        public List<tagTableStatus> TableList
        {
            get { return mTableList;}
        }

        private void OnTableStatusEvent(System.Object Target)
        {
            CMD_GR_TableStatus Obj = (CMD_GR_TableStatus)Target;
            if (Obj.wTableID >= mTableList.Count)
                return;
            mTableList[Obj.wTableID] = Obj.TableStatus;
            if (OnTableEvent != null)
            {
                TableEvent ev = new TableEvent();
                ev.EventID = ENUM_TABLE_EVENT.TABLE_UPDATE_EVENT;
                ev.wTableID = Obj.wTableID;
                ev.TableStatus = Obj.TableStatus;
                OnTableEvent(ev);
            }
        }

        private void OnTableInfoEvent(System.Object Target)
        {
            CMD_GR_TableInfo Obj = (CMD_GR_TableInfo)Target;
            foreach(var item in Obj.ItemArray)
            {
                mTableList.Add(item);
            }
            if (OnTableEvent != null)
            {
                TableEvent ev = new TableEvent();
                ev.EventID = ENUM_TABLE_EVENT.TABLE_INIT_EVENT;
                OnTableEvent(ev);
            }
        }
        private void OnSystemMessageEvent(System.Object Target)
        {
            CMD_CM_SystemMessage Obj = (CMD_CM_SystemMessage)Target;
            DebugKit.Log("System Message:" + Obj.szString);
        }
    }
}
