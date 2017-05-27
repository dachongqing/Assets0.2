﻿using System;
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

	private RollDiceUIManager rollUIMag;

    public SpeedLeveaRoomEvent() {

        minSpeedPoint = 3;

        maxSpeedPoint = 6;

        badSpeedPoint = 0;

        eventType = EventConstant.LEAVE_EVENT;
                
//		rollUIMag=FindObjectOfType<RollDiceUIManager>();会有脚本先后顺序上的bug
    }


    public EventResult excute(Character character, String selectCode)
    {

        EventResult er = new EventResult();
        int dicePoint = 0;
        if (character.isPlayer())
        {
			rollUIMag=FindObjectOfType<RollDiceUIManager>();
            //调用丢骰子UI
			dicePoint = rollUIMag.rollForJugement(character.getAbilityInfo()[1]);

        }
        else {
            int speed = character.getAbilityInfo()[1];
            dicePoint = diceRoll.calculateDice(speed);
        }

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
        eventBeginInfo = "fsdfsdfsdfsdfsdfsdfsdf";
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
