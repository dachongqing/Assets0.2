using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanCheckRoomEvent : CommonEvent
{
 
    private DiceRollCtrl diceRoll = new DiceRollCtrl();
    private int dicePoint;

    public SanCheckRoomEvent(int good, int bad,string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType, string subEventType,
        int goodValue, int normalValue, int bedDiceNum)
        :base(good, bad, eventBeginInfo,endInfoMap, eventType, subEventType, goodValue, normalValue, bedDiceNum)
    {
      
    }
    
    public override EventResult doExcute(Character character, string selectCode, int rollValue)
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

        if (dicePoint <= this.getBadCheckPoint())
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.SANCHECK_EVENT_BED);

        }
        else if (dicePoint > this.getBadCheckPoint() && dicePoint < this.getGoodCheckPoint()) 
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.SANCHECK_EVENT_NORMAL);
         } else
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.SANCHECK_EVENT_GOOD);
        }
           
        return er;
    }

    

   

   

   

    
}
