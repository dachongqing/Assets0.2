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

    public string getEventBeginInfo()

    {
        eventBeginInfo = "你约到了一个人要跟你谈心， 看看 你速度能否跟上他";
        return eventBeginInfo;
    }

    public string getEventEndInfo(string resultCode)
    {
        Debug.Log("resultCode " + resultCode);
        if (resultCode == EventConstant.LEAVE_EVENT_SAFE) {
            eventEndInfo = "太好了， 你安全的离开了房间。";
        }

        if (resultCode == EventConstant.LEAVE_EVENT_BAD) {
            eventEndInfo = "很遗憾， 你没能离开房间。";

        }
        if (resultCode == EventConstant.LEAVE_EVENT_SHIT)
        {
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
