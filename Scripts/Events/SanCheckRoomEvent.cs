using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanCheckRoomEvent : EventInterface
{
 
    private int goodSanCheckPoint;
    private int badSanCheckPoint;

    private DiceRollCtrl diceRoll = new DiceRollCtrl();
    private int dicePoint;

    public SanCheckRoomEvent(int good, int bad)
    {      
        goodSanCheckPoint = good;
        badSanCheckPoint = bad;     
    }

    public String[] eventBeginInfo;
    public Dictionary<string,string[]> endInfoMap;


    public EventResult excute(List<Character> characters)
    {
        throw new NotImplementedException();
    }

    public EventResult excute(Character character, string selectCode, int rollValue)
    {
        EventResult er = new EventResult();
        int dicePoint = 0;
        if (character.isPlayer())
        {
            dicePoint = rollValue;
            //Debug.Log("事件判定 你的结果为 " + dicePoint);
        }
        else
        {
            int san = character.getAbilityInfo()[3] + character.getDiceNumberBuffer();
            dicePoint = diceRoll.calculateDice(san);
            dicePoint = dicePoint + character.getDiceValueBuffer();

        }

        if (badSanCheckPoint > dicePoint)
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.SANCHECK_EVENT_BAD);

        }
        else
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.SANCHECK_EVENT_SAFE);
         }
           
        return er;
    }

    public string[] getEventBeginInfo()
    {
        return eventBeginInfo;
    }

    public string[] getEventEndInfo(string resultCode)
    {
        Debug.Log("resultCode " + resultCode + this.endInfoMap.Count);
        return this.endInfoMap[resultCode];
    }

    public string getEventType()
    {
        return EventConstant.SANCHECK_EVENT;
    }

    public Dictionary<string, string> getSelectItem()
    {
        return null;
    }

    public void setEventBeginInfo(string[] infos)
    {
        this.eventBeginInfo = infos;
    }

    public void setEventEndInfo(Dictionary<string, string[]> endMap)
    {
        this.endInfoMap = endMap;
    }

    
}
