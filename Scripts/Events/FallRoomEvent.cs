using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRoomEvent : CommonEvent
{

    private DiceRollCtrl diceRoll = new DiceRollCtrl();
    private int dicePoint;

    public FallRoomEvent(int good, int bad, string[] eventBeginInfo, Dictionary<string, string[]> endInfoMap, string eventType, string subEventType, int goodValue, int normalValue, int badDiceNum) : 
        base(good, bad, eventBeginInfo, endInfoMap, eventType, subEventType, goodValue, normalValue, badDiceNum)
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
            int spe = character.getAbilityInfo()[1] + character.getDiceNumberBuffer();
            dicePoint = diceRoll.calculateDice(spe);
            dicePoint = dicePoint + character.getDiceValueBuffer();

        }

        if (dicePoint <= this.getBadCheckPoint())
        {
            er.setStatus(false);
            er.setResultCode(EventConstant.FALL_DOWN__EVENT_BAD);

        }
        else if (dicePoint > this.getBadCheckPoint() && dicePoint < this.getGoodCheckPoint())
        {
            er.setStatus(false);
            er.setResultCode(EventConstant.FALL_DOWN__EVENT_NORMAL);
        }
        else
        {
            er.setStatus(true);
            er.setResultCode(EventConstant.FALL_DOWN__EVENT_GOOD);
        }

        return er;
    }
}
