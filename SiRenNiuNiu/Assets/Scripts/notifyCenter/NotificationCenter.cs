/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：NewBehaviourScript
 * 简    述：
 * 创建时间：2015年10月23日13:55:02
 * 创 建 人：=== 这里写自己的名字 ===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public abstract class INotificationCenter {
    protected Dictionary<string, EventHandler> eventTable;
    protected INotificationCenter() {
        eventTable = new Dictionary<string, EventHandler>();
    }

    public void PostNotification(string name) {
        this.PostNotification(name,null,EventArgs.Empty);
    }

    public void PostNotification(string name,object sender)
    {
        this.PostNotification(name, name, EventArgs.Empty);
    }

    public void PostNotification(string name, object sender,EventArgs e)
    {
        if (eventTable[name] != null) {
            eventTable[name](sender, e);
        }
    }

    public void AddEventHandler(string name, EventHandler handler) {
        eventTable[name] += handler;
    }

    public void RemoveEventHandler(string name, EventHandler handler) {
        eventTable[name] -= handler;
    }
}

public class NotificationCenter : INotificationCenter
{
    private static INotificationCenter singleton;

    private event EventHandler GameOver;
    private event EventHandler ScoreAdd;

    private NotificationCenter() : base() {
        eventTable["GameOver"] = GameOver;
        eventTable["ScoreAdd"] = ScoreAdd;
    }

    public static INotificationCenter GenInstance() {
        if (singleton == null) {
            singleton = new NotificationCenter();
        }
        return singleton;
    }
}