/**
 * Copyright (c) BoTing 2015
 * All rights reserved.
 * 
 * 文件名称：Observer
 * 简    述：
 * 创建时间：2015年10月23日13:55:02
 * 创 建 人：=== Coco===
 * 修改描述：
 * 修改时间：
 **/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public class Observer : MonoBehaviour
{
    private INotificationCenter center;
    private Dictionary<string, EventHandler> handlers;


    void Awake() {
        handlers = new Dictionary<string, EventHandler>();
        center = NotificationCenter.GenInstance();
    }

    void OnDestory() {
        foreach (KeyValuePair<string, EventHandler> kvp in handlers) {
            center.RemoveEventHandler(kvp.Key, kvp.Value);
        }
    }

    public void AddEventHandler(string name, EventHandler handler) {
        center.AddEventHandler(name,handler);
        handlers.Add(name, handler);
    }
}