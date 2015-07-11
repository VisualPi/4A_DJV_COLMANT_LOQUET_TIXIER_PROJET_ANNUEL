using System;
using System.Collections.Generic;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.ScrollList.Item;
using Assets.Scripts.GUI.ScrollList.Manager;
using UnityEngine;

public static class GameEventMessage {
    private static ManagedPopupEventMessage _managedPopupEvent;
    private static Sprite[] _iconsEventMessageSprite;

    static GameEventMessage() {
        _iconsEventMessageSprite = Resources.LoadAll<Sprite>("IconsEventMessage");

        var panelPopupEventInfo = GameObject.Find("ScrollViewPopupPanel");
        _managedPopupEvent = panelPopupEventInfo.GetComponent<ManagedPopupEventMessage>();
    }

    public static void AddEventMessage(EIconEventMessage iconEventMessage, string title, string description, DateTime dt) {
        var item = new ItemEvent {
            Title = title,
            Description = description,
            Timestamp = dt.ToString("T"),
            
            Icon = _iconsEventMessageSprite[(int)iconEventMessage],
            DoWorkEvent = FunctionTmp
        };

        _managedPopupEvent.PopulatePanel(new List<ItemEvent> { item });

        // TODO AMAU : Add implementation of ManagedEventMessage like ManagedPopupEvent
    }

    public static void FunctionTmp() {
        Debug.Log("FunctionTmp");
    }
}
