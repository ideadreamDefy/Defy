using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BoTing.GamePublic
{

public class EventTriggerAssist : EventTrigger
{
    private List<Action<PointerEventData>> onDownList = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> onUpList = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> onClickList = new List<Action<PointerEventData>>();

    private List<Action<PointerEventData>> onEnterList = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> onExitList = new List<Action<PointerEventData>>();

    private List<Action<PointerEventData>> onBeginDragList = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> onDragList = new List<Action<PointerEventData>>();
    private List<Action<PointerEventData>> onEndDragList = new List<Action<PointerEventData>>();

    private List<Action<PointerEventData>> onDropList = new List<Action<PointerEventData>>();
    //private List<Action<AxisEventData>> onMoveList = new List<Action<AxisEventData>>();

    //private List<Action<BaseEventData>> onSelectList = new List<Action<BaseEventData>>();
    //private List<Action<BaseEventData>> onUpdateSelectList = new List<Action<BaseEventData>>();

    static public EventTriggerAssist Get(GameObject inTarget)
    {
        if (!inTarget.GetComponent<EventTriggerAssist>())
            inTarget.AddComponent<EventTriggerAssist>();
        return inTarget.GetComponent<EventTriggerAssist>();
    }

    static public void SetEvent(GameObject gameObject, EventTriggerType eventTriggerType, Action<BaseEventData> action)
    {
        var trigger = gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                     trigger = gameObject.AddComponent<EventTrigger>();
        if(trigger.triggers == null)
            trigger.triggers = new List<Entry>();

        Entry entry = new Entry();
        entry.eventID = eventTriggerType;
        entry.callback = new TriggerEvent();
        entry.callback.AddListener(new UnityAction<BaseEventData>(action));
        trigger.triggers.Add(entry);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        onDownList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 鼠标按下事件
    /// </summary>
    public void onDown(Action<PointerEventData> inAction)
    {
        if (!onDownList.Contains(inAction))
        {
            onDownList.Add(inAction);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        onUpList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 鼠标弹起
    /// </summary>
    public void onUp(Action<PointerEventData> inAction)
    {
        if (!onUpList.Contains(inAction))
        {
            onUpList.Add(inAction);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        onClickList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    public void onClick(Action<PointerEventData> inAction)
    {
        if (!onClickList.Contains(inAction))
        {
            onClickList.Add(inAction);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        onEnterList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 鼠标进入
    /// </summary>
    public void onEnter(Action<PointerEventData> inAction)
    {
        if (!onEnterList.Contains(inAction))
        {
            onEnterList.Add(inAction);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        onExitList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 鼠标移出
    /// </summary>
    public void onExit(Action<PointerEventData> inAction)
    {
        if (!onExitList.Contains(inAction))
        {
            onExitList.Add(inAction);
        }
    }

    //public override void OnSelect(BaseEventData eventData)
    //{
    //    onSelectList.ForEach(i => i(eventData));
    //}
    ///// <summary>
    ///// on select
    ///// </summary>
    //public void onSelect(Action<BaseEventData> inAction)
    //{
    //    if (!onSelectList.Contains(inAction))
    //    {
    //        onSelectList.Add(inAction);
    //    }
    //}

    //public override void OnUpdateSelected(BaseEventData eventData)
    //{
    //    onUpdateSelectList.ForEach(i => i(eventData));
    //}
    ///// <summary>
    ///// on update select
    ///// </summary>
    //public void onUpdateSelect(Action<BaseEventData> inAction)
    //{
    //    if (!onUpdateSelectList.Contains(inAction))
    //    {
    //        onUpdateSelectList.Add(inAction);
    //    }
    //}

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDragList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    public void onBeginDrag(Action<PointerEventData> eventData)
    {
        if (!onBeginDragList.Contains(eventData))
        {
            onBeginDragList.Add(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        onDragList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 拖拽
    /// </summary>
    public void onDrag(Action<PointerEventData> eventData)
    {
        if (!onDragList.Contains(eventData))
        {
            onDragList.Add(eventData);
        }
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        onEndDragList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void onEndDrag(Action<PointerEventData> eventData)
    {
        if (!onEndDragList.Contains(eventData))
        {
            onEndDragList.Add(eventData);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        onDropList.ForEach(i => i(eventData));
    }
    /// <summary>
    /// on drop
    /// </summary>
    public void onDrop(Action<PointerEventData> eventData)
    {
        if (!onDropList.Contains(eventData))
        {
            onDropList.Add(eventData);
        }
    }

    //public override void OnMove(AxisEventData eventData)
    //{
    //    onMoveList.ForEach(i => i(eventData));
    //}
    ///// <summary>
    ///// on move
    ///// </summary>
    //public void onMove(Action<AxisEventData> eventData)
    //{
    //    if (!onMoveList.Contains(eventData))
    //    {
    //        onMoveList.Add(eventData);
    //    }
    //}

    void OnDestroy()
    {
        onClickList.Clear();
        onDownList.Clear();
        onClickList.Clear();
        onEnterList.Clear();
        onExitList.Clear();
        //onSelectList.Clear();
        //onUpdateSelectList.Clear();
        onBeginDragList.Clear();
        onDragList.Clear();
        onEndDragList.Clear();
        onDropList.Clear();
    }
}

}