using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLeveaRoomEvent : MonoBehaviour, EventInterface
{

    public String eventBeginInfo;

    public String eventEndInfo;

   
    private int minSpeedPoint;

    private int maxSpeedPoint;

    private int badSpeedPoint;

    private int dicePoint;

    private String eventType;

    public SpeedLeveaRoomEvent() {

        minSpeedPoint = 3;

        maxSpeedPoint = 6;

        badSpeedPoint = 0;

        eventType = EventConstant.LEAVE_EVENT;
    }


    public EventResult excute(Character character, String selectCode)
    {

        EventResult er = new EventResult();
        //调用丢骰子UI
        //int dicePoint = callDiceController(character.getAbilityInfo[1]);
        int dicePoint = 0;
        if (minSpeedPoint <= dicePoint)
        {
            er.setStatus(true);
            if (dicePoint >= maxSpeedPoint) {
            er.setResultCode(EventConstant.LEAVE_EVENT_SAFE);
            }
          
        }
        else {
            if (dicePoint <= badSpeedPoint)
            {
                er.setResultCode(EventConstant.LEAVE_EVENT_BAD);
                character.getAbilityInfo()[1] = character.getAbilityInfo()[1] - 2;
            }
            er.setStatus(false);
        }
            return er;
    }

    public EventResult excute(List<Character> characters)
    {
        throw new NotImplementedException();
    }

    public string getEventBeginInfo()
    {
        return eventBeginInfo;
    }

    public string getEventEndInfo(string resultCode)
    {

        if (resultCode == EventConstant.LEAVE_EVENT_SAFE) {
            eventEndInfo = "太好了， 你安全的离开了房间。";
        }

        if (resultCode == EventConstant.LEAVE_EVENT_BAD) {
            eventEndInfo = "很遗憾， 你没能离开房间，而且受到了伤害。";

        }
        return eventEndInfo;
    }

   

    public Dictionary<string, string> getSelectItem()
    {
        return null;
    }

    public string getEventType()
    {
        return eventType;
    }
}
