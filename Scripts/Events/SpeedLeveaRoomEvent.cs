using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLeveaRoomEvent : CommonEvent
{


    private int dicePoint;
    private DiceRollCtrl diceRoll = new DiceRollCtrl();

    public SpeedLeveaRoomEvent(int good, int bad, string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType, string subEventType,
      int goodValue, int normalValue, int bedDiceNum)
        :base(good, bad, eventBeginInfo,endInfoMap, eventType, subEventType, goodValue, normalValue, bedDiceNum)
    {

    }
    

    public override EventResult doExcute(Character character, String selectCode, int rollValue)
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

        if (  dicePoint <= this.getBadCheckPoint())
        {
            er.setStatus(false);           
			er.setResultCode(EventConstant.LEAVE_EVENT_BAD);
          
        }
        else {                       
            er.setResultCode(EventConstant.LEAVE_EVENT_SAFE);                      
            er.setStatus(true);
        }
            return er;
    }

   

    

   

   
}
