using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedfallRoomEvent : MonoBehaviour , EventInterface
{
    public String[] eventBeginInfo;

    public Dictionary<string, String[]> endInfoMap;

    private int minSpeedPoint;

    private int maxSpeedPoint;

    private int badSpeedPoint;

    private int dicePoint;

    private String eventType;

	private RollDiceUIManager rollDUIM;

    public SpeedfallRoomEvent() {
        minSpeedPoint = 3;

        maxSpeedPoint = 6;

        badSpeedPoint = 0;

        eventType= EventConstant.DOWN_EVENT;
    }
    public EventResult excute(Character character, String selectCode, int rollValue) {


        EventResult er = new EventResult();
        //调用丢骰子UI
        int dicePoint = 2;
        if (minSpeedPoint <= dicePoint)
        {
            er.setStatus(true);
            if (dicePoint >= maxSpeedPoint)
            {
                er.setResultCode(EventConstant.LEAVE_EVENT_SAFE);
            }

        }
        else
        {
            if (dicePoint <= badSpeedPoint)
            {
                er.setResultCode(EventConstant.LEAVE_EVENT_BAD);
            }
            er.setStatus(false);
        }
        return er;

    }
    public EventResult excute(List<Character> characters)
    {
        throw new NotImplementedException();
    }

    public string[] getEventBeginInfo()
    {
        return eventBeginInfo;
    }

    public string[] getEventEndInfo(string resultCode)
    {

       
        return endInfoMap[resultCode];
    }



    public Dictionary<string, string> getSelectItem()
    {
        return null;
    }

    public string getEventType()
    {
        return eventType;
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
