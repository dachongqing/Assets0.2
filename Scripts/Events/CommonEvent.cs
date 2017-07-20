using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CommonEvent : EventInterface {


    private String[] eventBeginInfo;
    private Dictionary<string, string[]> endInfoMap;

    private int goodCheckPoint;
    private int badCheckPoint;
    private string eventType;
    private string subEventType;

    private int goodValue;
    private int normalValue;
    private int badDiceNum;

    private List<string> effectedList;

    public CommonEvent(int good ,int bad, string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType,string subEventType,
       int goodValue, int normalValue, int badDiceNum)
    {
        this.goodCheckPoint = good;
        this.badCheckPoint = bad;
        this.eventBeginInfo = eventBeginInfo;
        this.endInfoMap = endInfoMap;
        this.eventType = eventType;
        this.subEventType = subEventType;
        this.goodValue = goodValue;
        this.normalValue = normalValue;
        this.badDiceNum = badDiceNum;
    }

    public EventResult excute(Character character, string selectCode, int rollValue)
    {
        if (this.effectedList== null || !this.effectedList.Contains(character.getName())) {
            Debug.Log(this.effectedList);
            Debug.Log(this.effectedList.Count);
            return doExcute(character, selectCode, rollValue);
        } else
        {
            EventResult er = new EventResult();
            er.setStatus(false);
            return er;
        }
    }

    public virtual EventResult doExcute(Character character, string selectCode, int rollValue)
    {
        return null;
    }

    public  string[] getEventBeginInfo()
    {
        return this.eventBeginInfo;
    }

    public  string[] getEventEndInfo(string resultCode)
    {
        return this.endInfoMap[resultCode];
    }

    public  string getEventType()
    {
        return this.eventType;
    }
    public string getSubEventType()
    {
        return this.subEventType;
    }

    public   Dictionary<string, string> getSelectItem()
    {
        return null;
    }

    public int getGoodValue()
    {
        return this.goodValue;
    }


    public int getGoodCheckPoint() {
        return this.goodCheckPoint;
    }

    public int getBadCheckPoint()
    {
        return this.badCheckPoint;
    }

    public int getNormalValue()
    {
        return this.normalValue;
    }

    public int getBadDiceNum()
    {
        return this.badDiceNum;
    }

    public void setEffectedList() {
         this.effectedList = new List<string>();
    }

    public void setEffectedList(List<string> list)
    {
        this.effectedList = list;
    }

    public List<string> getEffectedList()
    {
        return this.effectedList;
    }


}
