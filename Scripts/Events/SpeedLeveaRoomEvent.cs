using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLeveaRoomEvent : MonoBehaviour, EventInterface
{

    private String[] eventBeginInfo; 
    private Dictionary<string, string[]> endInfoMap;

   
    private int minSpeedPoint;

    private int maxSpeedPoint;

    private int badSpeedPoint;

    private int dicePoint;

    private String eventType;

    private DiceRollCtrl diceRoll = new DiceRollCtrl();

	

    public SpeedLeveaRoomEvent() {

        minSpeedPoint = 6;

        maxSpeedPoint = 15;

        badSpeedPoint = 3;

        eventType = EventConstant.LEAVE_EVENT;
    }


    public EventResult excute(Character character, String selectCode, int rollValue)
    {

        EventResult er = new EventResult();
        int dicePoint = 0;
        if (character.isPlayer())
        {
			
			dicePoint = rollValue;
			Debug.Log ("事件判定 你的结果为 "+dicePoint);

        }
        else {	
			int speed = character.getAbilityInfo()[1] + character.getDiceNumberBuffer();

				dicePoint = diceRoll.calculateDice(speed);
			dicePoint = dicePoint + character.getDiceValueBuffer();

        }

        if (minSpeedPoint <= dicePoint)
        {
            er.setStatus(true);
           
			er.setResultCode(EventConstant.LEAVE_EVENT_SAFE);
          
        }
        else {
            if (dicePoint <= badSpeedPoint)
            {
                character.getAbilityInfo()[1] = character.getAbilityInfo()[1] - 1;
                er.setResultCode(EventConstant.LEAVE_EVENT_SHIT);
            }
            else {
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
        //eventBeginInfo = "你约到了一个人要跟你谈心， 看看 你速度能否跟上他";
        return eventBeginInfo;
    }

    public string[] getEventEndInfo(string resultCode)
    {
        Debug.Log("resultCode " + resultCode);
        
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
